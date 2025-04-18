using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Http.Constants;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Kontur.Extern.Api.Client.Http.Models;
using Kontur.Extern.Api.Client.Http.Options;
using Kontur.Extern.Api.Client.Http.Serialization;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters
{
    internal class HttpRequest : IPayloadHttpRequest, IPayloadSpecifiedRequest
    {
        private Request request;
        private readonly RequestTimeouts requestTimeouts;
        private readonly Func<Request, TimeSpan, Task<Request>>? requestTransformAsync;
        private readonly Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler;
        private readonly FailoverAsync? failoverAsync;
        private readonly IClusterClient clusterClient;
        private readonly ILog log;
        private readonly IJsonSerializer serializer;
        private IHttpContent? payload;

        public HttpRequest(
            Request request,
            RequestTimeouts requestTimeouts,
            Func<Request, TimeSpan, Task<Request>>? requestTransformAsync,
            Func<IHttpResponse, ValueTask<bool>>? errorResponseHandler,
            FailoverAsync? failoverAsync,
            IClusterClient clusterClient,
            IJsonSerializer serializer,
            ILog log)
        {
            this.request = request ?? throw new ArgumentNullException(nameof(request));
            this.requestTimeouts = requestTimeouts ?? throw new ArgumentNullException(nameof(requestTimeouts));
            this.requestTransformAsync = requestTransformAsync;
            this.errorResponseHandler = errorResponseHandler;
            this.failoverAsync = failoverAsync;
            this.clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
            this.log = log;
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public IPayloadSpecifiedRequest WithPayload(IHttpContent content)
        {
            payload = content;
            return this;
        }

        public IHttpRequest Accept(string contentType)
        {
            request = request.WithAcceptHeader(contentType);
            return this;
        }

        public IHttpRequest Authorization(string scheme, in Base64String parameter)
        {
            request = request.WithAuthorizationHeader(scheme, parameter.ToString());
            return this;
        }

        public IHttpRequest Range(long from, long? to)
        {
            request = request.WithRangeHeader(from, to);
            return this;
        }

        public IHttpRequest ContentRange(long from, long to, long? totalLength)
        {
            if (payload == null)
                throw Errors.ContentMustBeSpecifiedBeforeSetRangeHeader();

            var contentLength = payload.Length;
            if (from > to)
                throw Errors.ContentRangeMustHaveValidBounds(nameof(to), from, to);
            if (contentLength.HasValue && to - from + 1 != contentLength)
                throw Errors.ContentRangeMustHaveEqualBytesAsContentLength(nameof(to), from, to, contentLength.Value);

            if (totalLength.HasValue)
            {
                if (totalLength < contentLength)
                    throw Errors.TotalLengthMustBeGreaterOrEqualToContentLength(nameof(totalLength), totalLength.Value, contentLength.Value);

                request = request.WithContentRangeHeader(from, to, totalLength.Value);
            }
            else
            {
                request = request.WithContentRangeHeader(from, to);
            }

            return this;
        }

        public async Task<IHttpResponse> SendAsync(TimeoutSpecification timeoutSpecification = default, Func<IHttpResponse, bool>? ignoreResponseErrors = null)
        {
            var attempt = 0u;
            while (true)
            {
                var httpResponse = await SendRequestAsync().ConfigureAwait(false);
                if (httpResponse.Status.IsSuccessful)
                    return httpResponse;

                var handlingResult = await HandleErrorAsync(httpResponse, ignoreResponseErrors, attempt++).ConfigureAwait(false);
                if (handlingResult == ErrorHandlingResult.ReturnResponse)
                    return httpResponse;
            }

            static Request AddTimeout(Request resultRequest, TimeSpan timeout) =>
                resultRequest.WithHeader(HttpHeaders.TimeoutHeader, timeout.ToString("c"));

            async Task<IHttpResponse> SendRequestAsync()
            {
                var timeout = timeoutSpecification.GetTimeout(request, requestTimeouts);
                var timeBudget = TimeBudget.StartNew(timeout);

                var resultRequest = request;
                if (requestTransformAsync != null)
                {
                    resultRequest = await requestTransformAsync(resultRequest, timeBudget.Remaining).ConfigureAwait(false);
                }

                resultRequest = AddTimeout(resultRequest, timeBudget.Remaining);
                if (payload != null)
                {
                    resultRequest = payload.Apply(resultRequest, serializer);
                }

                return await TrySendRequestAsync(resultRequest, timeBudget.Remaining).ConfigureAwait(false);
            }
        }

        private async Task<ErrorHandlingResult> HandleErrorAsync(IHttpResponse response, Func<IHttpResponse, bool>? ignoreResponseErrors, uint attempt)
        {
            var responseStatus = response.Status;
            if (ignoreResponseErrors != null && ignoreResponseErrors(response))
            {
                return ErrorHandlingResult.ReturnResponse;
            }

            if (failoverAsync != null)
            {
                var failoverDecision = await failoverAsync(response, attempt).ConfigureAwait(false);
                if (failoverDecision == FailoverDecision.RepeatRequest)
                    return ErrorHandlingResult.RepeatRequest;
            }

            if (errorResponseHandler == null || !await errorResponseHandler(response).ConfigureAwait(false))
            {
                responseStatus.EnsureSuccess();
            }

            return ErrorHandlingResult.ReturnResponse;
        }

        private async Task<IHttpResponse> TrySendRequestAsync(Request resultRequest, TimeSpan leftTimeout)
        {
            var result = await clusterClient.SendAsync(resultRequest, leftTimeout).ConfigureAwait(false);

            var resultLog = $"Client request '{resultRequest.Method} {resultRequest.Url}' ended with status = '{result.Status}'; " +
                            $"response-code = '{result.Response.Code}'; trace-id = '{result.Response.Headers["X-Kontur-Trace-Id"] ?? "none"}'";
            if (result.Response.Code.IsSuccessful())
                log.Info(resultLog);
            else if (result.Response.Code.IsClientError() || result.Response.Code.IsRedirection())
                log.Warn(resultLog);
            else
                log.Error(resultLog);

            return new HttpResponse(resultRequest, result.Response, serializer);
        }

        private enum ErrorHandlingResult
        {
            ReturnResponse,
            RepeatRequest
        }
    }
}

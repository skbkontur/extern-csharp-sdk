#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.HttpLevel.Constants;
using Kontur.Extern.Client.HttpLevel.Models;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Client.HttpLevel.ClusterClientAdapters
{
    internal class HttpRequest : IPayloadHttpRequest
    {
        private Request request;
        private readonly RequestSendingOptions options;
        private readonly Func<Request, TimeSpan, Task<Request>>? requestTransformAsync;
        private readonly Func<IHttpResponse, bool>? errorResponseHandler;
        private readonly IClusterClient clusterClient;
        private readonly IJsonSerializer serializer;

        public HttpRequest(
            Request request,
            RequestSendingOptions options,
            Func<Request, TimeSpan, Task<Request>>? requestTransformAsync,
            Func<IHttpResponse, bool>? errorResponseHandler,
            IClusterClient clusterClient,
            IJsonSerializer serializer)
        {
            this.request = request ?? throw new ArgumentNullException(nameof(request));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.requestTransformAsync = requestTransformAsync;
            this.errorResponseHandler = errorResponseHandler;
            this.clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public IHttpRequest WithPayload(IHttpContent content)
        {
            request = content.Apply(request, serializer);
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

        public async Task<IHttpResponse> SendAsync(TimeSpan? timeout = null, Func<IHttpResponse, bool>? ignoreResponseErrors = null)
        {
            timeout ??= request.IsWriteRequest() ? options.DefaultWriteTimeout : options.DefaultReadTimeout;
            var timeBudget = TimeBudget.StartNew(timeout.Value);

            var resultRequest = request;
            if (requestTransformAsync != null)
            {
                resultRequest = await requestTransformAsync(resultRequest, timeBudget.Remaining).ConfigureAwait(false);
            }

            resultRequest = AddTimeout(resultRequest, timeBudget.Remaining);
            var httpResponse = await TrySendRequestAsync(resultRequest, timeBudget.Remaining).ConfigureAwait(false);
            
            return EnsureSuccess(httpResponse, ignoreResponseErrors);

            static Request AddTimeout(Request resultRequest, TimeSpan timeout) =>
                resultRequest.WithHeader(HttpHeaders.TimeoutHeader, timeout.ToString("c"));
        }

        private IHttpResponse EnsureSuccess(IHttpResponse response, Func<IHttpResponse, bool>? ignoreResponseErrors)
        {
            var responseStatus = response.Status;
            if (!responseStatus.IsSuccessful)
            {
                if (ignoreResponseErrors != null && ignoreResponseErrors(response))
                {
                    return response;
                }

                if (errorResponseHandler == null || !errorResponseHandler(response))
                {
                    responseStatus.EnsureSuccess();
                }
            }

            return response;
        }

        private async Task<IHttpResponse> TrySendRequestAsync(Request resultRequest, TimeSpan leftTimeout)
        {
            var result = await clusterClient.SendAsync(resultRequest, leftTimeout).ConfigureAwait(false);
            return new HttpResponse(resultRequest, result.Response, serializer);
        }
    }
}
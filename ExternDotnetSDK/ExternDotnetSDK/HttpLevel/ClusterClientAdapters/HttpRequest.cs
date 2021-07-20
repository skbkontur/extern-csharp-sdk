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
using Vostok.Logging.Abstractions;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Client.HttpLevel.ClusterClientAdapters
{
    internal class HttpRequest : IPayloadHttpRequest
    {
        private Request request;
        private readonly RequestSendingOptions options;
        private readonly Func<Request, TimeSpan, Task<Request>>? requestTransformAsync;
        private readonly IClusterClient clusterClient;
        private readonly IJsonSerializer serializer;
        private readonly ILog log;

        public HttpRequest(
            Request request,
            RequestSendingOptions options,
            Func<Request, TimeSpan, Task<Request>>? requestTransformAsync,
            IClusterClient clusterClient,
            IJsonSerializer serializer,
            ILog log)
        {
            this.request = request;
            this.options = options;
            this.requestTransformAsync = requestTransformAsync;
            this.clusterClient = clusterClient;
            this.serializer = serializer;
            this.log = log;
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

        public async Task<IHttpResponse> SendAsync(TimeSpan? timeout = null)
        {
            var httpResponse = await TrySendAsync(timeout).ConfigureAwait(false);
            return EnsureSuccess(httpResponse);
        }

        public async Task<IHttpResponse> TrySendAsync(TimeSpan? timeout = null)
        {
            timeout ??= request.IsWriteRequest() ? options.DefaultWriteTimeout : options.DefaultReadTimeout;
            var timeBudget = TimeBudget.StartNew(timeout.Value);

            var resultRequest = request;
            if (requestTransformAsync != null)
            {
                resultRequest = await requestTransformAsync(resultRequest, timeBudget.Remaining).ConfigureAwait(false);
            }

            resultRequest = AddTimeout(resultRequest, timeBudget.Remaining);
            return await TrySendRequestAsync(resultRequest, timeBudget.Remaining).ConfigureAwait(false);

            static Request AddTimeout(Request resultRequest, TimeSpan timeout) =>
                resultRequest.WithHeader(HttpHeaders.TimeoutHeader, timeout.ToString("c"));
        }

        private IHttpResponse EnsureSuccess(IHttpResponse response)
        {
            var responseStatus = response.Status;
            if (!responseStatus.IsSuccessful)
            {
                log.Error($"StatusCode: {responseStatus.StatusCode}");
                responseStatus.EnsureSuccess();
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
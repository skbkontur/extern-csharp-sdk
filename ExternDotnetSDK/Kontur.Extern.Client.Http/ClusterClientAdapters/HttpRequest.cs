#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Http.Constants;
using Kontur.Extern.Client.Http.Models;
using Kontur.Extern.Client.Http.Options;
using Kontur.Extern.Client.Http.Serialization;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;
using Request = Vostok.Clusterclient.Core.Model.Request;

namespace Kontur.Extern.Client.Http.ClusterClientAdapters
{
    internal class HttpRequest : IPayloadHttpRequest
    {
        private Request request;
        private readonly RequestTimeouts requestTimeouts;
        private readonly Func<Request, TimeSpan, Task<Request>>? requestTransformAsync;
        private readonly Func<IHttpResponse, bool>? errorResponseHandler;
        private readonly IClusterClient clusterClient;
        private readonly IJsonSerializer serializer;

        public HttpRequest(
            Request request,
            RequestTimeouts requestTimeouts,
            Func<Request, TimeSpan, Task<Request>>? requestTransformAsync,
            Func<IHttpResponse, bool>? errorResponseHandler,
            IClusterClient clusterClient,
            IJsonSerializer serializer)
        {
            this.request = request ?? throw new ArgumentNullException(nameof(request));
            this.requestTimeouts = requestTimeouts ?? throw new ArgumentNullException(nameof(requestTimeouts));
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

        public async Task<IHttpResponse> SendAsync(TimeoutSpecification timeoutSpecification = default, Func<IHttpResponse, bool>? ignoreResponseErrors = null)
        {
            var timeout = timeoutSpecification.GetTimeout(request, requestTimeouts);
            var timeBudget = TimeBudget.StartNew(timeout);

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
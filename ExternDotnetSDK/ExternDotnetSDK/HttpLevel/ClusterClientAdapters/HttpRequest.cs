using System;
using System.IO;
using System.Threading.Tasks;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;
using Request = Vostok.Clusterclient.Core.Model.Request;
using StreamContent = Vostok.Clusterclient.Core.Model.StreamContent;

namespace Kontur.Extern.Client.HttpLevel.ClusterClientAdapters
{
    internal class HttpRequest : IPayloadHttpRequest
    {
        private Request request;
        private readonly RequestSendingOptions options;
        private readonly AuthenticationOptions authOptions;
        private readonly IClusterClient clusterClient;
        private readonly IRequestBodySerializer serializer;
        private readonly ILog log;

        public HttpRequest(Request request, RequestSendingOptions options, AuthenticationOptions authOptions, IClusterClient clusterClient, IRequestBodySerializer serializer, ILog log)
        {
            this.request = request;
            this.options = options;
            this.authOptions = authOptions;
            this.clusterClient = clusterClient;
            this.serializer = serializer;
            this.log = log;
        }

        public IHttpRequest WithPayload<TRequestMessage>(TRequestMessage message)
        {
            if (message is byte[] bytes)
            {
                request = request.WithContent(bytes);
                return this;
            }
            
            var memoryStream = new MemoryStream();
            serializer.SerializeToJsonStream(message, memoryStream);
            memoryStream.Position = 0;
            request = request.WithContent(new StreamContent(memoryStream)).WithContentTypeHeader(ContentTypes.Json);
            return this;
        }

        public IHttpRequest WithFormUrlEncoded(string content)
        {
            request = request.WithContent(content).WithContentTypeHeader(ContentTypes.FormUrlEncoded);
            return this;
        }

        public IHttpRequest WithJsonPayload(string json)
        {
            request = request.WithContent(json).WithContentTypeHeader(ContentTypes.Json);
            return this;
        }

        public IHttpRequest Accept(string contentType)
        {
            request = request.WithAcceptHeader(contentType);
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

            var sessionId = await authOptions.Provider.GetSessionId(timeBudget.Remaining).ConfigureAwait(false);

            var leftTimeout = timeBudget.Remaining;
            var resultRequest = BuildRequest(request, sessionId, authOptions.ApiKey, leftTimeout);

            return await TrySendRequestAsync(resultRequest, leftTimeout).ConfigureAwait(false);

            static Request BuildRequest(Request request, string sessionId, string apiKey, TimeSpan? timeout)
            {
                // await AuthenticationProvider.AuthenticateAsync().ConfigureAwait(false);
                // AuthenticationProvider.ApplyAuth(request);
                var resultRequest = request
                    .WithAuthorizationHeader(AuthSchemes.AuthSid, sessionId)
                    .WithHeader(HttpHeaders.ApiKeyHeader, apiKey);

                return timeout == null 
                    ? resultRequest 
                    : resultRequest.WithHeader(HttpHeaders.TimeoutHeader, timeout.Value.ToString("c"));
            }
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
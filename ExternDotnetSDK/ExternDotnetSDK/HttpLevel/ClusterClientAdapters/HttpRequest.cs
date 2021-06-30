using System;
using System.IO;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Clients.Common.Logging;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;
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
        private readonly ILogger logger;

        public HttpRequest(Request request, RequestSendingOptions options, AuthenticationOptions authOptions, IClusterClient clusterClient, IRequestBodySerializer serializer, ILogger logger)
        {
            this.request = request;
            this.options = options;
            this.authOptions = authOptions;
            this.clusterClient = clusterClient;
            this.serializer = serializer;
            this.logger = logger;
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

        public IHttpRequest WithJsonPayload(string json)
        {
            request = request.WithContent(json).WithContentTypeHeader(ContentTypes.Json);
            return this;
        }

        public async Task<IHttpResponse> SendAsync(TimeSpan? timeout = null)
        {
            timeout ??= request.IsWriteRequest() ? options.DefaultWriteTimeout : options.DefaultReadTimeout;
            var timeBudget = TimeBudget.StartNew(timeout.Value);

            var sessionId = await authOptions.Provider.GetSessionId(timeBudget.Remaining).ConfigureAwait(false);

            var leftTimeout = timeBudget.Remaining;
            var resultRequest = BuildRequest(request, sessionId, authOptions.ApiKey, leftTimeout);

            return await SendRequestAsync(resultRequest, leftTimeout);

            static Request BuildRequest(Request request, string sessionId, string apiKey, TimeSpan? timeout)
            {
                var resultRequest = request
                    .WithAuthorizationHeader(AuthSchemes.AuthSid, sessionId)
                    .WithHeader(HttpHeaders.ApiKeyHeader, apiKey);

                return timeout == null 
                    ? resultRequest 
                    : resultRequest.WithHeader(HttpHeaders.TimeoutHeader, timeout.Value.ToString("c"));
            }
        }

        private async Task<IHttpResponse> SendRequestAsync(Request resultRequest, TimeSpan leftTimeout)
        {
            var result = await clusterClient.SendAsync(resultRequest, leftTimeout).ConfigureAwait(false);
            try
            {
                return new HttpResponse(resultRequest, result.Response.EnsureSuccessStatusCode(), serializer);
            }
            catch (Exception e)
            {
                // todo: print response here (to not expose it to ILogger interface)
                //logger.Log(result, e);
                throw;
            }
        }
    }
}
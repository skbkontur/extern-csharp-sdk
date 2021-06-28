using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Exceptions;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;

namespace Kontur.Extern.Client.Clients.Common.RequestSenders
{
    internal class RequestSender : IRequestSender
    {
        private readonly RequestSendingOptions options;
        private readonly AuthenticationOptions authOption;
        private readonly IClusterClient clusterClient;
        private readonly IRequestBodySerializer serializer;

        public RequestSender(
            RequestSendingOptions options, 
            AuthenticationOptions authOption,
            IClusterClient clusterClient,
            IRequestBodySerializer serializer)
        {
            this.options = options;
            this.authOption = authOption;
            this.clusterClient = clusterClient;
            this.serializer = serializer;
        }

        public Task SendAsync(RequestBuilder requestBuilder, TimeSpan? timeout = null) => SendRequestAsync(requestBuilder, timeout);

        public async Task<TResponse> SendAsync<TResponse>(RequestBuilder requestBuilder, TimeSpan? timeout = null)
        {
            var response = await SendRequestAsync(requestBuilder, timeout).ConfigureAwait(false);
            if (!response.HasStream)
                throw Errors.ResponseHasToHaveBody(requestBuilder.ToString());
            if (response.Headers.ContentType != SenderConstants.MediaType) 
                throw Errors.ResponseHasUnexpectedContentType(requestBuilder.ToString(), response, SenderConstants.MediaType);

            var memoryStream = response.Content.ToMemoryStream();
            return serializer.DeserializeFromJson<TResponse>(memoryStream);
        }

        private async Task<Response> SendRequestAsync(RequestBuilder requestBuilder, TimeSpan? timeout)
        {
            timeout ??= requestBuilder.IsWriteRequest ? options.DefaultWriteTimeout : options.DefaultReadTimeout;
            var timeBudget = TimeBudget.StartNew(timeout.Value);

            var sessionId = await authOption.Provider.GetSessionId(timeBudget.Remaining).ConfigureAwait(false);

            var leftTimeout = timeBudget.Remaining;
            var request = requestBuilder
                .BuildRequest(authOption.ApiKey, sessionId, leftTimeout);

            var result = await clusterClient.SendAsync(request, leftTimeout).ConfigureAwait(false);
            return result.Response.EnsureSuccessStatusCode();
        }
    }
}
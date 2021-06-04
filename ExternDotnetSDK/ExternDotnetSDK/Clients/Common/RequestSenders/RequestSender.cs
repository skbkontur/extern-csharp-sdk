using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Authentication.Providers;
using Kontur.Extern.Client.Clients.Common.ResponseMessages;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Clients.Common.RequestSenders
{
    public class RequestSender : IRequestSender
    {
        private readonly HttpClient client;

        public RequestSender(IAuthenticationProvider authenticationProvider, string apiKey, HttpClient client)
        {
            AuthenticationProvider = authenticationProvider;
            ApiKey = apiKey;
            this.client = client;
        }

        public IAuthenticationProvider AuthenticationProvider { get; }
        public string ApiKey { get; }
        public async Task<IResponseMessage> SendJsonAsync(Request request, TimeSpan? timeout = null)
        {
            await AuthenticationProvider.AuthenticateAsync().ConfigureAwait(false);
            AuthenticationProvider.ApplyAuth(request);

            var httpRequestMessage = request.ToHttpRequestMessage();
            httpRequestMessage.Headers.Add(SenderConstants.ApiKeyHeader, ApiKey);
            if (timeout != null)
                httpRequestMessage.Headers.Add(SenderConstants.TimeoutHeader, timeout.Value.ToString("c"));
            var httpResponseMessage = await client.SendAsync(httpRequestMessage).ConfigureAwait(false);
            return new ResponseMessage(httpResponseMessage);
        }

        public async Task<IResponseMessage> SendAsync(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object content = null,
            TimeSpan? timeout = null)
        {
            await AuthenticationProvider.AuthenticateAsync();
            var request = new HttpRequestMessage(method, GetFullUri(uriPath, uriQueryParams));
           // request.Headers.Authorization = new AuthenticationHeaderValue("Bearer ", authorizationHeader);
          
            request.Headers.Add(SenderConstants.ApiKeyHeader, ApiKey);
            TryAddContent(content, request);
            if (timeout != null)
                request.Headers.Add(SenderConstants.TimeoutHeader, timeout.Value.ToString("c"));
            return new ResponseMessage(await client.SendAsync(request));
        }

        private static void TryAddContent(object content, HttpRequestMessage request)
        {
            if (content == null) return;
            request.Content = new StringContent(JsonConvert.SerializeObject(content));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(SenderConstants.MediaType);
        }

        private static string GetFullUri(string requestUri, Dictionary<string, object> uriQueryParams) =>
            uriQueryParams != null
                ? $"{requestUri}?{string.Join("&", uriQueryParams.Select(x => $"{x.Key}={x.Value}"))}"
                : requestUri;
    }
}
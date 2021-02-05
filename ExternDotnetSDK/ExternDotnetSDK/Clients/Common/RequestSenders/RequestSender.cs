using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication;
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

        public async Task<IResponseMessage> SendAsync(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object content = null,
            TimeSpan? timeout = null)
        {
            var request = new HttpRequestMessage(method, GetFullUri(uriPath, uriQueryParams));
            request.Headers.Authorization = new AuthenticationHeaderValue(
                SenderConstants.AuthSidHeader,
                await AuthenticationProvider.GetSessionId().ConfigureAwait(false));
            request.Headers.Add(SenderConstants.ApiKeyHeader, ApiKey);
            TryAddContent(content, request);
            if (timeout != null)
                request.Headers.Add(SenderConstants.TimeoutHeader, timeout.Value.ToString("c"));
            return new ResponseMessage(await client.SendAsync(request).ConfigureAwait(false));
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
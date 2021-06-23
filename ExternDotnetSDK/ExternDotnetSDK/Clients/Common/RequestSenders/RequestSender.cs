using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication;
using Kontur.Extern.Client.Clients.Common.Requests;
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
            var httpRequestMessage = new HttpRequestMessage(ConvertHttpMethod(request.Method), request.Url);
            await AddAuthHeaders(httpRequestMessage).ConfigureAwait(false);
            AddJsonContent(httpRequestMessage, request);
            if (timeout != null)
                httpRequestMessage.Headers.Add(SenderConstants.TimeoutHeader, timeout.Value.ToString("c"));
            var httpResponseMessage = await client.SendAsync(httpRequestMessage).ConfigureAwait(false);
            return new ResponseMessage(httpResponseMessage);
        }

        private async Task AddAuthHeaders(HttpRequestMessage httpRequestMessage)
        {
            var sessionId = await AuthenticationProvider.GetSessionId().ConfigureAwait(false);
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(SenderConstants.AuthSidHeader, sessionId);
            httpRequestMessage.Headers.Add(SenderConstants.ApiKeyHeader, ApiKey);
        }

        private void AddJsonContent(HttpRequestMessage httpRequestMessage, Request request)
        {
            if (request.Method == RequestMethod.Get || string.IsNullOrEmpty(request.JsonContent))
                return;
            httpRequestMessage.Content = new StringContent(request.JsonContent);
            httpRequestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(SenderConstants.MediaType);
        }

        private static HttpMethod ConvertHttpMethod(RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                default:
                    throw new ArgumentOutOfRangeException(nameof(method), method, null);
            }
        }

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
                ? $"{requestUri}?{string.Join("&", uriQueryParams.Where(x => !string.IsNullOrEmpty(x.Value?.ToString())).Select(x => $"{x.Key}={x.Value}"))}"
                : requestUri;
    }
}
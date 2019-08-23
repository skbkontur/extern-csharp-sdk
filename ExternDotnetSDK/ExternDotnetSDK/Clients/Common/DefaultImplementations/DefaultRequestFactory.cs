using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Clients.Common.DefaultImplementations
{
    public class DefaultRequestFactory : IRequestFactory
    {
        private const string MediaType = "application/json";
        private const string AuthSidHeader = "auth.sid";
        private const string ApiKeyHeader = "X-Kontur-Apikey";

        public DefaultRequestFactory(string apiKey, string sessionId) =>
            AuthenticationProvider = new DefaultAuthenticationProvider(apiKey, sessionId);

        public DefaultRequestFactory(IAuthenticationProvider authenticationProvider) =>
            AuthenticationProvider = authenticationProvider;

        public IAuthenticationProvider AuthenticationProvider { get; }

        public IHaveHttpRequestMessage CreateAuthorizedRequest(
            HttpMethod method,
            string uriPath,
            Dictionary<string, object> uriQueryParams = null,
            object content = null)
        {
            var request = new HttpRequestMessage(method, GetFullUri(uriPath, uriQueryParams));
            request.Headers.Authorization = new AuthenticationHeaderValue(
                AuthSidHeader,
                AuthenticationProvider.GetSessionId());
            request.Headers.Add(ApiKeyHeader, AuthenticationProvider.GetApiKey());
            if (content == null)
                return new DefaultRequest(request);
            request.Content = new StringContent(JsonConvert.SerializeObject(content));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
            return new DefaultRequest(request);
        }

        private static string GetFullUri(string requestUri, Dictionary<string, object> uriQueryParams) =>
            uriQueryParams != null
                ? $"{requestUri}?{string.Join("&", uriQueryParams.Select(x => $"{x.Key}={x.Value.ToString()}"))}"
                : requestUri;
    }
}
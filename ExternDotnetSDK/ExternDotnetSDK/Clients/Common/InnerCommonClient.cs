using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common.SendAsync;
using ExternDotnetSDK.Logging;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Clients.Common
{
    public class InnerCommonClient
    {
        private const string MediaType = "application/json";
        private const string AuthSidHeader = "auth.sid";
        private const string ApiKeyHeader = "X-Kontur-Apikey";

        protected readonly ILogError LogError;
        protected readonly IAuthenticationProvider AuthenticationProvider;
        protected readonly ISendAsync RequestSender;

        public InnerCommonClient(ILogError logError, ISendAsync requestSender, IAuthenticationProvider authenticationProvider)
        {
            LogError = logError;
            AuthenticationProvider = authenticationProvider;
            RequestSender = requestSender;
        }

        protected async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string requestUri,
            Dictionary<string, object> uriQueryParams = null)
        {
            using (var request = MakeAuthorizedRequest(method, GetFullUri(requestUri, uriQueryParams)))
                return JsonConvert.DeserializeObject<TResult>(await TryGetResponseAsync(request));
        }

        protected async Task SendRequestAsync(
            HttpMethod method,
            string requestUri,
            Dictionary<string, object> uriQueryParams = null)
        {
            using (var request = MakeAuthorizedRequest(method, GetFullUri(requestUri, uriQueryParams)))
                await TryGetResponseAsync(request);
        }

        protected async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string requestUri,
            object contentDto,
            Dictionary<string, object> uriQueryParams = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(contentDto)))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
                using (var request = MakeAuthorizedRequest(method, GetFullUri(requestUri, uriQueryParams)))
                {
                    request.Content = content;
                    return JsonConvert.DeserializeObject<TResult>(await TryGetResponseAsync(request));
                }
            }
        }

        protected async Task SendRequestAsync(
            HttpMethod method,
            string requestUri,
            object contentDto,
            Dictionary<string, object> uriQueryParams = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(contentDto)))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
                using (var request = MakeAuthorizedRequest(method, GetFullUri(requestUri, uriQueryParams)))
                {
                    request.Content = content;
                    await TryGetResponseAsync(request);
                }
            }
        }

        private async Task<string> TryGetResponseAsync(HttpRequestMessage message)
        {
            try
            {
                var response = await RequestSender.SendAsync(message);
                response.HttpResponseMessage.EnsureSuccessStatusCode();
                using (var content = response.HttpResponseMessage.Content)
                    return await content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                LogError.Error(e);
                throw;
            }
        }

        private HttpRequestMessage MakeAuthorizedRequest(HttpMethod method, string fullUri)
        {
            var request = new HttpRequestMessage(method, fullUri);
            request.Headers.Authorization = new AuthenticationHeaderValue(
                AuthSidHeader,
                AuthenticationProvider.GetSessionId());
            request.Headers.Add(ApiKeyHeader, AuthenticationProvider.GetApiKey());
            return request;
        }

        private static string GetFullUri(string requestUri, Dictionary<string, object> uriQueryParams) =>
            uriQueryParams != null ? requestUri + StringifyUriQueryParams(uriQueryParams) : requestUri;

        private static string StringifyUriQueryParams(Dictionary<string, object> parameters) =>
            $"?{string.Join("&", parameters.Select(x => $"{x.Key}={x.Value.ToString()}"))}";
    }
}
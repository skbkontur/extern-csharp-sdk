using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ExternDotnetSDK.Logging;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Clients.Common
{
    public class InnerCommonClient : IHttpClient
    {
        protected readonly ILogError LogError;
        public HttpClient Client { get; }

        public InnerCommonClient(ILogError logError, HttpClient client)
        {
            LogError = logError;
            Client = client;
        }

        private async Task<string> GetResponseAsync(HttpRequestMessage message)
        {
            using (var response = await Client.SendAsync(message))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    using (var content = response.Content)
                        return await content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    LogError.Error(e);
                    throw;
                }
            }
        }

        protected async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string requestUri,
            Dictionary<string, object> uriQueryParams = null)
        {
            using (var request = new HttpRequestMessage(method, GetFullUri(requestUri, uriQueryParams)))
                return JsonConvert.DeserializeObject<TResult>(await GetResponseAsync(request));
        }

        protected async Task SendRequestAsync(
            HttpMethod method,
            string requestUri,
            Dictionary<string, object> uriQueryParams = null)
        {
            using (var request = new HttpRequestMessage(method, GetFullUri(requestUri, uriQueryParams)))
                await GetResponseAsync(request);
        }

        protected async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string requestUri,
            object contentDto,
            Dictionary<string, object> uriQueryParams = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(contentDto)))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var request = new HttpRequestMessage(method, GetFullUri(requestUri, uriQueryParams)))
                {
                    request.Content = content;
                    return JsonConvert.DeserializeObject<TResult>(await GetResponseAsync(request));
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
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                using (var request = new HttpRequestMessage(method, GetFullUri(requestUri, uriQueryParams)))
                {
                    request.Content = content;
                    await GetResponseAsync(request);
                }
            }
        }

        private static string GetFullUri(string requestUri, Dictionary<string, object> uriQueryParams) =>
            uriQueryParams != null ? requestUri + StringifyUriQueryParams(uriQueryParams) : requestUri;

        private static string StringifyUriQueryParams(Dictionary<string, object> parameters) =>
            $"?{string.Join("&", parameters.Select(x => $"{x.Key}={x.Value.ToString()}"))}";
    }
}
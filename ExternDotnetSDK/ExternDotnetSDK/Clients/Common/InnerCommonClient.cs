using System;
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

        protected async Task<T> TryExecuteTask<T>(Task<T> task)
        {
            throw new NotImplementedException();
        }

        protected async Task TryExecuteTask(Task task)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GetResponseAsync(HttpRequestMessage message)
        {
            using (var response = await Client.SendAsync(message))
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

        protected string StringifyUriQueryParams(Dictionary<string, object> parameters) =>
            $"?{string.Join("&", parameters.Select(x => $"{x.Key}={x.Value.ToString()}"))}";

        protected async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string requestUri,
            string uriQueryParams = null)
        {
            var uri = uriQueryParams != null ? requestUri + uriQueryParams : requestUri;
            using (var request = new HttpRequestMessage(method, uri))
            {
                var result = await GetResponseAsync(request);
                return JsonConvert.DeserializeObject<TResult>(result);
            }
        }

        protected async Task SendRequestAsync(HttpMethod method, string requestUri, string uriQueryParams = null)
        {
            var uri = uriQueryParams != null ? requestUri + uriQueryParams : requestUri;
            using (var request = new HttpRequestMessage(method, uri))
                await GetResponseAsync(request);
        }

        protected async Task<TResult> SendRequestAsync<TResult>(
            HttpMethod method,
            string requestUri,
            object contentDto,
            string uriQueryParams = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(contentDto)))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var uri = uriQueryParams != null ? requestUri + uriQueryParams : requestUri;
                using (var request = new HttpRequestMessage(method, uri))
                {
                    request.Content = content;
                    var result = await GetResponseAsync(request);
                    return JsonConvert.DeserializeObject<TResult>(result);
                }
            }
        }

        protected async Task SendRequestAsync(
            HttpMethod method,
            string requestUri,
            object contentDto,
            string uriQueryParams = null)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(contentDto)))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var uri = uriQueryParams != null ? requestUri + uriQueryParams : requestUri;
                using (var request = new HttpRequestMessage(method, uri))
                {
                    request.Content = content;
                    await GetResponseAsync(request);
                }
            }
        }
    }
}
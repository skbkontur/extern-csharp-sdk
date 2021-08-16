using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Kontur.Extern.Client.Testing.Lifetimes;
using static System.Environment;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Http
{
    internal class HttpClientImplementation : IHttpClient
    {
        private readonly HttpClient httpClient;

        public HttpClientImplementation(string baseUrl, string apiKey, ILifetime lifetime)
        {
            httpClient = lifetime.Add(new HttpClient
            {
                BaseAddress = new Uri(baseUrl, UriKind.Absolute)
            });
            
            httpClient.DefaultRequestHeaders.Add("X-Kontur-Apikey", apiKey);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string relativeUrl, T request)
        {
            var responseMessage = await httpClient.PostAsJsonAsync(relativeUrl, request);
            await EnsureSuccessAsync(responseMessage);
            return responseMessage;
        }

        private static async Task EnsureSuccessAsync(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var message = $"Response status code does not indicate success: {responseMessage.StatusCode} ({responseMessage.ReasonPhrase})";
                if (responseMessage.Content.Headers.ContentLength > 0)
                {
                    var errorDetails = await responseMessage.Content.ReadAsStringAsync();
                    message = $"{message}{NewLine}{errorDetails}";
                }
                throw new HttpRequestException(message, null, responseMessage.StatusCode);
            }
        }
    }
}
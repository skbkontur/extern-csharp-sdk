using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Testing.Helpers.Json;
using Kontur.Extern.Api.Client.Testing.Lifetimes;
using Vostok.Logging.Abstractions;
using static System.Environment;

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.Http
{
    internal class HttpClientImplementation : IHttpClient
    {
        private readonly ILog log;
        private readonly HttpClient httpClient;

        public HttpClientImplementation(string baseUrl, string apiKey, ILifetime lifetime, ILog log)
        {
            this.log = log;
            httpClient = lifetime.Add(new HttpClient
            {
                BaseAddress = new Uri(baseUrl, UriKind.Absolute)
            });
            
            httpClient.DefaultRequestHeaders.Add("X-Kontur-Apikey", apiKey);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string relativeUrl, T request)
        {
            var jsonContent = JsonContent.Create(request);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, relativeUrl);
            requestMessage.Content = jsonContent;

            await DumpRequest(HttpMethod.Post, relativeUrl, jsonContent);
            
            var responseMessage = await httpClient.SendAsync(requestMessage);
            await EnsureSuccessAsync(responseMessage);
            
            await DumpResponse(responseMessage);
            return responseMessage;
        }

        private async Task DumpResponse(HttpResponseMessage responseMessage)
        {
            var builder = new StringBuilder();

            builder.Append("TestTool response: ").Append((int) responseMessage.StatusCode).Append(" ").Append(responseMessage.StatusCode).AppendLine();
            foreach (var (name, values) in responseMessage.Headers)
            {
                builder.Append(name).Append(": ").Append(string.Join(", ", values)).AppendLine();
            }

            if (responseMessage.Content.Headers.ContentLength > 0 && responseMessage.Content.Headers.ContentType?.MediaType == "application/json")
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                builder.AppendLine(json.EllipsisLongStringValuesInJson());
            }

            log.Debug(builder.ToString());
        }

        private async Task DumpRequest(HttpMethod httpMethod, string relativeUrl, JsonContent jsonContent)
        {
            var dump = new StringBuilder();
            dump.AppendLine($"TestTool request: {httpMethod} {relativeUrl}");
            var json = await jsonContent.ReadAsStringAsync();
            dump.AppendLine(json.EllipsisLongStringValuesInJson());
            log.Debug(dump.ToString());
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
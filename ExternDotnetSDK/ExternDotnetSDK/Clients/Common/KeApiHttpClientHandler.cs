using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ExternDotnetSDK.Clients.Common
{
    public class KeApiHttpClientHandler : HttpClientHandler
    {
        private readonly string apiKey;
        private readonly string authSid;
        private readonly HttpClient httpClient;

        public KeApiHttpClientHandler(string apiKey, string authSid, string baseAddress)
        {
            this.apiKey = apiKey;
            this.authSid = authSid;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var myRequest = new HttpRequestMessage(request.Method, request.RequestUri)
            {
                Content = request.Content,
                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("auth.sid", authSid),
                }
            };
            myRequest.Headers.Add("X-Kontur-Apikey", apiKey);
            var response = await httpClient.SendAsync(myRequest, cancellationToken);
            var message = new HttpResponseMessage(response.StatusCode)
            {
                Content = new StringContent(await response.Content.ReadAsStringAsync()),
                RequestMessage = myRequest,
                Version = new Version(1, 1)
            };
            foreach (var responseHeader in response.Headers.Where(x => x.Key.StartsWith("X")))
                message.Headers.Add(responseHeader.Key, responseHeader.Value);
            return message;
        }
    }
}
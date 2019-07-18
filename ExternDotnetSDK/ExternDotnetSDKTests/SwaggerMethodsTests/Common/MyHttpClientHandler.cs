using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Common
{
    class MyHttpClientHandler : HttpClientHandler
    {
        private readonly string apiKey;
        private readonly string authSid;
        private readonly HttpClient httpClient;

        public MyHttpClientHandler(string apiKey, string authSid, string baseAddress)
        {
            this.apiKey = apiKey;
            this.authSid = authSid;
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var myRequest = new HttpRequestMessage(request.Method, request.RequestUri)
            {
                Content = request.Content
            };
            foreach (var header in request.Headers)
                myRequest.Headers.Add(header.Key, header.Value);
            myRequest.Headers.Authorization = new AuthenticationHeaderValue("auth.sid", authSid);
            myRequest.Headers.Add("X-Kontur-Apikey", apiKey);

            var response = await httpClient.SendAsync(myRequest, cancellationToken);
            var content = await response.Content.ReadAsStringAsync();
            var message = new HttpResponseMessage(response.StatusCode)
            {
                Content = new StringContent(content),
                RequestMessage = myRequest,
                Version = new Version(1, 1)
            };

            foreach (var responseHeader in response.Headers)
                if (responseHeader.Key.StartsWith("X"))
                    message.Headers.Add(responseHeader.Key, responseHeader.Value);

            return message;
        }
    }
}

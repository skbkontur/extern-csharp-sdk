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

        public KeApiHttpClientHandler(string apiKey, string authSid)
        {
            this.apiKey = apiKey;
            this.authSid = authSid;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("auth.sid", authSid);
            request.Headers.Add("X-Kontur-Apikey", apiKey);
            var response = await base.SendAsync(request, cancellationToken);
            response.Content = new StringContent(await response.Content.ReadAsStringAsync());
            foreach (var responseHeader in response.Headers.Where(x => x.Key.StartsWith("X", StringComparison.Ordinal)))
                response.Headers.Add(responseHeader.Key, responseHeader.Value);
            return response;
        }
    }
}
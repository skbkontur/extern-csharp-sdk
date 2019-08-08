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
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;

namespace ExternDotnetSDK.Clients.Common.DefaultImplementations
{
    public class DefaultRequestSender : IRequestSender
    {
        private readonly HttpClient client;

        public DefaultRequestSender(HttpClient client) => this.client = client;

        public async Task<IHaveHttpResponseMessage> SendAsync(IHaveHttpRequestMessage request)
        {
            var requestMessage = new HttpRequestMessage
            {
                Version = new Version(1, 1),
                Content = request.Content,
                Method = request.Method,
                RequestUri = request.Uri,
                Headers = {Authorization = request.Headers.Authorization}
            };
            foreach (var header in request.Headers.Where(x => x.Key != "Authorization"))
                requestMessage.Headers.Add(header.Key, header.Value);
            return new DefaultResponse(await client.SendAsync(requestMessage));
        }
    }
}
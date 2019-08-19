using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common.ResponseMessage;

namespace ExternDotnetSDK.Clients.Common.SendAsync
{
    public class DefaultSender : ISendAsync
    {
        private readonly HttpClient client;

        public DefaultSender(HttpClient client) => this.client = client;

        public async Task<IHaveHttpResponseMessage> SendAsync(HttpRequestMessage request, params object[] extraParams) =>
            new DefaultResponse(await client.SendAsync(request));
    }
}
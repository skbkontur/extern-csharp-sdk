using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace ExternDotnetSDK.Clients.TestApi
{
    public class TestApiClient
    {
        private readonly ITestApiClientRefit clientRefit;

        public TestApiClient(HttpClient client)
        {
            clientRefit = RestService.For<ITestApiClientRefit>(client);
        }

        public async Task PokeAsync(Guid accountId, Guid docflowId, uint times = 1)
        {
            await clientRefit.PokeAsync(accountId, docflowId, times);
        }
    }
}
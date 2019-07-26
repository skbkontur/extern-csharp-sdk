using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Test;
using Refit;

namespace ExternDotnetSDK.Clients.TestUsers
{
    public class TestUsersClient
    {
        private readonly ITestUsersClientRefit clientRefit;

        public TestUsersClient(HttpClient client) => 
            clientRefit = RestService.For<ITestUsersClientRefit>(client);

        public async Task<CreateTestUsersResponseDto> CreateTestUserAsync(
            CreateTestUsersRequestDto request) =>
            await clientRefit.GenerateTestUserAsync(request);
    }
}
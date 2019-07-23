using System.Threading.Tasks;
using ExternDotnetSDK.Test;
using Refit;

namespace ExternDotnetSDK.Clients.TestUsers
{
    internal interface ITestUsersClientRefit
    {
        [Post("/v1/generate-test-user")]
        Task<CreateTestUsersResponseDto> GenerateTestUser([Body] CreateTestUsersRequestDto request);
    }
}
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Common
{
    public interface IAuthClientRefit
    {
        [Post("/auth/v5.13/authenticate-by-pass")]
        Task<SessionResponse> ByPass(string login, [Body] string password, [AliasAs("api-key")] string apiKey = null);
    }
}
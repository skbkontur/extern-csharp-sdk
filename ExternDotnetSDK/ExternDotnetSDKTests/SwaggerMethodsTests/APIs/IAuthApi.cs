using System.Threading.Tasks;
using ExternDotnetSDKTests.SwaggerMethodsTests.Common;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.APIs
{
    public interface IAuthApi
    {
        [Post("/auth/v5.13/authenticate-by-pass")]
        Task<SessionResponse> ByPass(string login, [Body] string password, [AliasAs("api-key")] string apiKey = null);
    }
}

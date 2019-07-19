using System.Threading.Tasks;
using ExternDotnetSDK.Accounts;
using ExternDotnetSDK.Common;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.APIs
{
    internal interface IAccountApi
    {
        [Get("/")]
        Task<Link[]> GetRootIndex();

        [Get("/v1?skip={skip}&take={take}")]
        Task<AccountList> GetAccounts(int skip = 0, int take = int.MaxValue);

        [Post("/v1")]
        Task<Account> CreateAccount([Body] CreateAccountRequestDto request);

        [Get("/v1/{accountId}")]
        Task<Account> GetAccount(string accountId);

        [Delete("/v1/{accountId}")]
        Task DeleteAccount(string accountId);
    }
}
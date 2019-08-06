using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Accounts;
using Refit;

namespace ExternDotnetSDK.Clients.Account
{
    public interface IAccountClientRefit
    {
        [Get("/v1?skip={skip}&take={take}")]
        Task<AccountList> GetAccountsAsync(int skip = 0, int take = int.MaxValue);

        [Post("/v1")]
        Task<Models.Accounts.Account> CreateAccountAsync([Body] CreateAccountRequestDto request);

        [Get("/v1/{accountId}")]
        Task<Models.Accounts.Account> GetAccountAsync(Guid accountId);

        [Delete("/v1/{accountId}")]
        Task DeleteAccountAsync(Guid accountId);
    }
}
using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Accounts;
using Refit;

namespace ExternDotnetSDK.Clients.Account
{
    public interface IAccountClientRefit
    {
        [Get("/v1?skip={skip}&take={take}")]
        Task<AccountList> GetAccounts(int skip = 0, int take = int.MaxValue);

        [Post("/v1")]
        Task<Accounts.Account> CreateAccount([Body] CreateAccountRequestDto request);

        [Get("/v1/{accountId}")]
        Task<Accounts.Account> GetAccount(Guid accountId);

        [Delete("/v1/{accountId}")]
        Task DeleteAccount(Guid accountId);
    }
}
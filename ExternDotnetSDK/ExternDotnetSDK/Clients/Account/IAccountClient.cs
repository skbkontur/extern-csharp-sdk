using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Models.Accounts;

namespace ExternDotnetSDK.Clients.Account
{
    public interface IAccountClient : IHttpClient
    {
        Task<AccountList> GetAccountsAsync(int skip = 0, int take = int.MaxValue);
        Task<Models.Accounts.Account> GetAccountAsync(Guid accountId);
        Task DeleteAccountAsync(Guid accountId);
        Task<Models.Accounts.Account> CreateAccountAsync(string inn, string kpp, string organizationName);
    }
}
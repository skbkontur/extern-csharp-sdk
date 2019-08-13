using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Accounts;

namespace ExternDotnetSDK.Clients.Account
{
    public interface IAccountClient
    {
        IAccountClientRefit ClientRefit { get; }

        Task<AccountList> GetAccountsAsync(int skip = 0, int take = int.MaxValue);
        Task<Models.Accounts.Account> GetAccountAsync(Guid accountId);
        Task DeleteAccountAsync(Guid accountId);
        Task<Models.Accounts.Account> CreateAccountAsync(string inn, string kpp, string organizationName);
    }
}
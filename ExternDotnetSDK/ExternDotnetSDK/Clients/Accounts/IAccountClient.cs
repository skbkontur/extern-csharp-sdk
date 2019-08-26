using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Accounts;
using ExternDotnetSDK.Models.Certificates;
using ExternDotnetSDK.Models.Warrants;

namespace ExternDotnetSDK.Clients.Accounts
{
    public interface IAccountClient
    {
        Task<AccountList> GetAccountsAsync(int skip = 0, int take = int.MaxValue);
        Task<Account> GetAccountAsync(Guid accountId);
        Task DeleteAccountAsync(Guid accountId);
        Task<Account> CreateAccountAsync(string inn, string kpp, string organizationName);
        Task<CertificateList> GetAccountCertificatesAsync(Guid accountId, int skip = 0, int take = 100, bool forAllUsers = false);

        Task<WarrantList> GetAccountWarrantsAsync(
            Guid accountId,
            int skip = 0,
            int take = int.MaxValue,
            bool forAllUsers = false);
    }
}
using System;
using System.Threading.Tasks;
using KeApiClientOpenSdk.Models.Accounts;
using KeApiClientOpenSdk.Models.Certificates;
using KeApiClientOpenSdk.Models.Warrants;

namespace KeApiClientOpenSdk.Clients.Accounts
{
    public interface IAccountClient
    {
        Task<AccountList> GetAccountsAsync(int skip = 0, int take = int.MaxValue, TimeSpan? timeout = null);
        Task<Account> GetAccountAsync(Guid accountId, TimeSpan? timeout = null);
        Task DeleteAccountAsync(Guid accountId, TimeSpan? timeout = null);
        Task<Account> CreateAccountAsync(string inn, string kpp, string organizationName, TimeSpan? timeout = null);

        Task<CertificateList> GetAccountCertificatesAsync(
            Guid accountId,
            int skip = 0,
            int take = 100,
            bool forAllUsers = false,
            TimeSpan? timeout = null);

        Task<WarrantList> GetAccountWarrantsAsync(
            Guid accountId,
            int skip = 0,
            int take = int.MaxValue,
            bool forAllUsers = false,
            TimeSpan? timeout = null);
    }
}
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Accounts;
using Refit;

namespace ExternDotnetSDK.Clients.Account
{
    public class AccountClient : IAccountClient
    {
        public IAccountClientRefit ClientRefit { get; }

        public AccountClient(HttpClient client) => ClientRefit = RestService.For<IAccountClientRefit>(client);

        public async Task<AccountList> GetAccountsAsync(int skip = 0, int take = int.MaxValue) =>
            await ClientRefit.GetAccountsAsync(skip, take);

        public async Task<Models.Accounts.Account> GetAccountAsync(Guid accountId) =>
            await ClientRefit.GetAccountAsync(accountId);

        public async Task DeleteAccountAsync(Guid accountId) =>
            await ClientRefit.DeleteAccountAsync(accountId);

        public async Task<Models.Accounts.Account> CreateAccountAsync(string inn, string kpp, string organizationName)
        {
            var request = new CreateAccountRequestDto
            {
                Inn = inn,
                Kpp = kpp,
                OrganizationName = organizationName
            };
            return await ClientRefit.CreateAccountAsync(request);
        }
    }
}
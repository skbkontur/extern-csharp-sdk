using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Accounts;
using Refit;

namespace ExternDotnetSDK.Clients.Account
{
    public class AccountClient
    {
        private readonly IAccountClientRefit clientRefit;

        public AccountClient(HttpClient client) => clientRefit = RestService.For<IAccountClientRefit>(client);

        public async Task<AccountList> GetAccountsAsync(int skip = 0, int take = int.MaxValue) 
            => await clientRefit.GetAccounts(skip, take);

        public async Task<Accounts.Account> GetAccountAsync(Guid accountId) 
            => await clientRefit.GetAccount(accountId);

        public async Task DeleteAccountAsync(Guid accountId) 
            => await clientRefit.DeleteAccount(accountId);

        public async Task<Accounts.Account> CreateAccountAsync(string inn, string kpp, string organizationName)
        {
            var request = new CreateAccountRequestDto
            {
                Inn = inn, Kpp = kpp, OrganizationName = organizationName
            };
            return await clientRefit.CreateAccount(request);
        }
    }
}
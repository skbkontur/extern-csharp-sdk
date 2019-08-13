using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Accounts;
using Refit;

namespace ExternDotnetSDK.Clients.Account
{
    public class AccountClient : InnerCommonClient, IAccountClient
    {
        public AccountClient(ILog log, HttpClient client)
            : base(log) =>
            ClientRefit = RestService.For<IAccountClientRefit>(client);

        public IAccountClientRefit ClientRefit { get; }

        public async Task<AccountList> GetAccountsAsync(int skip = 0, int take = int.MaxValue) =>
            await TryExecuteTask(ClientRefit.GetAccountsAsync(skip, take));

        public async Task<Models.Accounts.Account> GetAccountAsync(Guid accountId) =>
            await TryExecuteTask(ClientRefit.GetAccountAsync(accountId));

        public async Task DeleteAccountAsync(Guid accountId) =>
            await TryExecuteTask(ClientRefit.DeleteAccountAsync(accountId));

        public async Task<Models.Accounts.Account> CreateAccountAsync(string inn, string kpp, string organizationName) =>
            await TryExecuteTask(ClientRefit.CreateAccountAsync(
                new CreateAccountRequestDto
                {
                    Inn = inn,
                    Kpp = kpp,
                    OrganizationName = organizationName
                }));
    }
}
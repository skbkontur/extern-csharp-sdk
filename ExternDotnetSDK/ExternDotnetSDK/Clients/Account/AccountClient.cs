using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Accounts;

namespace ExternDotnetSDK.Clients.Account
{
    public class AccountClient : InnerCommonClient, IAccountClient
    {
        public AccountClient(ILogError logError, HttpClient client)
            : base(logError, client)
        {
        }

        public async Task<AccountList> GetAccountsAsync(int skip = 0, int take = int.MaxValue) =>
            await SendRequestAsync<AccountList>(
                HttpMethod.Get,
                "/v1",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take
                });

        public async Task<Models.Accounts.Account> GetAccountAsync(Guid accountId) =>
            await SendRequestAsync<Models.Accounts.Account>(HttpMethod.Get, $"/v1/{accountId}");

        public async Task DeleteAccountAsync(Guid accountId) =>
            await SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}");

        public async Task<Models.Accounts.Account> CreateAccountAsync(string inn, string kpp, string organizationName) =>
            await SendRequestAsync<Models.Accounts.Account>(
                HttpMethod.Post,
                "/v1",
                new CreateAccountRequestDto
                {
                    Inn = inn,
                    Kpp = kpp,
                    OrganizationName = organizationName
                });
    }
}
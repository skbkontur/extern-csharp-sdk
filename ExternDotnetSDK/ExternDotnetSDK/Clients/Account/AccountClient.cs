using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.SendAsync;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Accounts;
using ExternDotnetSDK.Models.Certificates;
using ExternDotnetSDK.Models.Warrants;

namespace ExternDotnetSDK.Clients.Account
{
    public class AccountClient : InnerCommonClient, IAccountClient
    {
        public AccountClient(ILogError logError, ISendAsync client, IAuthenticationProvider authenticationProvider)
            : base(logError, client, authenticationProvider)
        {
        }

        public async Task<AccountList> GetAccountsAsync(int skip = 0, int take = 100) =>
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

        public async Task DeleteAccountAsync(Guid accountId) => await SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}");

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

        public async Task<CertificateList> GetAccountCertificatesAsync(
            Guid accountId,
            int skip = 0,
            int take = 100,
            bool forAllUsers = false) =>
            await SendRequestAsync<CertificateList>(
                HttpMethod.Get,
                $"/v1/{accountId}/certificates",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take,
                    ["forAllUsers"] = forAllUsers
                });

        public async Task<WarrantList> GetAccountWarrantsAsync(
            Guid accountId,
            int skip = 0,
            int take = int.MaxValue,
            bool forAllUsers = false) =>
            await SendRequestAsync<WarrantList>(
                HttpMethod.Get,
                $"/v1/{accountId}/warrants",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take,
                    ["forAllUsers"] = forAllUsers
                });
    }
}
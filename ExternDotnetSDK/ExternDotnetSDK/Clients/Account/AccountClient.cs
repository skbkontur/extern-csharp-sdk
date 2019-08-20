using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Authentication;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.SendAsync;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Accounts;
using ExternDotnetSDK.Models.Certificates;
using ExternDotnetSDK.Models.Warrants;

namespace ExternDotnetSDK.Clients.Account
{
    public class AccountClient : IAccountClient
    {
        private readonly RequestFactory factory;

        public AccountClient(ILogger logger, IHttpSender client, IAuthenticationProvider authenticationProvider) =>
            factory = new RequestFactory(logger, client, authenticationProvider);

        public async Task<AccountList> GetAccountsAsync(int skip = 0, int take = 100) =>
            await factory.SendRequestAsync<AccountList>(
                HttpMethod.Get,
                "/v1",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take
                });

        public async Task<Models.Accounts.Account> GetAccountAsync(Guid accountId) =>
            await factory.SendRequestAsync<Models.Accounts.Account>(HttpMethod.Get, $"/v1/{accountId}");

        public async Task DeleteAccountAsync(Guid accountId) =>
            await factory.SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}");

        public async Task<Models.Accounts.Account> CreateAccountAsync(string inn, string kpp, string organizationName) =>
            await factory.SendRequestAsync<Models.Accounts.Account>(
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
            await factory.SendRequestAsync<CertificateList>(
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
            await factory.SendRequestAsync<WarrantList>(
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
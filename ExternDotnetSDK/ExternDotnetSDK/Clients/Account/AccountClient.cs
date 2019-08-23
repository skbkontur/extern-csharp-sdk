using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces.Logging;
using ExternDotnetSDK.Models.Accounts;
using ExternDotnetSDK.Models.Certificates;
using ExternDotnetSDK.Models.Warrants;

namespace ExternDotnetSDK.Clients.Account
{
    public class AccountClient : IAccountClient
    {
        private readonly InnerCommonClient client;

        public AccountClient(ILogger logger, IRequestSender sender, IRequestFactory requestFactory) =>
            client = new InnerCommonClient(logger, sender, requestFactory);

        public async Task<AccountList> GetAccountsAsync(int skip = 0, int take = 100) =>
            await client.SendRequestAsync<AccountList>(
                HttpMethod.Get,
                "/v1",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take
                });

        public async Task<Models.Accounts.Account> GetAccountAsync(Guid accountId) =>
            await client.SendRequestAsync<Models.Accounts.Account>(HttpMethod.Get, $"/v1/{accountId}");

        public async Task DeleteAccountAsync(Guid accountId) =>
            await client.SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}");

        public async Task<Models.Accounts.Account> CreateAccountAsync(string inn, string kpp, string organizationName) =>
            await client.SendRequestAsync<Models.Accounts.Account>(
                HttpMethod.Post,
                "/v1",
                contentDto: new CreateAccountRequestDto
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
            await client.SendRequestAsync<CertificateList>(
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
            await client.SendRequestAsync<WarrantList>(
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
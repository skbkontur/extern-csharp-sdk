using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KeApiClientOpenSdk.Clients.Common;
using KeApiClientOpenSdk.Clients.Common.Logging;
using KeApiClientOpenSdk.Clients.Common.RequestSenders;
using KeApiClientOpenSdk.Models.Accounts;
using KeApiClientOpenSdk.Models.Certificates;
using KeApiClientOpenSdk.Models.Warrants;

namespace KeApiClientOpenSdk.Clients.Accounts
{
    //todo Сделать нормальные тесты для методов.
    public class AccountClient : IAccountClient
    {
        private readonly InnerCommonClient client;

        public AccountClient(ILogger logger, IRequestSender requestSender) =>
            client = new InnerCommonClient(logger, requestSender);

        public async Task<AccountList> GetAccountsAsync(int skip = 0, int take = 100, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<AccountList>(
                HttpMethod.Get,
                "/v1",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take
                },
                timeout: timeout);

        public async Task<Account> GetAccountAsync(Guid accountId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Account>(HttpMethod.Get, $"/v1/{accountId}", timeout: timeout);

        public async Task DeleteAccountAsync(Guid accountId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}", timeout: timeout);

        public async Task<Account> CreateAccountAsync(
            string inn,
            string kpp,
            string organizationName,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Account>(
                HttpMethod.Post,
                "/v1",
                contentDto: new CreateAccountRequestDto
                {
                    Inn = inn,
                    Kpp = kpp,
                    OrganizationName = organizationName
                },
                timeout: timeout);

        public async Task<CertificateList> GetAccountCertificatesAsync(
            Guid accountId,
            int skip = 0,
            int take = 100,
            bool forAllUsers = false,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<CertificateList>(
                HttpMethod.Get,
                $"/v1/{accountId}/certificates",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take,
                    ["forAllUsers"] = forAllUsers
                },
                timeout: timeout);

        public async Task<WarrantList> GetAccountWarrantsAsync(
            Guid accountId,
            int skip = 0,
            int take = int.MaxValue,
            bool forAllUsers = false,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<WarrantList>(
                HttpMethod.Get,
                $"/v1/{accountId}/warrants",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take,
                    ["forAllUsers"] = forAllUsers
                },
                timeout: timeout);
    }
}
using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Accounts;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Accounts;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Certificates;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Warrants;
using Kontur.Extern.Api.Client.Models.Accounts;
using Kontur.Extern.Api.Client.Http;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Accounts
{
    public class AccountClient : IAccountClient
    {
        private readonly IHttpRequestsFactory http;

        public AccountClient(IHttpRequestsFactory http) => this.http = http;

        public Task<AccountList> GetAccountsAsync(int? skip = null, int? take = null, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder("v1")
                .AppendToQuery("skip", skip)
                .AppendToQuery("take", take)
                .Build();
            return http.GetAsync<AccountList>(url, timeout);
        }

        public Task<Account> GetAccountAsync(Guid accountId, TimeSpan? timeout = null) => 
            http.GetAsync<Account>($"v1/{accountId}", timeout);

        public Task<Account?> TryGetAccountAsync(Guid accountId, TimeSpan? timeout = null) => 
            http.TryGetAsync<Account>($"v1/{accountId}", timeout);

        public Task<bool> DeleteAccountAsync(Guid accountId, TimeSpan? timeout = null) => 
            http.TryDeleteAsync($"v1/{accountId}", timeout);

        public Task<Account> CreateAccountAsync(
            string inn,
            string? kpp,
            string organizationName,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<CreateAccountRequest, Account>(
                "v1",
                new CreateAccountRequest
                {
                    Inn = inn,
                    Kpp = kpp,
                    OrganizationName = organizationName
                },
                timeout
            );
        }

        public Task<CertificateList> GetAccountCertificatesAsync(
            Guid accountId,
            int? skip = null,
            int? take = null,
            bool? forAllUsers = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/certificates")
                .AppendToQuery("skip", skip)
                .AppendToQuery("take", take)
                .AppendToQuery("forAllUsers", forAllUsers)
                .Build();
            return http.GetAsync<CertificateList>(url, timeout);
        }

        public Task<WarrantList> GetAccountWarrantsAsync(
            Guid accountId,
            int? skip = null,
            int? take = null,
            bool? forAllUsers = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/warrants")
                .AppendToQuery("skip", skip)
                .AppendToQuery("take", take)
                .AppendToQuery("forAllUsers", forAllUsers)
                .Build();
            return http.GetAsync<WarrantList>(url, timeout);
        }
    }
}
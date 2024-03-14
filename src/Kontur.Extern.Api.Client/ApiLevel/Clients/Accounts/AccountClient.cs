using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Accounts;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Accounts;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Certificates;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Warrants;
using Kontur.Extern.Api.Client.Models.Accounts;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Models.Numbers;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Accounts
{
    public class AccountClient : IAccountClient
    {
        public IHttpRequestFactory HttpRequestFactory { get; }

        public AccountClient(IHttpRequestFactory http) => HttpRequestFactory = http;

        public Task<AccountList> GetAccountsAsync(int? skip = null, int? take = null, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder("v1")
                .AppendToQuery("skip", skip)
                .AppendToQuery("take", take)
                .Build();
            return HttpRequestFactory.GetAsync<AccountList>(url, timeout);
        }

        public Task<Account> GetAccountAsync(Guid accountId, TimeSpan? timeout = null) =>
            HttpRequestFactory.GetAsync<Account>($"v1/{accountId}", timeout);

        public Task<Account?> TryGetAccountAsync(Guid accountId, TimeSpan? timeout = null) =>
            HttpRequestFactory.TryGetAsync<Account>($"v1/{accountId}", timeout);

        public Task<bool> DeleteAccountAsync(Guid accountId, TimeSpan? timeout = null) =>
            HttpRequestFactory.TryDeleteAsync($"v1/{accountId}", timeout);

        public Task<Account> CreateAccountAsync(
            string inn,
            Kpp? kpp,
            string organizationName,
            TimeSpan? timeout = null)
        {
            return HttpRequestFactory.PostAsync<CreateAccountRequest, Account>(
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
            return HttpRequestFactory.GetAsync<CertificateList>(url, timeout);
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
            return HttpRequestFactory.GetAsync<WarrantList>(url, timeout);
        }
    }
}
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Models.Accounts;
using Kontur.Extern.Client.Models.Certificates;
using Kontur.Extern.Client.Models.Warrants;

namespace Kontur.Extern.Client.Clients.Accounts
{
    public class AccountClient : IAccountClient
    {
        private readonly InnerCommonClient client;
        private readonly IRequestBodySerializer requestBodySerializer;

        public AccountClient(ILogger logger, _IRequestSender requestSender, IRequestBodySerializer requestBodySerializer)
        {
            this.requestBodySerializer = requestBodySerializer;
            client = new InnerCommonClient(logger, requestSender);
        }

        public Task<AccountList> GetAccountsAsync(int? skip = null, int? take = null, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder("v1")
                .AppendToQuery("skip", skip)
                .AppendToQuery("take", take)
                .Build();
            var request = Request.Get(url);
            return client.SendJsonRequestAsync<AccountList>(request, timeout);
        }

        public Task<Account> GetAccountAsync(Guid accountId, TimeSpan? timeout = null)
        {
            var request = Request.Get($"v1/{accountId}");
            return client.SendJsonRequestAsync<Account>(request, timeout);
        }

        public Task DeleteAccountAsync(Guid accountId, TimeSpan? timeout = null)
        {
            var request = Request.Delete($"v1/{accountId}");
            return client.SendJsonRequestAsync(request, timeout);
        }

        public Task<Account> CreateAccountAsync(
            string inn,
            string kpp,
            string organizationName,
            TimeSpan? timeout = null)
        {
            var requestDto = new CreateAccountRequestDto
            {
                Inn = inn,
                Kpp = kpp,
                OrganizationName = organizationName
            };
            var request = Request.Post("v1")
                .WithContent(requestBodySerializer.SerializeToJson(requestDto));
            return client.SendJsonRequestAsync<Account>(request, timeout);
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
            var request = Request.Get(url);
            return client.SendJsonRequestAsync<CertificateList>(request, timeout);
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
            var request = Request.Get(url);
            return client.SendJsonRequestAsync<WarrantList>(request, timeout);
        }
    }
}
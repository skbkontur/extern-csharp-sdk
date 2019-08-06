using System;
using System.Net.Http;
using ExternDotnetSDK.Clients.Account;
using ExternDotnetSDK.Clients.Authentication;
using ExternDotnetSDK.Clients.Certificates;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Docflows;
using ExternDotnetSDK.Clients.Drafts;
using ExternDotnetSDK.Clients.Events;
using ExternDotnetSDK.Clients.Organizations;
using ExternDotnetSDK.Clients.Warrants;

namespace ExternDotnetSDK
{
    public class KeApiClient
    {
        private readonly IAuthClient authClient;
        public IAccountClient AccountClient;
        public ICertificateClient CertificateClient;
        public IDocflowsClient DocflowsClient;
        public IDraftClient DraftClient;
        public IEventsClient EventsClient;
        public IOrganizationsClient OrganizationsClient;
        public IWarrantsClient WarrantsClient;

        public KeApiClient(string baseAddress, string login, string password, string apiKey = null)
        {
            authClient = new AuthClient("https://api.testkontur.ru");
            var sessionResponse = authClient.ByPass(login, password, apiKey).Result;
            var client = new HttpClient(new KeApiHttpClientHandler(apiKey, sessionResponse.Sid, baseAddress))
            {
                BaseAddress = new Uri(baseAddress)
            };
            InitializeAllClients(client);
        }

        private void InitializeAllClients(HttpClient client)
        {
            AccountClient = new AccountClient(client);
            CertificateClient = new CertificateClient(client);
            DocflowsClient = new DocflowsClient(client);
            DraftClient = new DraftClient(client);
            EventsClient = new EventsClient(client);
            OrganizationsClient = new OrganizationsClient(client);
            WarrantsClient = new WarrantsClient(client);
        }
    }
}
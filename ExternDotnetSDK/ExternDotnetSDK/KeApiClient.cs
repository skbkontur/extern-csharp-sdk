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
using ExternDotnetSDK.Clients.RelatedDocflows;
using ExternDotnetSDK.Clients.Warrants;

namespace ExternDotnetSDK
{
    public class KeApiClient
    {
        public IAuthClient Auth;
        public IAccountClient Accounts;
        public ICertificateClient Certificates;
        public IDocflowsClient Docflows;
        public IDraftClient Drafts;
        public IEventsClient Events;
        public IOrganizationsClient Organizations;
        public IWarrantsClient Warrants;
        public IRelatedDocflowsClient RelatedDocflows;

        public KeApiClient(string authAddress, string baseAddress, string login, string password, string apiKey = null)
        {
            Auth = new AuthClient(authAddress);
            var sessionResponse = Auth.ByPass(login, password, apiKey).Result;
            var client = new HttpClient(new KeApiHttpClientHandler(apiKey, sessionResponse.Sid))
            {
                BaseAddress = new Uri(baseAddress)
            };
            InitializeAllClientInterfaces(client);
        }

        private void InitializeAllClientInterfaces(HttpClient client)
        {
            Accounts = new AccountClient(client);
            Certificates = new CertificateClient(client);
            Docflows = new DocflowsClient(client);
            Drafts = new DraftClient(client);
            Events = new EventsClient(client);
            Organizations = new OrganizationsClient(client);
            Warrants = new WarrantsClient(client);
            RelatedDocflows = new RelatedDocflowsClient(client);
        }
    }
}
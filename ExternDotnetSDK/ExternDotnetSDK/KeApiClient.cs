using System;
using System.Net.Http;
using ExternDotnetSDK.Clients.Account;
using ExternDotnetSDK.Clients.Authentication;
using ExternDotnetSDK.Clients.Certificates;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Docflows;
using ExternDotnetSDK.Clients.Drafts;
using ExternDotnetSDK.Clients.DraftsBuilders;
using ExternDotnetSDK.Clients.Events;
using ExternDotnetSDK.Clients.InventoryDocflows;
using ExternDotnetSDK.Clients.Organizations;
using ExternDotnetSDK.Clients.RelatedDocflows;
using ExternDotnetSDK.Clients.Warrants;
using ExternDotnetSDK.Logging;

namespace ExternDotnetSDK
{
    public class KeApiClient
    {
        public IAccountClient Accounts;
        public ICertificateClient Certificates;
        public IDocflowsClient Docflows;
        public IDraftClient Drafts;
        public IDraftsBuilderClient DraftsBuilder;
        public IEventsClient Events;
        public IInventoryDocflowsClient InventoryDocflows;
        public IOrganizationsClient Organizations;
        public IWarrantsClient Warrants;
        public IRelatedDocflowsClient RelatedDocflows;

        public KeApiClient(ILog iLog, string baseAddress, IAuthenticationProvider authProvider)
        {
            var client = new HttpClient(new KeApiHttpClientHandler(authProvider.GetApiKey(), authProvider.GetSessionId()))
            {
                BaseAddress = new Uri(baseAddress)
            };
            InitializeAllClients(iLog, client);
        }

        private void InitializeAllClients(ILog iLog, HttpClient client)
        {
            Accounts = new AccountClient(iLog, client);
            Certificates = new CertificateClient(iLog, client);
            Docflows = new DocflowsClient(iLog, client);
            Drafts = new DraftClient(iLog, client);
            DraftsBuilder = new DraftsBuilderClient(iLog, client);
            Events = new EventsClient(iLog, client);
            InventoryDocflows = new InventoryDocflowsClient(iLog, client);
            Organizations = new OrganizationsClient(iLog, client);
            Warrants = new WarrantsClient(iLog, client);
            RelatedDocflows = new RelatedDocflowsClient(iLog, client);
        }
    }
}
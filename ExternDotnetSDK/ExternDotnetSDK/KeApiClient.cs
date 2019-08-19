using System.Net.Http;
using ExternDotnetSDK.Clients.Account;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Docflows;
using ExternDotnetSDK.Clients.Drafts;
using ExternDotnetSDK.Clients.DraftsBuilders;
using ExternDotnetSDK.Clients.Events;
using ExternDotnetSDK.Clients.InventoryDocflows;
using ExternDotnetSDK.Clients.Organizations;
using ExternDotnetSDK.Logging;

namespace ExternDotnetSDK
{
    public class KeApiClient : IKeApiClient
    {
        private readonly ILogError iLog;
        private readonly HttpClient client;
        private readonly IAuthenticationProvider authProvider;

        private IAccountClient accounts;
        private IDocflowsClient docflows;
        private IDraftClient drafts;
        private IDraftsBuilderClient draftsBuilder;
        private IEventsClient events;
        private IInventoryDocflowsClient inventoryDocflows;
        private IOrganizationsClient organizations;

        public KeApiClient(ILogError iLog, IAuthenticationProvider authProvider, HttpClient client)
        {
            this.authProvider = authProvider;
            this.iLog = iLog;
            this.client = client;
        }

        public IAccountClient Accounts => accounts ?? (accounts = new AccountClient(iLog, client, authProvider));
        public IDocflowsClient Docflows => docflows ?? (docflows = new DocflowsClient(iLog, client, authProvider));
        public IDraftClient Drafts => drafts ?? (drafts = new DraftClient(iLog, client, authProvider));
        public IEventsClient Events => events ?? (events = new EventsClient(iLog, client, authProvider));

        public IDraftsBuilderClient DraftsBuilder =>
            draftsBuilder ?? (draftsBuilder = new DraftsBuilderClient(iLog, client, authProvider));

        public IOrganizationsClient Organizations =>
            organizations ?? (organizations = new OrganizationsClient(iLog, client, authProvider));

        public IInventoryDocflowsClient InventoryDocflows =>
            inventoryDocflows ?? (inventoryDocflows = new InventoryDocflowsClient(iLog, client, authProvider));
    }
}
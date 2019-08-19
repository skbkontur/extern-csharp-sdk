using System.Net.Http;
using ExternDotnetSDK.Clients.Account;
using ExternDotnetSDK.Clients.Authentication;
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
        private readonly ILogError iLogError;
        private readonly HttpClient client;
        private readonly IAuthenticationProvider authProvider;

        private IAccountClient accounts;
        private IDocflowsClient docflows;
        private IDraftClient drafts;
        private IDraftsBuilderClient draftsBuilder;
        private IEventsClient events;
        private IInventoryDocflowsClient inventoryDocflows;
        private IOrganizationsClient organizations;

        public KeApiClient(ILogError iLogError, IAuthenticationProvider authProvider, HttpClient client)
        {
            this.authProvider = authProvider;
            this.iLogError = iLogError;
            this.client = client;
        }

        public IAccountClient Accounts => accounts ?? (accounts = new AccountClient(iLogError, client));
        public IDocflowsClient Docflows => docflows ?? (docflows = new DocflowsClient(iLogError, client));
        public IDraftClient Drafts => drafts ?? (drafts = new DraftClient(iLogError, client));
        public IEventsClient Events => events ?? (events = new EventsClient(iLogError, client));

        public IDraftsBuilderClient DraftsBuilder =>
            draftsBuilder ?? (draftsBuilder = new DraftsBuilderClient(iLogError, client));

        public IOrganizationsClient Organizations =>
            organizations ?? (organizations = new OrganizationsClient(iLogError, client));

        public IInventoryDocflowsClient InventoryDocflows =>
            inventoryDocflows ?? (inventoryDocflows = new InventoryDocflowsClient(iLogError, client));
    }
}
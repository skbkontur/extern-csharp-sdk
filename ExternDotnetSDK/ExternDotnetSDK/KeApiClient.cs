using ExternDotnetSDK.Clients.Account;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.SendAsync;
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
        private readonly ISendAsync requestSender;
        private readonly IAuthenticationProvider authProvider;

        private IAccountClient accounts;
        private IDocflowsClient docflows;
        private IDraftClient drafts;
        private IDraftsBuilderClient draftsBuilder;
        private IEventsClient events;
        private IInventoryDocflowsClient inventoryDocflows;
        private IOrganizationsClient organizations;

        public KeApiClient(ILogError iLog, IAuthenticationProvider authProvider, ISendAsync requestSender)
        {
            this.authProvider = authProvider;
            this.iLog = iLog;
            this.requestSender = requestSender;
        }

        public IAccountClient Accounts => accounts ?? (accounts = new AccountClient(iLog, requestSender, authProvider));
        public IDocflowsClient Docflows => docflows ?? (docflows = new DocflowsClient(iLog, requestSender, authProvider));
        public IDraftClient Drafts => drafts ?? (drafts = new DraftClient(iLog, requestSender, authProvider));
        public IEventsClient Events => events ?? (events = new EventsClient(iLog, requestSender, authProvider));

        public IDraftsBuilderClient DraftsBuilder =>
            draftsBuilder ?? (draftsBuilder = new DraftsBuilderClient(iLog, requestSender, authProvider));

        public IOrganizationsClient Organizations =>
            organizations ?? (organizations = new OrganizationsClient(iLog, requestSender, authProvider));

        public IInventoryDocflowsClient InventoryDocflows =>
            inventoryDocflows ?? (inventoryDocflows = new InventoryDocflowsClient(iLog, requestSender, authProvider));
    }
}
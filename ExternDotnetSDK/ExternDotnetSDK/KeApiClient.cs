using System;
using System.Net.Http;
using ExternDotnetSDK.Clients.Account;
using ExternDotnetSDK.Clients.Authentication;
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
        //todo change this address for a real one when SDK is ready for release
        private const string BaseAddress = "https://extern-api.staging2.testkontur.ru";

        private readonly ILogger iLog;
        private readonly IHttpSender requestSender;
        private readonly IAuthenticationProvider authProvider;

        private IAccountClient accounts;
        private IDocflowsClient docflows;
        private IDraftClient drafts;
        private IDraftsBuilderClient draftsBuilder;
        private IEventsClient events;
        private IInventoryDocflowsClient inventoryDocflows;
        private IOrganizationsClient organizations;

        public KeApiClient(IAuthenticationProvider authProvider, IHttpSender customSender = null, ILogger customLogger = null)
        {
            this.authProvider = authProvider;
            iLog = customLogger ?? new SilentLogger();
            requestSender = SetIHttpSender(customSender);
        }

        public KeApiClient(string apiKey, string sessionId, IHttpSender customSender = null, ILogger customLogger = null)
        {
            authProvider = new SessionAuthenticationProvider(apiKey, sessionId);
            iLog = customLogger ?? new SilentLogger();
            requestSender = SetIHttpSender(customSender);
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

        private static IHttpSender SetIHttpSender(IHttpSender requestSender) =>
            requestSender ?? new HttpSender(new HttpClient {BaseAddress = new Uri(BaseAddress)});
    }
}
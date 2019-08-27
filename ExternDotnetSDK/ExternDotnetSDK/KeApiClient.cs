using System;
using System.Net.Http;
using KeApiOpenSdk.Clients.Accounts;
using KeApiOpenSdk.Clients.Authentication;
using KeApiOpenSdk.Clients.Common.Logging;
using KeApiOpenSdk.Clients.Common.RequestSenders;
using KeApiOpenSdk.Clients.Docflows;
using KeApiOpenSdk.Clients.Drafts;
using KeApiOpenSdk.Clients.DraftsBuilders;
using KeApiOpenSdk.Clients.Events;
using KeApiOpenSdk.Clients.InventoryDocflows;
using KeApiOpenSdk.Clients.Organizations;

namespace KeApiOpenSdk
{
    public class KeApiClient : IKeApiClient
    {
        private readonly ILogger iLog;
        private readonly IRequestSender requestSender;

        public KeApiClient(
            string apiKey,
            IAuthenticationProvider authenticationProvider,
            string baseAddress,
            ILogger logger = null)
        {
            requestSender = new RequestSender(
                authenticationProvider,
                apiKey,
                new HttpClient {BaseAddress = new Uri(baseAddress)});
            iLog = logger ?? new SilentLogger();
            InitializeClients();
        }

        public KeApiClient(string apiKey, IAuthenticationProvider authenticationProvider, Uri baseAddress, ILogger logger = null)
        {
            requestSender = new RequestSender(authenticationProvider, apiKey, new HttpClient {BaseAddress = baseAddress});
            iLog = logger ?? new SilentLogger();
            InitializeClients();
        }

        public KeApiClient(IRequestSender requestSender, ILogger logger = null)
        {
            this.requestSender = requestSender;
            iLog = logger ?? new SilentLogger();
            InitializeClients();
        }

        public IAccountClient Accounts { get; private set; }
        public IDocflowsClient Docflows { get; private set; }
        public IDraftClient Drafts { get; private set; }
        public IDraftsBuilderClient DraftsBuilder { get; private set; }
        public IEventsClient Events { get; private set; }
        public IInventoryDocflowsClient InventoryDocflows { get; private set; }
        public IOrganizationsClient Organizations { get; private set; }

        private void InitializeClients()
        {
            Accounts = new AccountClient(iLog, requestSender);
            Docflows = new DocflowsClient(iLog, requestSender);
            Drafts = new DraftClient(iLog, requestSender);
            Events = new EventsClient(iLog, requestSender);
            DraftsBuilder = new DraftsBuilderClient(iLog, requestSender);
            Organizations = new OrganizationsClient(iLog, requestSender);
            InventoryDocflows = new InventoryDocflowsClient(iLog, requestSender);
        }
    }
}
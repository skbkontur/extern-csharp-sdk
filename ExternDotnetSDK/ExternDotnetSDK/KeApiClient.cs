using System;
using System.Net.Http;
using Kontur.Extern.Client.Clients.Accounts;
using Kontur.Extern.Client.Clients.Authentication;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Clients.Docflows;
using Kontur.Extern.Client.Clients.Drafts;
using Kontur.Extern.Client.Clients.DraftsBuilders;
using Kontur.Extern.Client.Clients.Events;
using Kontur.Extern.Client.Clients.InventoryDocflows;
using Kontur.Extern.Client.Clients.Organizations;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Clients.Replies;

namespace Kontur.Extern.Client
{
    public class KeApiClient : IKeApiClient
    {
        private readonly ILogger iLog;
        private readonly IRequestSender requestSender;
        private readonly IRequestBodySerializer requestBodySerializer;

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
            requestBodySerializer = new RequestBodySerializer();
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
        public IRepliesClient Replies { get; private set; }
        public IDraftClient Drafts { get; private set; }
        public IDraftsBuilderClient DraftsBuilder { get; private set; }
        public IEventsClient Events { get; private set; }
        public IInventoryDocflowsClient InventoryDocflows { get; private set; }
        public IOrganizationsClient Organizations { get; private set; }
        public ICrypt CryptoProvider { get; private set; }

        private void InitializeClients()
        {
            Accounts = new AccountClient(iLog, requestSender, requestBodySerializer);
            Docflows = new DocflowsClient(iLog, requestSender, requestBodySerializer);
            Replies = new RepliesClient(iLog, requestSender, requestBodySerializer);
            Drafts = new DraftClient(iLog, requestSender);
            Events = new EventsClient(iLog, requestSender);
            DraftsBuilder = new DraftsBuilderClient(iLog, requestSender);
            Organizations = new OrganizationsClient(iLog, requestSender);
            InventoryDocflows = new InventoryDocflowsClient(iLog, requestSender);
            CryptoProvider=new WinApiCrypt();
        }
    }
}
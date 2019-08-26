using System;
using System.Net.Http;
using ExternDotnetSDK.Clients.Accounts;
using ExternDotnetSDK.Clients.Common.DefaultImplementations;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces.Logging;
using ExternDotnetSDK.Clients.Docflows;
using ExternDotnetSDK.Clients.Drafts;
using ExternDotnetSDK.Clients.DraftsBuilders;
using ExternDotnetSDK.Clients.Events;
using ExternDotnetSDK.Clients.InventoryDocflows;
using ExternDotnetSDK.Clients.Organizations;

namespace ExternDotnetSDK
{
    /// <summary>
    ///     Main class for using Kontur Extern API
    /// </summary>
    public class KeApiClient : IKeApiClient
    {
        //todo change this address for a real one when SDK is ready for release
        private const string BaseAddress = "https://extern-api.staging2.testkontur.ru";

        private readonly ILogger iLog;
        private readonly IRequestSender requestSender;
        private readonly IRequestFactory requestFactory;

        public KeApiClient(IRequestFactory customRequestFactory, IRequestSender customSender = null, ILogger customLogger = null)
        {
            requestFactory = customRequestFactory;
            iLog = customLogger ?? new SilentLogger();
            requestSender = customSender ?? new DefaultRequestSender(new HttpClient {BaseAddress = new Uri(BaseAddress)});
            InitializeClients();
        }

        public KeApiClient(string apiKey, string sessionId, IRequestSender customSender = null, ILogger customLogger = null)
        {
            requestFactory = new DefaultRequestFactory(new DefaultAuthenticationProvider(apiKey, sessionId));
            iLog = customLogger ?? new SilentLogger();
            requestSender = customSender ?? new DefaultRequestSender(new HttpClient {BaseAddress = new Uri(BaseAddress)});
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
            Accounts = new AccountClient(iLog, requestSender, requestFactory);
            Docflows = new DocflowsClient(iLog, requestSender, requestFactory);
            Drafts = new DraftClient(iLog, requestSender, requestFactory);
            Events = new EventsClient(iLog, requestSender, requestFactory);
            DraftsBuilder = new DraftsBuilderClient(iLog, requestSender, requestFactory);
            Organizations = new OrganizationsClient(iLog, requestSender, requestFactory);
            InventoryDocflows = new InventoryDocflowsClient(iLog, requestSender, requestFactory);
        }
    }
}
using System;
using System.Net.Http;
using Kontur.Extern.Client.Clients.Accounts;
using Kontur.Extern.Client.Clients.Authentication.Providers;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Clients.Docflows;
using Kontur.Extern.Client.Clients.Drafts;
using Kontur.Extern.Client.Clients.DraftsBuilders;
using Kontur.Extern.Client.Clients.Events;
using Kontur.Extern.Client.Clients.Organizations;
using Kontur.Extern.Client.Clients.Replies;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.Engine.Services.Docflows;

namespace Kontur.Extern.Client.Engine
{
    public class ExternEngine : IEngine
    {
        private readonly ILogger iLog;
        private readonly IRequestSender requestSender;
        private readonly IAccountIdProvider accountIdProvider;
        private readonly TimeSpan? defaultTimeout;
        private readonly ClientConfiguration clientConfiguration;

        public ExternEngine(
            IAuthenticationProvider authenticationProvider,
            ICrypt cryptoProvider,
            IAccountIdProvider accountIdProvider,
            TimeSpan? defaultTimeout,
            ClientConfiguration clientConfiguration,
            ILogger logger = null)
        {
            CryptoProvider = cryptoProvider;
            this.accountIdProvider = accountIdProvider;
            this.defaultTimeout = defaultTimeout;
            this.clientConfiguration = clientConfiguration;
            requestSender = new RequestSender(
                authenticationProvider,
                clientConfiguration.apiKey,
                new HttpClient {BaseAddress = new Uri(clientConfiguration.serviceBaseUri)});
            iLog = logger ?? new SilentLogger();
            InitializeClients();
        }

        public IDocflowsService DocflowsService { get; private set; }
        public IKeApiClient KeApiClient { get; private set; }
        public ICrypt CryptoProvider { get; }


        private void InitializeClients()
        {
            KeApiClient = new KeApiClient(requestSender, iLog);
            DocflowsService = new DocflowsService(accountIdProvider, KeApiClient.Docflows, defaultTimeout);
        }
    }
}
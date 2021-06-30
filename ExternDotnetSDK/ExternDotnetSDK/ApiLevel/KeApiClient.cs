using System;
using Kontur.Extern.Client.ApiLevel.Clients.Accounts;
using Kontur.Extern.Client.ApiLevel.Clients.Authentication;
using Kontur.Extern.Client.ApiLevel.Clients.Common.Logging;
using Kontur.Extern.Client.ApiLevel.Clients.Docflows;
using Kontur.Extern.Client.ApiLevel.Clients.Drafts;
using Kontur.Extern.Client.ApiLevel.Clients.DraftsBuilders;
using Kontur.Extern.Client.ApiLevel.Clients.Events;
using Kontur.Extern.Client.ApiLevel.Clients.Organizations;
using Kontur.Extern.Client.ApiLevel.Clients.Replies;
using Kontur.Extern.Client.Cryptography;
using Kontur.Extern.Client.HttpLevel;

namespace Kontur.Extern.Client.ApiLevel
{
    public class KeApiClient : IKeApiClient
    {
        public KeApiClient(
            string apiKey,
            IAuthenticationProvider authenticationProvider,
            string baseAddress,
            ILogger logger = null)
        {
            throw new NotSupportedException();
        }

        internal KeApiClient(IHttpRequestsFactory httpRequestsFactory, ICrypt cryptoProvider)
        {
            Http = httpRequestsFactory;
            Accounts = new AccountClient(httpRequestsFactory);
            Docflows = new DocflowsClient(httpRequestsFactory);
            Replies = new RepliesClient(httpRequestsFactory);
            Drafts = new DraftsClient(httpRequestsFactory);
            Events = new EventsClient(httpRequestsFactory);
            DraftsBuilder = new DraftsBuilderClient(httpRequestsFactory);
            Organizations = new OrganizationsClient(httpRequestsFactory);
            CryptoProvider = cryptoProvider;
        }

        public IHttpRequestsFactory Http { get; }
        public IAccountClient Accounts { get; }
        public IDocflowsClient Docflows { get; }
        public IRepliesClient Replies { get; }
        public IDraftsClient Drafts { get; }
        public IDraftsBuilderClient DraftsBuilder { get; }
        public IEventsClient Events { get; }
        public IOrganizationsClient Organizations { get; }
        public ICrypt CryptoProvider { get; }
    }
}
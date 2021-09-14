using Kontur.Extern.Api.Client.ApiLevel.Clients.Accounts;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Contents;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Docflows;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Drafts;
using Kontur.Extern.Api.Client.ApiLevel.Clients.DraftsBuilders;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Events;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Organizations;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Replies;
using Kontur.Extern.Api.Client.Http;

namespace Kontur.Extern.Api.Client.ApiLevel
{
    internal class KeApiClient : IKeApiClient
    {
        internal KeApiClient(IHttpRequestsFactory httpRequestsFactory)
        {
            Http = httpRequestsFactory;
            Accounts = new AccountClient(httpRequestsFactory);
            Docflows = new DocflowsClient(httpRequestsFactory);
            Replies = new RepliesClient(httpRequestsFactory);
            Drafts = new DraftsClient(httpRequestsFactory);
            Events = new EventsClient(httpRequestsFactory);
            Contents = new ContentsClient(httpRequestsFactory);
            DraftsBuilder = new DraftsBuilderClient(httpRequestsFactory);
            Organizations = new OrganizationsClient(httpRequestsFactory);
        }

        public IHttpRequestsFactory Http { get; }
        public IAccountClient Accounts { get; }
        public IDocflowsClient Docflows { get; }
        public IRepliesClient Replies { get; }
        public IDraftsClient Drafts { get; }
        public IDraftsBuilderClient DraftsBuilder { get; }
        public IEventsClient Events { get; }
        public IOrganizationsClient Organizations { get; }
        public IContentsClient Contents { get; }
    }
}
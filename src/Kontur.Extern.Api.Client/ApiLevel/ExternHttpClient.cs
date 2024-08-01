using Kontur.Extern.Api.Client.ApiLevel.Clients.Accounts;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Contents;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Docflows;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Drafts;
using Kontur.Extern.Api.Client.ApiLevel.Clients.DraftsBuilders;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Events;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Organizations;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Replies;
using Kontur.Extern.Api.Client.ApiLevel.Clients.ReportsTables;
using Kontur.Extern.Api.Client.Http;

namespace Kontur.Extern.Api.Client.ApiLevel
{
    internal class ExternHttpClient : IExternHttpClient
    {
        internal ExternHttpClient(IHttpRequestFactory httpRequestFactory)
        {
            Http = httpRequestFactory;
            Accounts = new AccountClient(httpRequestFactory);
            Docflows = new DocflowsClient(httpRequestFactory);
            Replies = new RepliesClient(httpRequestFactory);
            Drafts = new DraftsClient(httpRequestFactory);
            Events = new EventsClient(httpRequestFactory);
            Contents = new ContentsClient(httpRequestFactory);
            DraftsBuilder = new DraftsBuilderClient(httpRequestFactory);
            Organizations = new OrganizationsClient(httpRequestFactory);
            ReportsTables = new ReportsTablesClient(httpRequestFactory);
            Handbooks = new HandbooksClient(httpRequestFactory);
        }

        public IHttpRequestFactory Http { get; }
        public IAccountClient Accounts { get; }
        public IDocflowsClient Docflows { get; }
        public IRepliesClient Replies { get; }
        public IDraftsClient Drafts { get; }
        public IDraftsBuilderClient DraftsBuilder { get; }
        public IEventsClient Events { get; }
        public IOrganizationsClient Organizations { get; }
        public IContentsClient Contents { get; }
        public IReportsTablesClient ReportsTables { get; }
        public IHandbooksClient Handbooks { get; }
    }
}
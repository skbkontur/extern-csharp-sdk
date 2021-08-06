using Kontur.Extern.Client.ApiLevel.Clients.Accounts;
using Kontur.Extern.Client.ApiLevel.Clients.Contents;
using Kontur.Extern.Client.ApiLevel.Clients.Docflows;
using Kontur.Extern.Client.ApiLevel.Clients.Drafts;
using Kontur.Extern.Client.ApiLevel.Clients.DraftsBuilders;
using Kontur.Extern.Client.ApiLevel.Clients.Events;
using Kontur.Extern.Client.ApiLevel.Clients.Organizations;
using Kontur.Extern.Client.ApiLevel.Clients.Replies;
using Kontur.Extern.Client.Http;

namespace Kontur.Extern.Client.ApiLevel
{
    //todo Сделать нормальные тесты для всех методов всех реализованных подклиентов этого интерфейса.
    public interface IKeApiClient
    {
        IHttpRequestsFactory Http { get; }
        IAccountClient Accounts { get; }
        IDocflowsClient Docflows { get; }
        IRepliesClient Replies { get; }
        IDraftsClient Drafts { get; }
        IDraftsBuilderClient DraftsBuilder { get; }
        IEventsClient Events { get; }
        IOrganizationsClient Organizations { get; }
        IContentsClient Contents { get; }
    }
}
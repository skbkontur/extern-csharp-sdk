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
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Http;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.ApiLevel
{
    //todo Сделать нормальные тесты для всех методов всех реализованных подклиентов этого интерфейса.
    [ClientDocumentationSection]
    public interface IExternHttpClient
    {
        IHttpRequestFactory Http { get; }
        IAccountClient Accounts { get; }
        IDocflowsClient Docflows { get; }
        IRepliesClient Replies { get; }
        IDraftsClient Drafts { get; }
        IDraftsBuilderClient DraftsBuilder { get; }
        IEventsClient Events { get; }
        IOrganizationsClient Organizations { get; }
        IContentsClient Contents { get; }
        IReportsTablesClient ReportsTables { get; }
        IHandbooksClient Handbooks { get; }
    }
}
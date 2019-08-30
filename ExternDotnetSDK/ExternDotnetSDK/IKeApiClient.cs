using Kontur.Extern.Client.Clients.Accounts;
using Kontur.Extern.Client.Clients.Docflows;
using Kontur.Extern.Client.Clients.Drafts;
using Kontur.Extern.Client.Clients.DraftsBuilders;
using Kontur.Extern.Client.Clients.Events;
using Kontur.Extern.Client.Clients.InventoryDocflows;
using Kontur.Extern.Client.Clients.Organizations;

namespace Kontur.Extern.Client
{
    //todo Сделать нормальные тесты для всех методов всех реализованных подклиентов этого интерфейса.
    public interface IKeApiClient
    {
        IAccountClient Accounts { get; }
        IDocflowsClient Docflows { get; }
        IDraftClient Drafts { get; }
        IDraftsBuilderClient DraftsBuilder { get; }
        IEventsClient Events { get; }
        IInventoryDocflowsClient InventoryDocflows { get; }
        IOrganizationsClient Organizations { get; }
    }
}
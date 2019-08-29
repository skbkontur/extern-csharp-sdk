using KeApiClientOpenSdk.Clients.Accounts;
using KeApiClientOpenSdk.Clients.Docflows;
using KeApiClientOpenSdk.Clients.Drafts;
using KeApiClientOpenSdk.Clients.DraftsBuilders;
using KeApiClientOpenSdk.Clients.Events;
using KeApiClientOpenSdk.Clients.InventoryDocflows;
using KeApiClientOpenSdk.Clients.Organizations;

namespace KeApiClientOpenSdk
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
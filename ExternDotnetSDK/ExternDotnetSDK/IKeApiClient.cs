using KeApiOpenSdk.Clients.Accounts;
using KeApiOpenSdk.Clients.Docflows;
using KeApiOpenSdk.Clients.Drafts;
using KeApiOpenSdk.Clients.DraftsBuilders;
using KeApiOpenSdk.Clients.Events;
using KeApiOpenSdk.Clients.InventoryDocflows;
using KeApiOpenSdk.Clients.Organizations;

namespace KeApiOpenSdk
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
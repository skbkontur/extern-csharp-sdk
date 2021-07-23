#nullable enable
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Helpers;
using Kontur.Extern.Client.Model.DocflowFiltering;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class InventoryDocflowListPathExtension
    {
        public static IEntityList<DocflowPageItem> List(this in InventoryDocflowListPath path, DocflowFilterBuilder? filterBuilder = null)
        {
            return DocflowListsHelper.DocflowsList(
                path.Services.Api,
                path.AccountId,
                path.DocflowId,
                path.DocumentId,
                filterBuilder,
                (apiClient, accountId, relatedDocflowId, relatedDocumentId, filter, tm) => apiClient.Docflows.GetInventoryDocflowsAsync(accountId, relatedDocflowId, relatedDocumentId, filter, tm)
            );
        }
    }
}
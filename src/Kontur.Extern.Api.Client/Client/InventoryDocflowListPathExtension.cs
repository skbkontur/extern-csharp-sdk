#nullable enable
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Helpers;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class InventoryDocflowListPathExtension
    {
        public static IEntityList<IDocflow> List(this in InventoryDocflowListPath path, DocflowFilterBuilder? filterBuilder = null)
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
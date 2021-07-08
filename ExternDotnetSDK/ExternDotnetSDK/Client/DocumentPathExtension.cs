#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Model.DocflowFiltering;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class DocumentPathExtension
    {
        public static IEntityList<DocflowPageItem> RelatedDocflowsList(this in DocumentPath path, DocflowFilterBuilder? filterBuilder = null, TimeSpan? timeout = null)
        {
            return DocflowsList(
                path,
                filterBuilder,
                (apiClient, accountId, relatedDocflowId, relatedDocumentId, filter, tm) => apiClient.Docflows.GetRelatedDocflows(accountId, relatedDocflowId, relatedDocumentId, filter, tm),
                timeout
            );
        }
        
        public static IEntityList<DocflowPageItem> InventoryDocflowsList(this in DocumentPath path, DocflowFilterBuilder? filterBuilder = null, TimeSpan? timeout = null)
        {
            return DocflowsList(
                path,
                filterBuilder,
                (apiClient, accountId, relatedDocflowId, relatedDocumentId, filter, tm) => apiClient.Docflows.GetInventoryDocflowsAsync(accountId, relatedDocflowId, relatedDocumentId, filter, tm),
                timeout
            );
        }
        
        public static ILongOperation DssDecrypt(this in DocumentPath path) => throw new NotImplementedException();
        
        private static IEntityList<DocflowPageItem> DocflowsList(in DocumentPath path, 
                                                                 DocflowFilterBuilder? filterBuilder,
                                                                 LoadPage loadPage, 
                                                                 TimeSpan? timeout)
        {
            var apiClient = path.Services.Api;

            var accountId = path.AccountId;
            var relatedDocflowId = path.DocflowId;
            var relatedDocumentId = path.DocumentId;
            
            var docflowFilter = filterBuilder?.CreateFilter() ?? new DocflowFilter();
            return new EntityList<DocflowPageItem>(
                async (skip, take) =>
                {
                    checked
                    {
                        docflowFilter.Skip = (int) skip;
                        docflowFilter.Take = (int) take;
                    }
                    var relatedDocflows = await loadPage(apiClient, accountId, relatedDocflowId, relatedDocumentId, docflowFilter, timeout).ConfigureAwait(false);
                    return (relatedDocflows.DocflowsPageItem, relatedDocflows.TotalCount);
                });
        }

        private delegate Task<DocflowPage> LoadPage(IKeApiClient apiClient, 
                                                    Guid accountId,
                                                    Guid relatedDocflowId,
                                                    Guid relatedDocumentId,
                                                    DocflowFilter filter = null,
                                                    TimeSpan? timeout = null);
    }
}
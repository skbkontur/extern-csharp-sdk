#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Responses.Docflows;
using Kontur.Extern.Client.Model.DocflowFiltering;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client.Helpers
{
    internal static class DocflowListsHelper
    {
        internal static IEntityList<DocflowPageItem> DocflowsList(
            IKeApiClient apiClient,
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            DocflowFilterBuilder? filterBuilder,
            LoadPage loadPage)
        {
            var docflowFilter = filterBuilder?.CreateFilter() ?? new DocflowFilter();
            return new EntityList<DocflowPageItem>(
                async (skip, take, timeout) =>
                {
                    checked
                    {
                        docflowFilter.Skip = (int) skip;
                        docflowFilter.Take = (int) take;
                    }

                    var relatedDocflows = await loadPage(apiClient, accountId, docflowId, documentId, docflowFilter, timeout).ConfigureAwait(false);
                    return (relatedDocflows.DocflowsPageItem, relatedDocflows.TotalCount);
                });
        }

        internal delegate Task<DocflowPage> LoadPage(IKeApiClient apiClient, 
                                                     Guid accountId,
                                                     Guid docflowId,
                                                     Guid documentId,
                                                     DocflowFilter? filter = null,
                                                     TimeSpan? timeout = null);
    }
}
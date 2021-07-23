#nullable enable
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Model.DocflowFiltering;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class DocflowListPathExtension
    {
        public static IEntityList<DocflowPageItem> List(this in DocflowListPath path, DocflowFilterBuilder? filterBuilder = null)
        {
            var apiClient = path.Services.Api;
            var apiFilter = filterBuilder?.CreateFilter() ?? new DocflowFilter();
            
            var accountId = path.AccountId;
            return new EntityList<DocflowPageItem>(
                async (skip, take, timeout) =>
                {
                    checked
                    {
                        apiFilter.Skip = (int) skip;
                        apiFilter.Take = (int) take;
                    }

                    var docflowPage = await apiClient.Docflows.GetDocflowsAsync(accountId, apiFilter, timeout);

                    return (docflowPage.DocflowsPageItem, docflowPage.TotalCount);
                }
            );
        }
    }
}
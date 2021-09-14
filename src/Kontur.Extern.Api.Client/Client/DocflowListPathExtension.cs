using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DocflowListPathExtension
    {
        public static IEntityList<IDocflow> List(this in DocflowListPath path, DocflowFilterBuilder? filterBuilder = null)
        {
            var apiClient = path.Services.Api;
            var apiFilter = filterBuilder?.CreateFilter() ?? new DocflowFilter();
            
            var accountId = path.AccountId;
            return new EntityList<IDocflow>(
                async (skip, take, timeout) =>
                { 
                    apiFilter.Skip = skip;
                    apiFilter.Take = take;

                    var docflowPage = await apiClient.Docflows.GetDocflowsAsync(accountId, apiFilter, timeout);

                    return (docflowPage.DocflowsPageItem, docflowPage.TotalCount);
                }
            );
        }
    }
}
using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Model.DocflowFiltering;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    internal static class DocflowListPathExtension
    {
        public static IEntityList<DocflowPageItem> List(this in DocflowListPath path, DocflowFilterBuilder filterBuilder, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            var apiFilter = filterBuilder.CreateFilter();
            
            var accountId = path.AccountId;
            return new EntityList<DocflowPageItem>(
                async (skip, take) =>
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
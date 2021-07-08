using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Model.DocflowFiltering;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    internal static class DocumentPathExtension
    {
        public static IEntityList<DocflowPageItem> GetRelatedDocflows(this in DocumentPath path, DocflowFilterBuilder filterBuilder, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;

            var accountId = path.AccountId;
            var relatedDocflowId = path.DocflowId;
            var relatedDocumentId = path.DocumentId;
            
            var docflowFilter = filterBuilder.CreateFilter();
            return new EntityList<DocflowPageItem>(
                async (skip, take) =>
                {
                    checked
                    {
                        docflowFilter.Skip = (int) skip;
                        docflowFilter.Take = (int) take;
                    }
                    var relatedDocflows = await apiClient.Docflows.GetRelatedDocflows(accountId, relatedDocflowId, relatedDocumentId, docflowFilter, timeout).ConfigureAwait(false);
                    
                    return (relatedDocflows.DocflowsPageItem, relatedDocflows.TotalCount);
                });
        }
        
        public static ILongOperation DssDecrypt(this in DocumentPath path) => throw new NotImplementedException();
    }
}
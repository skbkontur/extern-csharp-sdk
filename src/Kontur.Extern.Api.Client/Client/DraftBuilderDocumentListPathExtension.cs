using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.Documents;
using Kontur.Extern.Api.Client.Model.DraftBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DraftBuilderDocumentListPathExtension
    {
        public static Task<DraftsBuilderDocumentMeta> SetAsync(
            this in DraftBuilderDocumentListPath path, 
            IDraftsBuilderDocument document,
            DraftsBuilderDocumentData? data,
            TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            Guid documentId;
            var request = new DraftsBuilderDocumentMetaRequest
            {
                BuilderData = data ?? new UnknownBuilderDocumentData()
            };
            return apiClient.DraftsBuilder.UpdateDocumentMetaAsync(path.AccountId, path.DraftBuilderId, documentId, request, timeout);
        }
        
        public static Task<IReadOnlyCollection<DraftsBuilderDocument>> ListAsync(this in DraftBuilderDocumentListPath path, IDraftsBuilderDocument document, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.GetDocumentsAsync(path.AccountId, path.DraftBuilderId, timeout);
        }
    }
}
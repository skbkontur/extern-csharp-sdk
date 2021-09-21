using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DraftBuilderDocumentPathExtension
    {
        public static Task<DraftsBuilderDocument> GetAsync(this in DraftBuilderDocumentPath path, DraftsBuilderDocumentData data, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.GetDocumentAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, timeout);
        }
        
        public static Task<DraftsBuilderDocument?> TryGetAsync(this in DraftBuilderDocumentPath path, DraftsBuilderDocumentData data, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.TryGetDocumentAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, timeout);
        }
        
        public static Task<DraftsBuilderDocumentMeta> GetMetaAsync(this in DraftBuilderDocumentPath path, DraftsBuilderDocumentData data, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.GetDocumentMetaAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, timeout);
        }
        
        public static Task<DraftsBuilderDocumentMeta?> TryGetMetaAsync(this in DraftBuilderDocumentPath path, DraftsBuilderDocumentData data, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.TryGetDocumentMetaAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, timeout);
        }
        
        public static Task<bool> DeleteAsync(this in DraftBuilderDocumentPath path, DraftsBuilderDocumentData data, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.DraftsBuilder.DeleteDocumentAsync(path.AccountId, path.DraftBuilderId, path.DocumentId, timeout);
        }
    }
}
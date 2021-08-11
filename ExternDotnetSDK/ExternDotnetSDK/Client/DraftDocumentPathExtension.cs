using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.Paths;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class DraftDocumentPathExtension
    {
        public static Task<ApiLevel.Models.Drafts.DraftDocument> GetAsync(this DraftDocumentPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.GetDocumentAsync(path.AccountId, path.DraftId, path.DocumentId, timeout);
        }
        
        public static Task DeleteAsync(this DraftDocumentPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.DeleteDocumentAsync(path.AccountId, path.DraftId, path.DocumentId, timeout);
        }
    }
}
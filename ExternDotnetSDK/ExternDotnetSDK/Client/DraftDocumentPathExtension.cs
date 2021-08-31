using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Documents;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.Http.Models;
using Kontur.Extern.Client.Paths;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class DraftDocumentPathExtension
    {
        public static Task<DraftDocument> GetAsync(this DraftDocumentPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.GetDocumentAsync(path.AccountId, path.DraftId, path.DocumentId, timeout);
        }
        
        public static Task DeleteAsync(this DraftDocumentPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.DeleteDocumentAsync(path.AccountId, path.DraftId, path.DocumentId, timeout);
        }

        public static async Task<Guid> AddSignatureAsync(this DraftDocumentPath path, Base64String signature, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            var signatureRequest = new SignatureRequest
            {
                Base64Content = signature.ToString()
            };
            var documentId = path.DocumentId;
            var draftId = path.DraftId;
            var accountId = path.AccountId;

            var createdSignature = await apiClient.Drafts.CreateSignatureAsync(accountId, draftId, documentId, signatureRequest, timeout).ConfigureAwait(false);

            return createdSignature.Id;
        }
    }
}
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Signatures;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Http.Models;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DraftDocumentSignaturePathExtension
    {
        public static Task<Signature> GetAsync(this DraftDocumentSignaturePath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.GetSignatureAsync(path.AccountId, path.DraftId, path.DocumentId, path.SignatureId, timeout);
        }

        public static Task<bool> DeleteAsync(this DraftDocumentSignaturePath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.DeleteSignatureAsync(path.AccountId, path.DraftId, path.DocumentId, path.SignatureId, timeout);
        }

        public static Task UpdateAsync(this DraftDocumentSignaturePath path, Base64String signature, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            var signatureRequest = new SignatureRequest
            {
                Base64Content = signature.ToString()
            };
            return apiClient.Drafts.UpdateSignatureAsync(path.AccountId, path.DraftId, path.DocumentId, path.SignatureId, signatureRequest, timeout);
        }

        public static async Task<byte[]> DownloadAsync(this DraftDocumentSignaturePath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            var signatureContent = await apiClient.Drafts.GetSignatureContentAsync(path.AccountId, path.DraftId, path.DocumentId, path.SignatureId, timeout).ConfigureAwait(false);
            return signatureContent;
        }
    }
}
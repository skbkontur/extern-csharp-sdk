using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Signatures;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Http.Models;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct DraftDocumentSignaturePath
    {
        public DraftDocumentSignaturePath(Guid accountId, Guid draftId, Guid documentId, Guid signatureId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftId = draftId;
            DocumentId = documentId;
            SignatureId = signatureId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftId { get; }
        public Guid DocumentId { get; }
        public Guid SignatureId { get; }
        public IExternClientServices Services { get; }

        public Task<Signature> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Drafts.GetSignatureAsync(AccountId, DraftId, DocumentId, SignatureId, timeout);
        }

        public Task<bool> DeleteAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Drafts.DeleteSignatureAsync(AccountId, DraftId, DocumentId, SignatureId, timeout);
        }

        public Task UpdateAsync(Base64String signature, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            var signatureRequest = new SignatureRequest
            {
                Base64Content = signature.ToString()
            };
            return apiClient.Drafts.UpdateSignatureAsync(AccountId, DraftId, DocumentId, SignatureId, signatureRequest, timeout);
        }

        public async Task<byte[]> DownloadAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            var signatureContent = await apiClient.Drafts.GetSignatureContentAsync(AccountId, DraftId, DocumentId, SignatureId, timeout).ConfigureAwait(false);
            return signatureContent;
        }
    }
}
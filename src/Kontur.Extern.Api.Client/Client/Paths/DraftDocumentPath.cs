using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Signatures;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Http.Models;
using Kontur.Extern.Api.Client.Models.Drafts.Documents;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct DraftDocumentPath
    {
        public DraftDocumentPath(Guid accountId, Guid draftId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftId = draftId;
            DocumentId = documentId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftId { get; }
        public Guid DocumentId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public DraftDocumentSignaturePath Signature(Guid signatureId) => new(AccountId, DraftId, DocumentId, signatureId, services);

        public Task<DraftDocument> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Drafts.GetDocumentAsync(AccountId, DraftId, DocumentId, timeout);
        }

        public Task<bool> DeleteAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Drafts.DeleteDocumentAsync(AccountId, DraftId, DocumentId, timeout);
        }

        public async Task<Guid> AddSignatureAsync(Base64String signature, TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            var signatureRequest = new SignatureRequest
            {
                Base64Content = signature.ToString()
            };
            var documentId = DocumentId;
            var draftId = DraftId;
            var accountId = AccountId;

            var createdSignature = await apiClient.Drafts.CreateSignatureAsync(accountId, draftId, documentId, signatureRequest, timeout).ConfigureAwait(false);

            return createdSignature.Id;
        }
    }
}
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Api.Client.Model;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct DraftBuilderDocumentFilePath
    {
        public DraftBuilderDocumentFilePath(Guid accountId, Guid draftBuilderId, Guid documentId, Guid fileId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftBuilderId = draftBuilderId;
            DocumentId = documentId;
            FileId = fileId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftBuilderId { get; }
        public Guid DocumentId { get; }
        public Guid FileId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public Task<DraftsBuilderDocumentFile> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.GetFileAsync(AccountId, DraftBuilderId, DocumentId, FileId, timeout);
        }

        public Task<DraftsBuilderDocumentFile?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.TryGetFileAsync(AccountId, DraftBuilderId, DocumentId, FileId, timeout);
        }

        public Task<bool> DeleteAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.DeleteFileAsync(AccountId, DraftBuilderId, DocumentId, FileId, timeout);
        }

        public Task<DraftsBuilderDocumentFileMeta> GetMetaAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.GetFileMetaAsync(AccountId, DraftBuilderId, DocumentId, FileId, timeout);
        }

        public Task<DraftsBuilderDocumentFileMeta?> TryGetMetaAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.TryGetFileMetaAsync(AccountId, DraftBuilderId, DocumentId, FileId, timeout);
        }

        public Task<DraftsBuilderDocumentFileMeta> UpdateMetaAsync(string fileName, DraftsBuilderDocumentFileData? data = null, TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.UpdateFileMetaAsync(
                AccountId,
                DraftBuilderId,
                DocumentId,
                FileId,
                new DraftsBuilderFileMetaRequest(fileName, data),
                timeout
            );
        }

        public async Task<Signature> GetSignatureAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return await apiClient.DraftsBuilder.GetSignatureAsync(AccountId, DraftBuilderId, DocumentId, FileId, timeout).ConfigureAwait(false);
        }

        public async Task<Signature?> TryGetSignatureAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            var signatureBytes = await apiClient.DraftsBuilder.TryGetSignatureAsync(AccountId, DraftBuilderId, DocumentId, FileId, timeout).ConfigureAwait(false);
            return signatureBytes is null ? null : Signature.FromBytes(signatureBytes);
        }
    }
}
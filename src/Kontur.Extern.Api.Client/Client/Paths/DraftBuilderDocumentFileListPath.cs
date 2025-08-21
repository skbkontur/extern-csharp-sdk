using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles;


namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
    public readonly struct DraftBuilderDocumentFileListPath
    {
        public DraftBuilderDocumentFileListPath(Guid accountId, Guid draftBuilderId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftBuilderId = draftBuilderId;
            DocumentId = documentId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftBuilderId { get; }
        public Guid DocumentId { get; }
        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public DraftBuilderDocumentFilePath WithId(Guid fileId) => new(AccountId, DraftBuilderId, DocumentId, fileId, services);

        public async Task<DraftsBuilderDocumentFile> SetFileAsync( //todo перенести в DraftBuilderDocumentFilePath?
            Kontur.Extern.Api.Client.Model.DraftBuilders.DraftsBuilderDocumentFile file,
            TimeSpan? uploadTimeout = null,
            TimeSpan? putTimeout = null)
        {
            var apiClient = services.Api;
            var uploader = services.ContentService;
            var crypt = services.Crypt;

            var documentRequest = await file
                .CreateSignedRequestAsync(AccountId, uploader, crypt, uploadTimeout).ConfigureAwait(false);

            return await apiClient.DraftsBuilder
                .UpdateFileAsync(AccountId, DraftBuilderId, DocumentId, file.FileId, documentRequest, putTimeout).ConfigureAwait(false);
        }

        public Task<IReadOnlyCollection<DraftsBuilderDocumentFile>> ListAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.DraftsBuilder.GetFilesAsync(AccountId, DraftBuilderId, DocumentId, timeout);
        }
    }
}
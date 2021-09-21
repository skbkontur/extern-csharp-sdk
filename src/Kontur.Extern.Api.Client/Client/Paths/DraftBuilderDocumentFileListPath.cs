using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct DraftBuilderDocumentFileListPath
    {
        public DraftBuilderDocumentFileListPath(Guid accountId, Guid draftBuilderId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftBuilderId = draftBuilderId;
            DocumentId = documentId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftBuilderId { get; }
        public Guid DocumentId { get; }
        public IExternClientServices Services { get; }

        public DraftBuilderDocumentFilePath WithId(Guid fileId) => new(AccountId, DraftBuilderId, DocumentId, fileId, Services);
    }
}
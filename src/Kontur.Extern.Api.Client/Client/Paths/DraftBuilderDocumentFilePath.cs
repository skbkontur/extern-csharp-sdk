using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct DraftBuilderDocumentFilePath
    {
        public DraftBuilderDocumentFilePath(Guid accountId, Guid draftBuilderId, Guid documentId, Guid fileId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftBuilderId = draftBuilderId;
            DocumentId = documentId;
            FileId = fileId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftBuilderId { get; }
        public Guid DocumentId { get; }
        public Guid FileId { get; }
        public IExternClientServices Services { get; }
    }
}
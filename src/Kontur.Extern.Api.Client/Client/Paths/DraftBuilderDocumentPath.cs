using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct DraftBuilderDocumentPath
    {
        public DraftBuilderDocumentPath(Guid accountId, Guid draftBuilderId, Guid documentId, IExternClientServices services)
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

        public DraftBuilderDocumentFileListPath Files => new(AccountId, DraftBuilderId, DocumentId, Services);
    }
}
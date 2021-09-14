using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct DraftDocumentPath
    {
        public DraftDocumentPath(Guid accountId, Guid draftId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftId = draftId;
            DocumentId = documentId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftId { get; }
        public Guid DocumentId { get; }
        public IExternClientServices Services { get; }
        
        public DraftDocumentSignaturePath Signature(Guid signatureId) => new(AccountId, DraftId, DocumentId, signatureId, Services);
    }
}
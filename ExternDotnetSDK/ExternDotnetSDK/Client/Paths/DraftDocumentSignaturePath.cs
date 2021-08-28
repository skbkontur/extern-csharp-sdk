using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
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
    }
}
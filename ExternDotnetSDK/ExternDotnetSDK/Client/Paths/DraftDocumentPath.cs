using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct DraftDocumentPath
    {
        public DraftDocumentPath(Guid accountId, Guid draftId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftId = draftId;
            DocumentId = documentId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DraftId { get; }
        public Guid DocumentId { get; }
        public IExternClientServices Services { get; }
    }
}
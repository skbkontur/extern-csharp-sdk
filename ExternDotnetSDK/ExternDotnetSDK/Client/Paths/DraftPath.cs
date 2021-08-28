using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct DraftPath
    {
        public DraftPath(Guid accountId, Guid draftId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftId = draftId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftId { get; }
        public IExternClientServices Services { get; }
        
        public DraftDocumentPath Document(Guid documentId) => new(AccountId, DraftId, documentId, Services);
    }
}
using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct DocumentPath
    {
        public DocumentPath(Guid accountId, Guid docflowId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public InventoryDocflowListPath InventoryDocflows => new(AccountId, DocflowId, DocumentId, Services);
        public DocumentContentListPath Contents => new(AccountId, DocflowId, DocumentId, Services);
        public DocumentReplyPath Reply(Guid replyId) => new(AccountId, DocflowId, DocumentId, replyId, Services);
        public IExternClientServices Services { get; }
    }
}
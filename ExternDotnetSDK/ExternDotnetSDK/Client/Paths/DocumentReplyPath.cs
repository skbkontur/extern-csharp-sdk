using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct DocumentReplyPath
    {
        public DocumentReplyPath(Guid accountId, Guid docflowId, Guid documentId, Guid replyId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            ReplyId = replyId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public Guid ReplyId { get; }
        public IExternClientServices Services { get; }
    }
}
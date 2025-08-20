using System;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.Docflows;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ApiPathSection]
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

        public Task<IDocflowWithDocuments> SendReplyAsync(IPAddress address, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Replies.SendReplyAsync(
                AccountId,
                DocflowId,
                DocumentId,
                ReplyId,
                address,
                timeout);
        }
    }
}
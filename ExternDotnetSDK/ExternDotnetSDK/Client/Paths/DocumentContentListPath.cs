using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct DocumentContentListPath
    {
        public DocumentContentListPath(Guid accountId, Guid docflowId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public IExternClientServices Services { get; }

        public DocumentContentPath WithId(Guid contentId) => new DocumentContentPath(AccountId, DocflowId, DocumentId, contentId, Services);
    }
}
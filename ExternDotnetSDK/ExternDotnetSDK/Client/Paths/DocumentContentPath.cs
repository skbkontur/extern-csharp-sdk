using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct DocumentContentPath
    {
        public DocumentContentPath(Guid accountId, Guid docflowId, Guid documentId, Guid contentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            ContentId = contentId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public Guid ContentId { get; }
        public IExternClientServices Services { get; }
    }
}
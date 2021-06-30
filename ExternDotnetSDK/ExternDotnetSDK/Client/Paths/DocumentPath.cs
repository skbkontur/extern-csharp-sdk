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
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public IExternClientServices Services { get; }
    }
}
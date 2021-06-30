using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct DocumentListPath
    {
        public DocumentListPath(Guid accountId, Guid docflowId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public IExternClientServices Services { get; }

        public DocumentPath WithId(Guid documentId) => new(AccountId, DocflowId, documentId, Services);
    }
}
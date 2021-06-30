using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct DocflowPath
    {
        public DocflowPath(Guid accountId, Guid docflowId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public IExternClientServices Services { get; }

        public DocumentListPath Documents => new(AccountId, DocflowId, Services);
    }
}
using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct DocflowListPath
    {
        public DocflowListPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services;
        }

        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public DocflowPath WithId(Guid docflowId) => new(AccountId, docflowId, Services);
    }
}
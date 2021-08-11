using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct ContentsPath
    {
        public ContentsPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services;
        }

        public Guid AccountId { get; }
        public IExternClientServices Services { get; }
    }
}
using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct AccountListPath
    {
        public AccountListPath(IExternClientServices services) => Services = services ?? throw new ArgumentNullException(nameof(services));
        
        public IExternClientServices Services { get; }

        public AccountPath WithId(Guid accountId) => new(accountId, Services);
    }
}
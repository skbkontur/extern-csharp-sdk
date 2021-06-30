using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct AccountPath
    {
        public AccountPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services;
        }
        
        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public OrganizationListPath Organizations => new(AccountId, Services);
        public DocflowListPath Docflows => new(AccountId, Services);
    }
}
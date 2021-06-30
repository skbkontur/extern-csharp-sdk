using System;
using Kontur.Extern.Client.Common;

namespace Kontur.Extern.Client.Paths
{
    public readonly struct OrganizationPath
    {
        public OrganizationPath(Guid accountId, Guid organizationId, IExternClientServices services)
        {
            AccountId = accountId;
            OrganizationId = organizationId;
            Services = services;
        }
        
        public Guid AccountId { get; }
        public Guid OrganizationId { get; }
        public IExternClientServices Services { get; }
    }
}
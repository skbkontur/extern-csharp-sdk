using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct OrganizationPath
    {
        public OrganizationPath(Guid accountId, Guid organizationId, IExternClientServices services)
        {
            AccountId = accountId;
            OrganizationId = organizationId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
        
        public Guid AccountId { get; }
        public Guid OrganizationId { get; }
        public IExternClientServices Services { get; }
    }
}
using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    public readonly struct OrganizationListPath
    {
        public OrganizationListPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }
        
        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public OrganizationPath WithId(Guid organizationId) => new(AccountId, organizationId, Services);
    }
}
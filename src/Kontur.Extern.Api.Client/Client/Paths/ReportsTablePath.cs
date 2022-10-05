using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

public readonly struct ReportsTablePath
{
    public ReportsTablePath(Guid accountId, Guid organizationId, IExternClientServices services)
    {
        AccountId = accountId;
        OrganizationId = organizationId;
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public Guid AccountId { get; }
    public Guid OrganizationId { get; }
    public IExternClientServices Services { get; }

    public FormListPath Forms => new(AccountId, OrganizationId, Services);
}
using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

public readonly struct ReportsTableListPath
{
    public ReportsTableListPath(Guid accountId, IExternClientServices services)
    {
        AccountId = accountId;
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public Guid AccountId { get; }
    public IExternClientServices Services { get; }

    public ReportsTablePath WithOrganizationId(Guid organizationId)
    {
        return new(AccountId, organizationId, Services);
    }

    public PaymentsListPath Payments => new(AccountId, Services);
}
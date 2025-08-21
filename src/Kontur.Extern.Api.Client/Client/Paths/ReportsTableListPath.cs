using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ApiPathSection]
public readonly struct ReportsTableListPath
{
    public ReportsTableListPath(Guid accountId, IExternClientServices services)
    {
        AccountId = accountId;
        this.services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public Guid AccountId { get; }
    private readonly IExternClientServices services;

    #region ObsoleteCode
    [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
    public IExternClientServices Services => services;
    #endregion

    public ReportsTablePath WithOrganizationId(Guid organizationId)
    {
        return new(AccountId, organizationId, services);
    }

    public PaymentsListPath Payments => new(AccountId, services);

    public IEntityList<ReportsTable> List(
        Guid[]? organizationIds = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null)
    {
        var apiClient = services.Api;
        var accountId = AccountId;
        return new EntityList<ReportsTable>(
            async (skip, take, timeout) =>
            {
                int intSkip;
                checked
                {
                    intSkip = (int)skip;
                }

                var reportsTableResult = await apiClient
                    .ReportsTables
                    .GetReportsTablesAsync(
                        accountId,
                        organizationIds,
                        dateFrom,
                        dateTo,
                        intSkip,
                        take,
                        timeout);

                return (reportsTableResult.ReportsTables, reportsTableResult.ReportsTables.Length);
            });
    }
}
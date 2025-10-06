using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ClientDocumentationSection]
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

    public IEntityList<ReportsTable> List(
        Guid[]? organizationIds = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null)
    {
        var apiClient = Services.Api;
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
using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client;

[PublicAPI]
public static class ReportsTablesListPathExtension
{
    public static IEntityList<ReportsTable> List(
        this in ReportsTableListPath path,
        Guid[]? organizationIds = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        bool? includeDeleted = false)
    {
        var apiClient = path.Services.Api;
        var accountId = path.AccountId;
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
                        includeDeleted,
                        intSkip,
                        take,
                        timeout);

                return (reportsTableResult.ReportsTables, reportsTableResult.ReportsTables.Length);
            });
    }
}
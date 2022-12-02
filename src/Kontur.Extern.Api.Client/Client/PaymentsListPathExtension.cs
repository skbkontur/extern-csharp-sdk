using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables.Payments;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client;

[PublicAPI]
public static class PaymentsListPathExtension
{
    public static IEntityList<OrganizationPayments> List(
        this in PaymentsListPath path,
        Guid[]? organizationIds = null,
        DateTime? deadlineFrom = null,
        DateTime? deadlineTo = null)
    {
        var apiClient = path.Services.Api;
        var accountId = path.AccountId;
        return new EntityList<OrganizationPayments>(
            async (skip, take, timeout) =>
            {
                int intSkip;
                checked
                {
                    intSkip = (int)skip;
                }

                var paymentsResult = await apiClient
                    .ReportsTables
                    .GetPaymentsAsync(
                        accountId,
                        organizationIds,
                        deadlineFrom,
                        deadlineTo,
                        intSkip,
                        take,
                        timeout);

                return (paymentsResult.OrganizationPayments, paymentsResult.TotalCount);
            });
    }
}
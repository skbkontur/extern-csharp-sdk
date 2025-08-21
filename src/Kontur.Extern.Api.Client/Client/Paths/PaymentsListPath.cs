using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.ReportsTables.Payments;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ApiPathSection]
public readonly struct PaymentsListPath
{
    public Guid AccountId { get; }
    private readonly IExternClientServices services;

    #region ObsoleteCode
    [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
    public IExternClientServices Services => services;
    #endregion

    public PaymentsListPath(Guid accountId, IExternClientServices services)
    {
        AccountId = accountId;
        this.services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public IEntityList<OrganizationPayments> List(
        Guid[]? organizationIds = null,
        DateTime? deadlineFrom = null,
        DateTime? deadlineTo = null)
    {
        var apiClient = services.Api;
        var accountId = AccountId;
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
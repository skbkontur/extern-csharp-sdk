using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ApiPathSection]
public readonly struct ReportsTableDocflowsPath
{
    public ReportsTableDocflowsPath(Guid accountId, Guid organizationId, IExternClientServices services)
    {
        AccountId = accountId;
        OrganizationId = organizationId;
        this.services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public Guid AccountId { get; }
    public Guid OrganizationId { get; }
    private readonly IExternClientServices services;

    #region ObsoleteCode
    [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
    public IExternClientServices Services => services;
    #endregion

    public async Task<ReportsTableDocflows> ListAsync(
        int formId,
        DateTime deadline,
        int periodYear,
        int periodNumber,
        TimeSpan? timeout = null)
    {
        var apiClient = services.Api;
        var docflows = await apiClient.ReportsTables.GetReportDocflowsAsync(
                AccountId,
                OrganizationId,
                formId,
                deadline,
                periodYear,
                periodNumber,
                timeout)
            .ConfigureAwait(false);
        return docflows;
    }
}
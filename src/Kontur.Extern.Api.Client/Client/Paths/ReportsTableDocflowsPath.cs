using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ClientDocumentationSection]
public readonly struct ReportsTableDocflowsPath
{
    public ReportsTableDocflowsPath(Guid accountId, Guid organizationId, IExternClientServices services)
    {
        AccountId = accountId;
        OrganizationId = organizationId;
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public Guid AccountId { get; }
    public Guid OrganizationId { get; }
    public IExternClientServices Services { get; }

    public async Task<ReportsTableDocflows> ListAsync(
        int formId,
        DateTime deadline,
        int periodYear,
        int periodNumber,
        TimeSpan? timeout = null)
    {
        var apiClient = Services.Api;
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
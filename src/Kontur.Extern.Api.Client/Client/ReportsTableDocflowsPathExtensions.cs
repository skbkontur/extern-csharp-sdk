using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client;

[PublicAPI]
public static class ReportsTableDocflowsPathExtensions
{
    public static async Task<ReportsTableDocflows> ListAsync(
        this ReportsTableDocflowsPath path,
        int formId,
        string deadline,
        int periodYear,
        int periodNumber,
        TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        var docflows = await apiClient.ReportsTables.GetDocflowsAsync(path.AccountId, path.OrganizationId, formId, deadline, periodYear, periodNumber).ConfigureAwait(false);
        return docflows;
    }
}
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.ReportsTables;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client;

[PublicAPI]
public static class FormListPathExtension
{
    public static Task<FormsResult> ListAsync(this in FormListPath path, bool? includeDeleted = false, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        return apiClient.ReportsTables.GetFormsAsync(path.AccountId, path.OrganizationId, includeDeleted, timeout);
    }
}
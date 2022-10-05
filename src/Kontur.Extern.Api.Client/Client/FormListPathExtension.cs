using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client;

[PublicAPI]
public static class FormListPathExtension
{
    public static Task<IReadOnlyCollection<FormInfo>> ListAsync(this in FormListPath path, bool? includeDeleted = false, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        return apiClient.ReportsTables.GetFormsAsync(path.AccountId, path.OrganizationId, includeDeleted, timeout);
    }
}
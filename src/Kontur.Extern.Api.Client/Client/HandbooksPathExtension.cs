using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client;

[PublicAPI]
public static class HandbooksPathExtension
{
    public static async Task<List<ControlUnit>> GetControlUnits(this HandbooksPath path, HandbookFilter? handbookFilter = null, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        var controlUnits = await apiClient.Handbooks.GetControlUnits(handbookFilter, timeout).ConfigureAwait(false);
        return controlUnits;
    }

    public static async Task<ControlUnit> GetControlUnit(this HandbooksPath path, string code, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        var controlUnit = await apiClient.Handbooks.GetControlUnit(code, timeout).ConfigureAwait(false);
        return controlUnit;
    }

    public static async Task<List<FnsForm>> GetFnsForms(this HandbooksPath path, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        var fnsForms = await apiClient.Handbooks.GetFnsForms(timeout).ConfigureAwait(false);
        return fnsForms;
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client;

[PublicAPI]
public static class HandbooksPathExtension
{
    public static async Task<List<ControlUnit>> GetControlUnits(this HandbooksPath path, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        var controlUnits = await apiClient.Handbooks.GetControlUnits(path.AccountId, timeout).ConfigureAwait(false);
        return controlUnits;
    }

    public static async Task<ControlUnit> GetControlUnit(this HandbooksPath path, string code, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        var controlUnit = await apiClient.Handbooks.GetControlUnit(path.AccountId, code, timeout).ConfigureAwait(false);
        return controlUnit;
    }

    public static async Task<List<FnsForm>> GetFnsForms(this HandbooksPath path, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        var fnsForms = await apiClient.Handbooks.GetFnsForms(path.AccountId, timeout).ConfigureAwait(false);
        return fnsForms;
    }
}
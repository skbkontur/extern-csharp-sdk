using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client;

[PublicAPI]
public static class HandbooksPathExtension
{
    public static async Task<ControlUnitsPage> GetControlUnits(this HandbooksPath path, ControlUnitsFilter? filter = null, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        var controlUnits = await apiClient.Handbooks.GetControlUnits(filter, timeout).ConfigureAwait(false);
        return controlUnits;
    }

    public static async Task<ControlUnit> GetControlUnit(this HandbooksPath path, string code, AmbiguousUnitType? unitType = null, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        var controlUnit = await apiClient.Handbooks.GetControlUnit(code, unitType).ConfigureAwait(false);
        return controlUnit;
    }

    public static async Task<FnsFormsPage> GetFnsForms(this HandbooksPath path, FnsFormsFilter? filter = null, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        var fnsForms = await apiClient.Handbooks.GetFnsForms(filter, timeout).ConfigureAwait(false);
        return fnsForms;
    }
}
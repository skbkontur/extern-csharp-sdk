using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ApiPathSection]
public readonly struct HandbooksPath
{
    public HandbooksPath(IExternClientServices services)
    {
        this.services = services ?? throw new ArgumentNullException(nameof(services));
    }

    private readonly IExternClientServices services;

    #region ObsoleteCode
    [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
    public IExternClientServices Services => services;
    #endregion

    public async Task<ControlUnitsPage> GetControlUnits(ControlUnitsFilter? filter = null, TimeSpan? timeout = null)
    {
        var apiClient = services.Api;
        var controlUnits = await apiClient.Handbooks.GetControlUnits(filter, timeout).ConfigureAwait(false);
        return controlUnits;
    }

    public async Task<ControlUnit> GetControlUnit(string code, TimeSpan? timeout = null)
    {
        var apiClient = services.Api;
        var controlUnit = await apiClient.Handbooks.GetControlUnit(code, timeout).ConfigureAwait(false);
        return controlUnit;
    }

    public async Task<FnsFormsPage> GetFnsForms(FnsFormsFilter? filter = null, TimeSpan? timeout = null)
    {
        var apiClient = services.Api;
        var fnsForms = await apiClient.Handbooks.GetFnsForms(filter, timeout).ConfigureAwait(false);
        return fnsForms;
    }
}
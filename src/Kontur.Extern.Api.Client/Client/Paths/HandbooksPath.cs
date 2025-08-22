using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ClientDocumentationSection]
public readonly struct HandbooksPath
{
    public HandbooksPath(IExternClientServices services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public IExternClientServices Services { get; }

    public async Task<ControlUnitsPage> GetControlUnits(ControlUnitsFilter? filter = null, TimeSpan? timeout = null)
    {
        var apiClient = Services.Api;
        var controlUnits = await apiClient.Handbooks.GetControlUnits(filter, timeout).ConfigureAwait(false);
        return controlUnits;
    }

    public async Task<ControlUnit> GetControlUnit(string code, TimeSpan timeout)
    {
        return await GetControlUnit(code, null, timeout);
    }

    public async Task<ControlUnit> GetControlUnit(string code, AmbiguousControlUnitType? controlUnitType = null, TimeSpan? timeout = null)
    {
        var apiClient = Services.Api;
        var controlUnit = await apiClient.Handbooks.GetControlUnit(code, controlUnitType).ConfigureAwait(false);
        return controlUnit;
    }

    public async Task<FnsFormsPage> GetFnsForms(FnsFormsFilter? filter = null, TimeSpan? timeout = null)
    {
        var apiClient = Services.Api;
        var fnsForms = await apiClient.Handbooks.GetFnsForms(filter, timeout).ConfigureAwait(false);
        return fnsForms;
    }

    public async Task<HandbookPage> GetHandbook(HandbookType handbookType, HandbookFilter? handbookFilter = null, TimeSpan? timeout = null)
    {
        var apiClient = Services.Api;
        var handbook = await apiClient.Handbooks.GetHandbook(handbookType, handbookFilter, timeout);
        return handbook;
    }
}
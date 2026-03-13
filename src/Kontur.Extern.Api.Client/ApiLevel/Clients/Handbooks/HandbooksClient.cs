using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks;
using Kontur.Extern.Api.Client.Http;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Handbooks;

public class HandbooksClient : IHandbooksClient
{
    private readonly IHttpRequestFactory http;

    public HandbooksClient(IHttpRequestFactory http) => this.http = http;

    public async Task<ControlUnitsPage> GetControlUnits(ControlUnitsFilter? filter, TimeSpan? timeout = null)
    {
        filter ??= new ControlUnitsFilter();
        var url = new RequestUrlBuilder("/v1/handbooks/control-units")
            .AppendToQuery("type", filter.Type)
            .AppendToQuery("region", filter.Region)
            .AppendToQuery("take", filter.Take)
            .AppendToQuery("skip", filter.Skip)
            .AppendToQuery("includeinactive", filter.IncludeInactive);

        var uri = url.Build();
        var controlUnits = await http.GetAsync<ControlUnitsPage>(uri, timeout, $"{nameof(HandbooksClient)}.{nameof(GetControlUnits)}").ConfigureAwait(false);
        return controlUnits;
    }

    public async Task<ControlUnit> GetControlUnit(string code, TimeSpan? timeout = null)
    {
        return await GetControlUnit(code, null, timeout);
    }

    public async Task<ControlUnit> GetControlUnit(string code, AmbiguousControlUnitType? controlUnitType, TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder($"/v1/handbooks/control-units/{code}")
            .AppendToQuery("controlUnitType", controlUnitType).Build();
        var callingMethod = $"{nameof(HandbooksClient)}.{nameof(GetControlUnit)}";
        var controlUnit = await http.GetAsync<ControlUnit>(url, timeout, callingMethod).ConfigureAwait(false);
        return controlUnit;
    }

    public async Task<FnsFormsPage> GetFnsForms(FnsFormsFilter? filter, TimeSpan? timeout = null)
    {
        filter ??= new FnsFormsFilter();
        var url = new RequestUrlBuilder("/v1/handbooks/fns-forms")
            .AppendToQuery("knd", filter.Knd)
            .AppendToQuery("skip", filter.Skip)
            .AppendToQuery("take", filter.Take)
            .Build();
        var callingMethod = $"{nameof(HandbooksClient)}.{nameof(GetFnsForms)}";
        var fnsForms = await http.GetAsync<FnsFormsPage>(url, timeout, callingMethod);
        return fnsForms;
    }

    public async Task<HandbookPage> GetHandbook(HandbookType handbookType, HandbookFilter? handbookFilter = null, TimeSpan? timeout = null)
    {
        handbookFilter ??= new HandbookFilter();
        var url = new RequestUrlBuilder($"/v1/handbooks/{handbookType}")
            .AppendToQuery("take", handbookFilter.Take)
            .AppendToQuery("skip", handbookFilter.Skip).Build();
        var callingMethod = $"{nameof(HandbooksClient)}.{nameof(GetHandbook)}";
        var handbook = await http.GetAsync<HandbookPage>(url, timeout, callingMethod);
        return handbook;
    }
}
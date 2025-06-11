using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
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
        var controlUnits = await http.GetAsync<ControlUnitsPage>(uri);
        return controlUnits;
    }

    public async Task<ControlUnit> GetControlUnit(string code, AmbiguousUnitType? unitType = null, TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder($"/v1/handbooks/control-units/{code}")
            .AppendToQuery("unitType", unitType).Build();
        var controlUnit = await http.GetAsync<ControlUnit>(url);
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
        var fnsForms = await http.GetAsync<FnsFormsPage>(url);
        return fnsForms;
    }
}
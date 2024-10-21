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

    public async Task<ControlUnitsPage> GetControlUnits(ControlUnitsFilter? handbookFilter, TimeSpan? timeout = null)
    {
        handbookFilter ??= new ControlUnitsFilter();
        var url = new RequestUrlBuilder("/v1/handbooks/control-units")
            .AppendToQuery("type", handbookFilter.Type)
            .AppendToQuery("region", handbookFilter.Region)
            .AppendToQuery("take", handbookFilter.Take)
            .AppendToQuery("skip", handbookFilter.Skip)
            .AppendToQuery("includeinactive", handbookFilter.IncludeInactive);

        var uri = url.Build();
        var controlUnits = await http.GetAsync<ControlUnitsPage>(uri);
        return controlUnits;
    }

    public async Task<ControlUnitsPageItem> GetControlUnit(string code, TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder($"/v1/handbooks/control-units/{code}").Build();
        var controlUnit = await http.GetAsync<ControlUnitsPageItem>(url);
        return controlUnit;
    }

    public async Task<FnsFormsPage> GetFnsForms(FnsFormsFilter? fnsFormsFilter, TimeSpan? timeout = null)
    {
        fnsFormsFilter ??= new FnsFormsFilter();
        var url = new RequestUrlBuilder("/v1/handbooks/fns-forms")
            .AppendToQuery("knd", fnsFormsFilter.Knd)
            .AppendToQuery("skip", fnsFormsFilter.Skip)
            .AppendToQuery("take", fnsFormsFilter.Take)
            .Build();
        var fnsForms = await http.GetAsync<FnsFormsPage>(url);
        return fnsForms;
    }
}
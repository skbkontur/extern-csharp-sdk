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

    public async Task<ControlUnitsPage> GetControlUnits(ControlUnitsFilter? handbookFilter = null, TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder("/v1/handbooks/control-units");
        if (handbookFilter != null)
        {
            url.AppendToQuery("type", handbookFilter.Type);
            url.AppendToQuery("region", handbookFilter.Region);
            url.AppendToQuery("take", handbookFilter.Take);
            url.AppendToQuery("skip", handbookFilter.Skip);
            url.AppendToQuery("includeinactive", handbookFilter.IncludeInactive);

        }

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

    public async Task<FnsFormsPage> GetFnsForms(int skip, int take, TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder("/v1/handbooks/fns-forms")
            .AppendToQuery("skip", skip)
            .AppendToQuery("take", take)
            .Build();
        var fnsForms = await http.GetAsync<FnsFormsPage>(url);
        return fnsForms;
    }
}
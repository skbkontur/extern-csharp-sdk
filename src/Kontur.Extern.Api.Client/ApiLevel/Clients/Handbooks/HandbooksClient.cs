using System;
using System.Collections.Generic;
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

    public async Task<List<ControlUnit>> GetControlUnits(HandbookFilter? handbookFilter = null, TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder("/v1/handbooks/controlUnits");
        if (handbookFilter != null)
        {
            foreach (var e in handbookFilter.Types)
                url.AppendToQuery("types", e);
            foreach (var e in handbookFilter.Regions)
                url.AppendToQuery("regions", e);
        }

        var uri = url.Build();
        var controlUnits = await http.GetAsync<List<ControlUnit>>(uri);
        return controlUnits;
    }

    public async Task<ControlUnit> GetControlUnit(string code, TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder($"/v1/handbooks/controlUnit/{code}").Build();
        var controlUnit = await http.GetAsync<ControlUnit>(url);
        return controlUnit;
    }

    public async Task<List<FnsForm>> GetFnsForms(TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder("/v1/handbooks/fnsForms").Build();
        var fnsForms = await http.GetAsync<List<FnsForm>>(url);
        return fnsForms;
    }
}
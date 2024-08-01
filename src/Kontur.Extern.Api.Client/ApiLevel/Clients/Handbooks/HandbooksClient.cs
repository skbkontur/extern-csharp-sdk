using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;
using Kontur.Extern.Api.Client.Http;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Handbooks;

public class HandbooksClient : IHandbooksClient
{
    private readonly IHttpRequestFactory http;

    public HandbooksClient(IHttpRequestFactory http) => this.http = http;
    
    public async Task<List<ControlUnit>> GetControlUnits(TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder("/v1/handbooks/controlUnits").Build();
        var controlUnits = await http.GetAsync<List<ControlUnit>>(url);
        return controlUnits;
    }

    public async Task<List<FnsForm>> GetFnsForms(TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder("/v1/handbooks/fnsForms").Build();
        var fnsForms = await http.GetAsync<List<FnsForm>>(url);
        return fnsForms;
    }
}
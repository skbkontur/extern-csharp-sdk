using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.ReportsTables;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.ReportsTables;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Models.ReportsTables;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.ReportsTables;

public class ReportsTablesClient : IReportsTablesClient
{
    private readonly IHttpRequestFactory http;

    public ReportsTablesClient(IHttpRequestFactory http)
    {
        this.http = http;
    }

    public Task<FormsList> GetFormsAsync(Guid accountId, Guid organizationId, bool? includeDeleted = false, TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder($"/v1/{accountId}/reports-tables/{organizationId}/forms")
            .AppendToQuery("includeDeleted", includeDeleted)
            .Build();
        return http.GetAsync<FormsList>(url, timeout);
    }

    public Task<ReportsTableList> GetReportsTablesAsync(
        Guid accountId,
        Guid[]? organizationIds = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        bool? includeDeleted = false,
        int? skip = null, int? take = null,
        TimeSpan? timeout = null)
    {
        var url = new RequestUrlBuilder($"/v1/{accountId}/reports-tables/search").Build();
        return http.PostAsync<SearchReportsRequest, ReportsTableList>(
            url, 
            new SearchReportsRequest
            {
                OrganizationIds = organizationIds,
                DateFrom = dateFrom,
                DateTo = dateTo,
                IncludeDeleted = includeDeleted,
                Skip = skip,
                Take = take,
            },
            timeout);
    }
}
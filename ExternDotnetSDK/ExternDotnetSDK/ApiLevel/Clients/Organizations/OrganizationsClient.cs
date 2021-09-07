#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Organizations;
using Kontur.Extern.Client.ApiLevel.Models.Responses.Organizations;
using Kontur.Extern.Client.Http;
using Vostok.Clusterclient.Core.Model;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Clients.Organizations
{
    //todo Сделать нормальные тесты для методов.
    public class OrganizationsClient : IOrganizationsClient
    {
        private readonly IHttpRequestsFactory http;

        public OrganizationsClient(IHttpRequestsFactory http) => this.http = http;

        public async Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId,
            string? inn = null,
            string? kpp = null,
            int skip = 0,
            int take = 1000,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/organizations")
                .AppendToQuery("inn", inn) 
                .AppendToQuery("kpp", kpp) 
                .AppendToQuery("skip", skip)
                .AppendToQuery("take", take)
                .Build();
            var response = await http.Get(url).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<OrganizationBatch>().ConfigureAwait(false);
        }

        public Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null) =>
            http.GetAsync<Organization>(
                $"/v1/{accountId}/organizations/{orgId}",
                timeout
            );

        public Task<Organization?> TryGetOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null) =>
            http.TryGetAsync<Organization>(
                $"/v1/{accountId}/organizations/{orgId}",
                timeout
            );

        public Task<Organization> UpdateOrganizationAsync(
            Guid accountId,
            Guid orgId,
            string newName,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<UpdateOrganizationRequest, Organization>(
                $"/v1/{accountId}/organizations/{orgId}",
                new UpdateOrganizationRequest {Name = newName},
                timeout
            );
        }

        public Task<Organization> CreateOrganizationAsync(
            Guid accountId,
            string inn,
            string? kpp,
            string name,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<CreateOrganizationRequest, Organization>(
                $"/v1/{accountId}/organizations",
                new CreateOrganizationRequest
                {
                    Inn = inn,
                    Kpp = kpp,
                    Name = name
                },
                timeout
            );
        }

        public Task<bool> DeleteOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null) =>
            http.TryDeleteAsync($"/v1/{accountId}/organizations/{orgId}", timeout);
    }
}
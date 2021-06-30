﻿using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Organizations;
using Kontur.Extern.Client.HttpLevel;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.ApiLevel.Clients.Organizations
{
    //todo Сделать нормальные тесты для методов.
    public class OrganizationsClient : IOrganizationsClient
    {
        private readonly IHttpRequestsFactory http;

        public OrganizationsClient(IHttpRequestsFactory http) => this.http = http;

        public async Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId,
            string inn = null,
            string kpp = null,
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
            return response.GetMessage<OrganizationBatch>();
        }

        public Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null) =>
            http.GetAsync<Organization>(
                $"/v1/{accountId}/organizations/{orgId}",
                timeout
            );

        public Task<Organization> UpdateOrganizationAsync(
            Guid accountId,
            Guid orgId,
            string newName,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<UpdateOrganizationRequestDto, Organization>(
                $"/v1/{accountId}/organizations/{orgId}",
                new UpdateOrganizationRequestDto {Name = newName},
                timeout
            );
        }

        public Task<Organization> CreateOrganizationAsync(
            Guid accountId,
            string inn,
            string kpp,
            string name,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<CreateOrganizationRequestDto, Organization>(
                $"/v1/{accountId}/organizations",
                new CreateOrganizationRequestDto
                {
                    Inn = inn,
                    Kpp = kpp,
                    Name = name
                },
                timeout
            );
        }

        public Task DeleteOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null) =>
            http.DeleteAsync($"/v1/{accountId}/organizations/{orgId}", timeout);
    }
}
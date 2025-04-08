﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Organizations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Organizations.ControlUnitSubscriptions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations.ControlUnitSubscriptions;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Models.Numbers;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Organizations
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OrganizationsClient : IOrganizationsClient
    {
        private readonly IHttpRequestFactory http;

        public OrganizationsClient(IHttpRequestFactory http) => this.http = http;

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
            Kpp? kpp,
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

        public async Task<OrganizationSedoSubscriptionResponse> SearchOrganizationControlUnitSubscriptionsAsync(
            Guid accountId,
            Guid orgId,
            SedoSubscriptionSearchRequest request,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/organizations/{orgId}/control-unit-subscriptions").Build();

            return await http.PostAsync<SedoSubscriptionSearchRequest, OrganizationSedoSubscriptionResponse>(url, request, timeout)
                .ConfigureAwait(false);
        }
    }
}
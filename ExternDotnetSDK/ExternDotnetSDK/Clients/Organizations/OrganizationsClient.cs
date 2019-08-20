using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Authentication;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.SendAsync;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Organizations;

namespace ExternDotnetSDK.Clients.Organizations
{
    public class OrganizationsClient : IOrganizationsClient
    {
        private readonly RequestFactory factory;

        public OrganizationsClient(ILogger logger, IHttpSender client, IAuthenticationProvider authenticationProvider) =>
            factory = new RequestFactory(logger, client, authenticationProvider);

        public async Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId,
            string inn = null,
            string kpp = null,
            int skip = 0,
            int take = 1000) =>
            await factory.SendRequestAsync<OrganizationBatch>(
                HttpMethod.Get,
                $"/v1/{accountId}/organizations",
                new Dictionary<string, object>
                {
                    ["inn"] = inn ?? string.Empty,
                    ["kpp"] = kpp ?? string.Empty,
                    ["skip"] = skip,
                    ["take"] = take
                });

        public async Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId) =>
            await factory.SendRequestAsync<Organization>(HttpMethod.Get, $"/v1/{accountId}/organizations/{orgId}");

        public async Task<Organization> UpdateOrganizationAsync(Guid accountId, Guid orgId, string newName) =>
            await factory.SendRequestAsync<Organization>(
                HttpMethod.Put,
                $"/v1/{accountId}/organizations/{orgId}",
                new UpdateOrganizationRequestDto {Name = newName});

        public async Task<Organization> CreateOrganizationAsync(Guid accountId, string inn, string kpp, string name) =>
            await factory.SendRequestAsync<Organization>(
                HttpMethod.Post,
                $"/v1/{accountId}/organizations",
                new CreateOrganizationRequestDto
                {
                    Inn = inn,
                    Kpp = kpp,
                    Name = name
                });

        public async Task DeleteOrganizationAsync(Guid accountId, Guid orgId) =>
            await factory.SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}/organizations/{orgId}");
    }
}
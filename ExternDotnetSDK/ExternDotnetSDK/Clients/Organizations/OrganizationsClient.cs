using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces.Logging;
using ExternDotnetSDK.Models.Organizations;

namespace ExternDotnetSDK.Clients.Organizations
{
    public class OrganizationsClient : IOrganizationsClient
    {
        private readonly InnerCommonClient client;

        public OrganizationsClient(ILogger logger, IRequestSender sender, IRequestFactory requestFactory) =>
            client = new InnerCommonClient(logger, sender, requestFactory);

        public async Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId,
            string inn = null,
            string kpp = null,
            int skip = 0,
            int take = 1000) =>
            await client.SendRequestAsync<OrganizationBatch>(
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
            await client.SendRequestAsync<Organization>(HttpMethod.Get, $"/v1/{accountId}/organizations/{orgId}");

        public async Task<Organization> UpdateOrganizationAsync(Guid accountId, Guid orgId, string newName) =>
            await client.SendRequestAsync<Organization>(
                HttpMethod.Put,
                $"/v1/{accountId}/organizations/{orgId}",
                contentDto: new UpdateOrganizationRequestDto {Name = newName});

        public async Task<Organization> CreateOrganizationAsync(Guid accountId, string inn, string kpp, string name) =>
            await client.SendRequestAsync<Organization>(
                HttpMethod.Post,
                $"/v1/{accountId}/organizations",
                contentDto: new CreateOrganizationRequestDto
                {
                    Inn = inn,
                    Kpp = kpp,
                    Name = name
                });

        public async Task DeleteOrganizationAsync(Guid accountId, Guid orgId) =>
            await client.SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}/organizations/{orgId}");
    }
}
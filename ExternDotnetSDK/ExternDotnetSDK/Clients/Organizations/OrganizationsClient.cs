using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Models.Organizations;

namespace Kontur.Extern.Client.Clients.Organizations
{
    //todo Сделать нормальные тесты для методов.
    public class OrganizationsClient : IOrganizationsClient
    {
        private readonly InnerCommonClient client;

        public OrganizationsClient(ILogger logger, IRequestSender requestSender) =>
            client = new InnerCommonClient(logger, requestSender);

        public async Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId,
            string inn = null,
            string kpp = null,
            int skip = 0,
            int take = 1000,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<OrganizationBatch>(
                HttpMethod.Get,
                $"/v1/{accountId}/organizations",
                new Dictionary<string, object>
                {
                    ["inn"] = inn ?? string.Empty,
                    ["kpp"] = kpp ?? string.Empty,
                    ["skip"] = skip,
                    ["take"] = take
                },
                timeout: timeout);

        public async Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Organization>(
                HttpMethod.Get,
                $"/v1/{accountId}/organizations/{orgId}",
                timeout: timeout);

        public async Task<Organization> UpdateOrganizationAsync(
            Guid accountId,
            Guid orgId,
            string newName,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Organization>(
                HttpMethod.Put,
                $"/v1/{accountId}/organizations/{orgId}",
                contentDto: new UpdateOrganizationRequestDto {Name = newName},
                timeout: timeout);

        public async Task<Organization> CreateOrganizationAsync(
            Guid accountId,
            string inn,
            string kpp,
            string name,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Organization>(
                HttpMethod.Post,
                $"/v1/{accountId}/organizations",
                contentDto: new CreateOrganizationRequestDto
                {
                    Inn = inn,
                    Kpp = kpp,
                    Name = name
                },
                timeout: timeout);

        public async Task DeleteOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}/organizations/{orgId}", timeout: timeout);
    }
}
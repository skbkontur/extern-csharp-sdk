using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Organizations;
using Refit;

namespace ExternDotnetSDK.Clients.Organizations
{
    public class OrganizationsClient : IOrganizationsClient
    {
        public IOrganizationClientRefit ClientRefit { get; }

        public OrganizationsClient(HttpClient client) => ClientRefit = RestService.For<IOrganizationClientRefit>(client);

        public async Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId,
            string inn = null,
            string kpp = null,
            int skip = 0,
            int take = 1000) =>
            await ClientRefit.GetAllOrganizationsAsync(accountId, inn, kpp, skip, take);

        public async Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId) =>
            await ClientRefit.GetOrganizationAsync(accountId, orgId);

        public async Task<Organization> UpdateOrganizationAsync(Guid accountId, Guid orgId, string newName)
        {
            var request = new UpdateOrganizationRequestDto {Name = newName};
            return await ClientRefit.UpdateOrganizationAsync(accountId, orgId, request);
        }

        public async Task<Organization> CreateOrganizationAsync(Guid accountId, string inn, string kpp, string name)
        {
            var request = new CreateOrganizationRequestDto
            {
                Inn = inn,
                Kpp = kpp,
                Name = name
            };
            return await ClientRefit.CreateOrganizationAsync(accountId, request);
        }

        public async Task DeleteOrganizationAsync(Guid accountId, Guid orgId) =>
            await ClientRefit.DeleteOrganizationAsync(accountId, orgId);
    }
}
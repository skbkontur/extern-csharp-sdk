using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Models.Organizations;

namespace ExternDotnetSDK.Clients.Organizations
{
    public interface IOrganizationsClient : IHttpClient
    {
        Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId,
            string inn = null,
            string kpp = null,
            int skip = 0,
            int take = 1000);

        Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId);
        Task<Organization> UpdateOrganizationAsync(Guid accountId, Guid orgId, string newName);
        Task<Organization> CreateOrganizationAsync(Guid accountId, string inn, string kpp, string name);
        Task DeleteOrganizationAsync(Guid accountId, Guid orgId);
    }
}
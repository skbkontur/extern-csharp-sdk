using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Organizations;
using Refit;

namespace ExternDotnetSDK.Clients.Organizations
{
    public interface IOrganizationClientRefit
    {
        [Get("/v1/{accountId}/organizations?inn={inn}&kpp={kpp}&skip={skip}&take={take}")]
        Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId, string inn = null, string kpp = null, int skip = 0, int take = 1000);

        [Get("/v1/{accountId}/organizations/{orgId}")]
        Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId);

        [Put("/v1/{accountId}/organizations/{orgId}")]
        Task<Organization> UpdateOrganizationAsync(Guid accountId, Guid orgId, [Body] UpdateOrganizationRequestDto request);

        [Post("/v1/{accountId}/organizations")]
        Task<Organization> CreateOrganizationAsync(Guid accountId, [Body] CreateOrganizationRequestDto request);

        [Delete("/v1/{accountId}/organizations/{orgId}")]
        Task DeleteOrganizationAsync(Guid accountId, Guid orgId);
    }
}
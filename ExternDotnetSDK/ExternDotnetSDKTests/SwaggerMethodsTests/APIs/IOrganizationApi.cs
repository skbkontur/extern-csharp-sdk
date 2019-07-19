using System.Threading.Tasks;
using ExternDotnetSDK.Organizations;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.APIs
{
    internal interface IOrganizationApi
    {
        [Get("/v1/{accountId}/organizations?inn={inn}&kpp={kpp}&skip={skip}&take={take}")]
        Task<OrganizationBatch> GetAllOrganizations(string accountId, string inn = null, string kpp = null, long skip = 0, long take = long.MaxValue);

        [Get("/v1/{accountId}/organizations/{orgId}")]
        Task<Organization> GetOrganization(string accountId, string orgId);

        [Put("/v1/{accountId}/organizations/{orgId}")]
        Task<Organization> UpdateOrganization(string accountId, string orgId, [Body] UpdateOrganizationRequestDto request);

        [Post("/v1/{accountId}/organizations")]//does not work yet
        Task<string> CreateOrganization(string accountId, [Body] CreateOrganizationRequestDto request);

        //[Delete("/v1/{accountId}/organizations/{orgId}")]//not tested yet
        //Task DeleteOrganization(string accountId, string orgId);
    }
}
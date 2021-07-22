#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Organizations;

namespace Kontur.Extern.Client.ApiLevel.Clients.Organizations
{
    public interface IOrganizationsClient
    {
        Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId,
            string? inn = null,
            string? kpp = null,
            int skip = 0,
            int take = 1000,
            TimeSpan? timeout = null);

        Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null);
        Task<Organization?> TryGetOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null);
        Task<Organization> UpdateOrganizationAsync(Guid accountId, Guid orgId, string newName, TimeSpan? timeout = null);
        Task<Organization> CreateOrganizationAsync(Guid accountId, string inn, string kpp, string name, TimeSpan? timeout = null);
        Task DeleteOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null);
    }
}
using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations.ControlUnitSubscriptions;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    public static class OrganizationPathExtension
    {
        public static Task<Organization> GetAsync(this in OrganizationPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Organizations.GetOrganizationAsync(path.AccountId, path.OrganizationId, timeout);
        }

        public static Task<Organization?> TryGetAsync(this in OrganizationPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Organizations.TryGetOrganizationAsync(path.AccountId, path.OrganizationId, timeout);
        }

        public static Task<bool> DeleteAsync(this in OrganizationPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Organizations.DeleteOrganizationAsync(path.AccountId, path.OrganizationId, timeout);
        }

        public static Task RenameAsync(this in OrganizationPath path, string name, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Organizations.UpdateOrganizationAsync(path.AccountId, path.OrganizationId, name, timeout);
        }

        public static Task<OrganizationSedoSubscriptionResponse> SearchOrganizationSedoSubscriptionsAsync(
            this in OrganizationPath path,
            string? registrationNumber,
            int skip = 0,
            int take = 100,
            TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Organizations.SearchOrganizationSedoSubscriptionsAsync(path.AccountId, path.OrganizationId, registrationNumber, skip, take, timeout);
        }
    }
}
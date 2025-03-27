using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Organizations.ControlUnitSubscriptions;
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

        public static Task<OrganizationSedoSubscriptionResponse> SearchOrganizationControlUnitSubscriptionsAsync(
            this in OrganizationPath path,
            SedoSubscriptionSearchRequest request,
            TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Organizations.SearchOrganizationControlUnitSubscriptionsAsync(path.AccountId, path.OrganizationId, request, timeout);
        }
    }
}
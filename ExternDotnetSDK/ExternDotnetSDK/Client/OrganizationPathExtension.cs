using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Organizations;
using Kontur.Extern.Client.Paths;

namespace Kontur.Extern.Client
{
    public static class OrganizationPathExtension
    {
        public static Task<Organization> GetAsync(this in OrganizationPath path)
        {
            var apiClient = path.Services.Api;
            return apiClient.Organizations.GetOrganizationAsync(path.AccountId, path.OrganizationId);
        }

        public static Task DeleteAsync(this in OrganizationPath path)
        {
            var apiClient = path.Services.Api;
            return apiClient.Organizations.DeleteOrganizationAsync(path.AccountId, path.OrganizationId);
        }

        public static Task Rename(this in OrganizationPath path, string name)
        {
            var apiClient = path.Services.Api;
            return apiClient.Organizations.UpdateOrganizationAsync(path.AccountId, path.OrganizationId, name);
        }
    }
}
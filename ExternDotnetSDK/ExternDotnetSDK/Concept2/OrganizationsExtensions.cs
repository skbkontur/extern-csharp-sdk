using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Accounts;
using Kontur.Extern.Client.Models.Organizations;

namespace Kontur.Extern.Client.Concept2
{
    internal static class OrganizationListContextExtension
    {
        public static Task<Organization> CreateAsync(this in OrganizationListPath path, string inn, string kpp, string name)
        {
            var apiClient = path.Services.ApiClient;
            return apiClient.Organizations.CreateOrganizationAsync(path.AccountId, inn, kpp, name);
        }
        
        public static IEntityList<Account> List(this in OrganizationListPath path, string inn = null, string kpp = null) => throw new NotImplementedException();
    }

    internal static class OrganizationContextExtension
    {
        public static Task<Organization> GetAsync(this in OrganizationPath path)
        {
            var apiClient = path.Services.ApiClient;
            return apiClient.Organizations.GetOrganizationAsync(path.AccountId, path.OrganizationId);
        }

        public static Task DeleteAsync(this in OrganizationPath path)
        {
            var apiClient = path.Services.ApiClient;
            return apiClient.Organizations.DeleteOrganizationAsync(path.AccountId, path.OrganizationId);
        }

        public static Task Rename(this in OrganizationPath path, string name)
        {
            var apiClient = path.Services.ApiClient;
            return apiClient.Organizations.UpdateOrganizationAsync(path.AccountId, path.OrganizationId, name);
        }
    }
}
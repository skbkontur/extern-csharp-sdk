using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Accounts;
using Kontur.Extern.Client.Models.Organizations;

namespace Kontur.Extern.Client.Concept2
{
    internal static class OrganizationListContextExtension
    {
        public static Task<Organization> CreateAsync(this in OrganizationListContext context, string inn, string kpp, string name)
        {
            var apiClient = context.Services.ApiClient;
            return apiClient.Organizations.CreateOrganizationAsync(context.AccountId, inn, kpp, name);
        }
        
        public static IEntityList<Account> List(this in OrganizationListContext context, string inn = null, string kpp = null) => throw new NotImplementedException();
    }

    internal static class OrganizationContextExtension
    {
        public static Task<Organization> GetAsync(this in OrganizationContext context)
        {
            var apiClient = context.Services.ApiClient;
            return apiClient.Organizations.GetOrganizationAsync(context.AccountId, context.OrganizationId);
        }

        public static Task DeleteAsync(this in OrganizationContext context)
        {
            var apiClient = context.Services.ApiClient;
            return apiClient.Organizations.DeleteOrganizationAsync(context.AccountId, context.OrganizationId);
        }

        public static Task Rename(this in OrganizationContext context, string name)
        {
            var apiClient = context.Services.ApiClient;
            return apiClient.Organizations.UpdateOrganizationAsync(context.AccountId, context.OrganizationId, name);
        }
    }
}
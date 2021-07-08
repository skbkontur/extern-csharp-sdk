using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.ApiLevel.Models.Organizations;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    public static class OrganizationListPathExtension
    {
        public static Task<Organization> CreateAsync(this in OrganizationListPath path, string inn, string kpp, string name)
        {
            var apiClient = path.Services.Api;
            return apiClient.Organizations.CreateOrganizationAsync(path.AccountId, inn, kpp, name);
        }
        
        public static IEntityList<Account> List(this in OrganizationListPath path, string inn = null, string kpp = null) => throw new NotImplementedException();
    }
}
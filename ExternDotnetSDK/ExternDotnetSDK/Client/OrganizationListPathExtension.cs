using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Responses.Organizations;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Models.Numbers;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    public static class OrganizationListPathExtension
    {
        public static Task<Organization> AddIndividualEntrepreneurOrganizationAsync(this in OrganizationListPath path, Inn inn, string name, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(name);
            var apiClient = path.Services.Api;
            return apiClient.Organizations.CreateOrganizationAsync(path.AccountId, inn.ToString(), null, name, timeout);
        }
        
        public static Task<Organization> AddLegalEntityOrganizationAsync(this in OrganizationListPath path, LegalEntityInn inn, Kpp kpp, string name, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(name);
            
            var apiClient = path.Services.Api;
            return apiClient.Organizations.CreateOrganizationAsync(path.AccountId, inn.ToString(), kpp.ToString(), name, timeout);
        }
        
        public static IEntityList<Organization> List(this in OrganizationListPath path, string inn = null, string kpp = null)
        {
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            return new EntityList<Organization>(async (skip, take, timeout) =>
            {
                int skipValue;
                int takeValue;
                checked
                {
                    skipValue = (int) skip;
                    takeValue = (int) take;
                }

                var organizationBatch = await apiClient.Organizations.GetAllOrganizationsAsync(accountId, inn, kpp, skipValue, takeValue, timeout).ConfigureAwait(false);

                return (organizationBatch.Organizations, organizationBatch.TotalCount);
            });
        }
    }
}
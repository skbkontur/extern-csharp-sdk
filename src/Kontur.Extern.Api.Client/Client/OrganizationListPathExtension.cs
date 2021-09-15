using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client
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
        
        public static IEntityList<Organization> List(this in OrganizationListPath path, string? inn = null, string? kpp = null)
        {
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            return new EntityList<Organization>(async (skip, take, timeout) =>
            {
                int skipValue;
                checked
                {
                    skipValue = (int) skip;
                }

                var organizationBatch = await apiClient.Organizations.GetAllOrganizationsAsync(accountId, inn, kpp, skipValue, take, timeout).ConfigureAwait(false);

                return (organizationBatch.Organizations, organizationBatch.TotalCount);
            });
        }
    }
}
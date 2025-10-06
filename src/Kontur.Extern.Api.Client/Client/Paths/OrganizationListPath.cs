using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ClientDocumentationSection]
    public readonly struct OrganizationListPath
    {
        public OrganizationListPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public OrganizationPath WithId(Guid organizationId) => new(AccountId, organizationId, Services);

        public  Task<Organization> AddIndividualEntrepreneurOrganizationAsync(Inn inn, string name, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(name);
            var apiClient = Services.Api;
            return apiClient.Organizations.CreateOrganizationAsync(AccountId, inn.ToString(), null, name, timeout);
        }

        public  Task<Organization> AddLegalEntityOrganizationAsync(LegalEntityInn inn, Kpp kpp, string name, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(name);

            var apiClient = Services.Api;
            return apiClient.Organizations.CreateOrganizationAsync(AccountId, inn.ToString(), kpp, name, timeout);
        }

        public  IEntityList<Organization> List(string? inn = null, string? kpp = null)
        {
            var apiClient = Services.Api;
            var accountId = AccountId;
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
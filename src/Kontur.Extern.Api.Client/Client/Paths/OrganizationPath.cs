using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Organizations.ControlUnitSubscriptions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations.ControlUnitSubscriptions;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ClientDocumentationSection]
    public readonly struct OrganizationPath
    {
        public OrganizationPath(Guid accountId, Guid organizationId, IExternClientServices services)
        {
            AccountId = accountId;
            OrganizationId = organizationId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid OrganizationId { get; }
        public IExternClientServices Services { get; }

        public Task<Organization> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Organizations.GetOrganizationAsync(AccountId, OrganizationId, timeout);
        }

        public Task<Organization?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Organizations.TryGetOrganizationAsync(AccountId, OrganizationId, timeout);
        }

        public Task<bool> DeleteAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Organizations.DeleteOrganizationAsync(AccountId, OrganizationId, timeout);
        }

        public Task RenameAsync(string name, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Organizations.UpdateOrganizationAsync(AccountId, OrganizationId, name, timeout);
        }

        public Task<OrganizationSedoSubscriptionResponse> SearchOrganizationControlUnitSubscriptionsAsync(
            SedoSubscriptionSearchRequest request,
            TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Organizations.SearchOrganizationControlUnitSubscriptionsAsync(AccountId, OrganizationId, request, timeout);
        }
    }
}
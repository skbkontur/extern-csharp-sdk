using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations.ControlUnitSubscriptions;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestContext
{
    internal class OrganizationTestContext
    {
        private readonly IExtern konturExtern;
        private readonly EntityScopeFactory<Organization> scopeFactory;

        public OrganizationTestContext(IExtern konturExtern, EntityScopeFactory<Organization> scopeFactory)
        {
            this.konturExtern = konturExtern;
            this.scopeFactory = scopeFactory;
        }

        public ValueTask<EntityScope<Organization>> AddIndividualEntrepreneurOrganization(Guid accountId, Inn inn, string name) =>
            scopeFactory(
                () => konturExtern.Accounts.WithId(accountId).Organizations.AddIndividualEntrepreneurOrganizationAsync(inn, name),
                organization => konturExtern.Accounts.WithId(accountId).Organizations.WithId(organization.Id).DeleteAsync()
            );

        public ValueTask<EntityScope<Organization>> AddLegalEntityOrganization(Guid accountId, LegalEntityInn inn, Kpp kpp, string name) =>
            scopeFactory(
                () => konturExtern.Accounts.WithId(accountId).Organizations.AddLegalEntityOrganizationAsync(inn, kpp, name),
                organization => konturExtern.Accounts.WithId(accountId).Organizations.WithId(organization.Id).DeleteAsync()
            );

        public Task<Organization> GetOrganization(Guid accountId, Guid organizationId) =>
            konturExtern.Accounts.WithId(accountId).Organizations.WithId(organizationId).GetAsync();

        public Task<Organization?> GetOrganizationOrNull(Guid accountId, Guid organizationId) =>
            konturExtern.Accounts.WithId(accountId).Organizations.WithId(organizationId).TryGetAsync();

        public Task<IReadOnlyList<Organization>> LoadAll(Guid accountId) =>
            konturExtern.Accounts.WithId(accountId).Organizations.List().SliceBy(100).LoadAllAsync();

        public Task<IReadOnlyList<Organization>> FilterByInn(Guid accountId, string inn) =>
            konturExtern.Accounts.WithId(accountId).Organizations.List(inn: inn).SliceBy(100).LoadAllAsync();

        public Task<IReadOnlyList<Organization>> FilterByInnKpp(Guid accountId, string inn, string kpp) =>
            konturExtern.Accounts.WithId(accountId).Organizations.List(inn, kpp).SliceBy(100).LoadAllAsync();

        public Task<long> Count(Guid accountId, string? inn = null) =>
            konturExtern.Accounts.WithId(accountId).Organizations.List(inn).CountAsync();

        public Task Rename(Guid accountId, Guid organizationId, string newName) =>
            konturExtern.Accounts.WithId(accountId).Organizations.WithId(organizationId).RenameAsync(newName);

        public Task<OrganizationSedoSubscriptionResponse> SearchOrganizationSedoSubscriptions(Guid accountId, Guid organizationId, string? registrationNumber = null) =>
            konturExtern.Accounts.WithId(accountId).Organizations.WithId(organizationId).SearchOrganizationSedoSubscriptionsAsync(registrationNumber);
    }
}
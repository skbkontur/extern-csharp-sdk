using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations;
using Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Testing.Generators;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client
{
    public class OrganizationPathsExtensions_Tests : GeneratedAccountTests
    {
        private readonly AuthoritiesCodesGenerator codesGenerator;

        public OrganizationPathsExtensions_Tests(ITestOutputHelper output, IsolatedAccountEnvironment environment)
            : base(output, environment)
        {
            codesGenerator = new AuthoritiesCodesGenerator();
        }

        [Fact]
        public async Task Get_should_fail_if_the_organization_is_not_exist()
        {
            Func<Task> func = async () => await Context.Organizations.GetOrganization(AccountId, Guid.NewGuid());

            await func.Should().ThrowAsync<ApiException>();
        }

        [Fact]
        public async Task TryGet_should_return_null_if_the_organization_is_not_exist()
        {
            var organization = await Context.Organizations.GetOrganizationOrNull(AccountId, Guid.NewGuid());

            organization.Should().BeNull();
        }

        [Fact]
        public async Task Should_add_a_new_org_of_individual_entrepreneur()
        {
            await using var organizationScope = await Context.Organizations
                .AddIndividualEntrepreneurOrganization(AccountId, codesGenerator.PersonInn(), "the org");
            var organization = organizationScope.Entity;

            var loadedOrganization = await Context.Organizations.GetOrganization(AccountId, organization.Id);
            
            loadedOrganization.Should().BeEquivalentTo(organization);
        }

        [Fact]
        public async Task Should_add_a_new_org_of_legal_entity()
        {
            await using var organizationScope = await Context.Organizations
                .AddLegalEntityOrganization(AccountId, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org");
            var organization = organizationScope.Entity;

            var loadedOrganization = await Context.Organizations.GetOrganization(AccountId, organization.Id);
            
            loadedOrganization.Should().BeEquivalentTo(organization);
        }

        [Fact]
        public async Task TryGet_should_not_load_deleted_organization()
        {
            Organization createdOrganization;
            await using(var organizationScope = await Context.Organizations
                .AddLegalEntityOrganization(AccountId, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org"))
            {
                createdOrganization = organizationScope.Entity;
                var loadedOrganization = await Context.Organizations.GetOrganization(AccountId, createdOrganization.Id);
                ShouldBeEqual(loadedOrganization, createdOrganization);
            }
         
            var loadedOrgAfterDelete = await Context.Organizations.GetOrganizationOrNull(AccountId, createdOrganization.Id);
            loadedOrgAfterDelete.Should().BeNull();
        }

        [Fact]
        public async Task List_should_return_default_organization_of_the_account()
        {
            var organizationsOfAccount = await Context.Organizations.LoadAll(AccountId);

            organizationsOfAccount.Should().HaveCount(1);
            var organization = organizationsOfAccount.First().General;
            organization.Inn.Should().Be(GeneratedAccount.Inn.ToString());
            organization.Kpp.Should().Be(GeneratedAccount.Kpp.ToString());
            organization.IsMainOrg.Should().BeTrue();

        }

        [Fact]
        public async Task List_should_load_all_organizations_of_the_account()
        {
            await using var organizationScope1 = await Context.Organizations
                .AddIndividualEntrepreneurOrganization(AccountId, codesGenerator.PersonInn(), "the org");
            await using var organizationScope2 = await Context.Organizations
                .AddLegalEntityOrganization(AccountId, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org");
            
            var organizationsOfAccount = await Context.Organizations.LoadAll(AccountId);
            
            organizationsOfAccount.Should().HaveCount(3);
          
            ShouldContainOrganization(organizationsOfAccount, organizationScope1.Entity);
            ShouldContainOrganization(organizationsOfAccount, organizationScope2.Entity);
        }

        [Fact]
        public async Task List_should_not_load_deleted_organization()
        {
            Organization createdOrganization;
            await using(var organizationScope = await Context.Organizations
                .AddLegalEntityOrganization(AccountId, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org"))
            {
                createdOrganization = organizationScope.Entity;
                var organizationsAfterCreate = await Context.Organizations.LoadAll(AccountId);
                ShouldContainOrganization(organizationsAfterCreate, createdOrganization);
            }
         
            var organizationsAfterDelete = await Context.Organizations.LoadAll(AccountId);
            ShouldNotContainOrganization(organizationsAfterDelete, createdOrganization);
        }

        [Fact]
        public async Task List_should_filter_organization_by_inn()
        {
            var personInn = codesGenerator.PersonInn();
            await using var organizationScope1 = await Context.Organizations
                .AddIndividualEntrepreneurOrganization(AccountId, personInn, "the org");
            await using var organizationScope2 = await Context.Organizations
                .AddLegalEntityOrganization(AccountId, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org");
            
            var organizationsOfAccount = await Context.Organizations.FilterByInn(AccountId, personInn.ToString());
            
            organizationsOfAccount.Should().HaveCount(1);
            organizationsOfAccount.Select(x => x.Id).Should().Contain(organizationScope1.Entity.Id);
        }

        [Fact]
        public async Task List_should_filter_organization_by_inn_and_kpp()
        {
            await using var organizationScope1 = await Context.Organizations
                .AddIndividualEntrepreneurOrganization(AccountId, codesGenerator.PersonInn(), "the org");
            var legalEntityInn = codesGenerator.LegalEntityInn();
            await using var organizationScope2 = await Context.Organizations
                .AddLegalEntityOrganization(AccountId, legalEntityInn, codesGenerator.Kpp(), "the org");
            await using var organizationScope3 = await Context.Organizations
                .AddLegalEntityOrganization(AccountId, legalEntityInn, codesGenerator.Kpp(), "the org");
            
            var organizationsOfAccount = await Context.Organizations
                .FilterByInnKpp(AccountId, legalEntityInn.ToString(), organizationScope2.Entity.General.Kpp);
            
            organizationsOfAccount.Should().HaveCount(1);
            organizationsOfAccount.Select(x => x.Id).Should().Contain(organizationScope2.Entity.Id);
        }

        [Fact]
        public async Task Should_rename_an_organization()
        {

            await using var organizationScope = await Context.Organizations
                .AddLegalEntityOrganization(AccountId, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org");

            var organization = organizationScope.Entity;
            const string newName = "the new name";
            var expectedOrganization = new Organization
            {
                Id = organization.Id,
                General = new OrganizationGeneral
                {
                    Inn = organization.General.Inn,
                    Kpp = organization.General.Kpp,
                    Name = newName,
                    IsMainOrg = organization.General.IsMainOrg,
                    Links = organization.General.Links
                }
            };
            
            await Context.Organizations.Rename(AccountId, organization.Id, newName);

            var loadedOrg = await Context.Organizations.GetOrganization(AccountId, organization.Id);
            ShouldBeEqual(loadedOrg, expectedOrganization);
        }

        private static void ShouldBeEqual(Organization organization, Organization expectedOrganization)
        {
            organization.Should().BeEquivalentTo(expectedOrganization);
        }

        private static void ShouldContainOrganization(IEnumerable<Organization> organizations, Organization expectedOrganization)
        {
            organizations.Should().ContainEquivalentOf(expectedOrganization, x => x.Excluding(o => o.General.Links));
        }

        private static void ShouldNotContainOrganization(IEnumerable<Organization> organizations, Organization expectedOrganization)
        {
            organizations.Select(x => x.Id).Should().NotContain(expectedOrganization.Id);
        }

        private object GetMainOrganizationOfTheAccount()
        {
            return new 
            {
                GeneratedAccount.Inn,
                IsMainOrg = true,
                GeneratedAccount.Kpp,
                Name = GeneratedAccount.OrganizationName,
                
            };
        }
    }
}
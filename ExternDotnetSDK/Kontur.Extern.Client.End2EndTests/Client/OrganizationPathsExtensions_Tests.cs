using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Organizations;
using Kontur.Extern.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Testing.Generators;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client
{
    public class OrganizationPathsExtensions_Tests : DefaultAccountPathsTests, IClassFixture<AuthoritiesCodesGenerator>
    {
        private readonly AuthoritiesCodesGenerator codesGenerator;

        public OrganizationPathsExtensions_Tests(ITestOutputHelper output, AuthoritiesCodesGenerator codesGenerator)
            : base(output)
        {
            this.codesGenerator = codesGenerator;
        }
        
        [Fact]
        public void Get_should_fail_if_the_organization_is_not_exist()
        {
            Func<Task> func = async () => await Context.Organizations.GetOrganization(DefaultAccount.Id, Guid.NewGuid());

            func.Should().Throw<ApiException>();
        }

        [Fact]
        public async Task TryGet_should_return_null_if_the_organization_is_not_exist()
        {
            var organization = await Context.Organizations.GetOrganizationOrNull(DefaultAccount.Id, Guid.NewGuid());

            organization.Should().BeNull();
        }

        [Fact]
        public async Task Should_add_a_new_org_of_individual_entrepreneur()
        {
            await using var organizationScope = await Context.Organizations
                .AddIndividualEntrepreneurOrganization(DefaultAccount.Id, codesGenerator.PersonInn(), "the org");
            var organization = organizationScope.Entity;

            var loadedOrganization = await Context.Organizations.GetOrganization(DefaultAccount.Id, organization.Id);
            
            loadedOrganization.Should().BeEquivalentTo(organization);
        }

        [Fact]
        public async Task Should_add_a_new_org_of_legal_entity()
        {
            await using var organizationScope = await Context.Organizations
                .AddLegalEntityOrganization(DefaultAccount.Id, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org");
            var organization = organizationScope.Entity;

            var loadedOrganization = await Context.Organizations.GetOrganization(DefaultAccount.Id, organization.Id);
            
            loadedOrganization.Should().BeEquivalentTo(organization);
        }

        [Fact]
        public async Task TryGet_should_not_load_deleted_organization()
        {
            Organization createdOrganization;
            await using(var organizationScope = await Context.Organizations
                .AddLegalEntityOrganization(DefaultAccount.Id, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org"))
            {
                createdOrganization = organizationScope.Entity;
                var loadedOrganization = await Context.Organizations.GetOrganization(DefaultAccount.Id, createdOrganization.Id);
                ShouldBeEqual(loadedOrganization, createdOrganization);
            }
         
            var loadedOrgAfterDelete = await Context.Organizations.GetOrganizationOrNull(DefaultAccount.Id, createdOrganization.Id);
            loadedOrgAfterDelete.Should().BeNull();
        }

        [Fact]
        public async Task List_should_return_default_organization_of_the_account()
        {
            var expectedOrganization = GetMainOrganizationOfTheAccount();
            var organizationsOfAccount = await Context.Organizations.LoadAll(DefaultAccount.Id);

            organizationsOfAccount.Should().HaveCount(1);
            organizationsOfAccount[0].General.Should().BeEquivalentTo(expectedOrganization);
        }

        [Fact]
        public async Task List_should_load_all_organizations_of_the_account()
        {
            await using var organizationScope1 = await Context.Organizations
                .AddIndividualEntrepreneurOrganization(DefaultAccount.Id, codesGenerator.PersonInn(), "the org");
            await using var organizationScope2 = await Context.Organizations
                .AddLegalEntityOrganization(DefaultAccount.Id, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org");
            
            var organizationsOfAccount = await Context.Organizations.LoadAll(DefaultAccount.Id);
            
            organizationsOfAccount.Should().HaveCount(3);
            organizationsOfAccount.Select(x => x.General).Should().ContainEquivalentOf(GetMainOrganizationOfTheAccount());
            ShouldContainOrganization(organizationsOfAccount, organizationScope1.Entity);
            ShouldContainOrganization(organizationsOfAccount, organizationScope2.Entity);
        }

        [Fact]
        public async Task List_should_not_load_deleted_organization()
        {
            Organization createdOrganization;
            await using(var organizationScope = await Context.Organizations
                .AddLegalEntityOrganization(DefaultAccount.Id, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org"))
            {
                createdOrganization = organizationScope.Entity;
                var organizationsAfterCreate = await Context.Organizations.LoadAll(DefaultAccount.Id);
                ShouldContainOrganization(organizationsAfterCreate, createdOrganization);
            }
         
            var organizationsAfterDelete = await Context.Organizations.LoadAll(DefaultAccount.Id);
            ShouldNotContainOrganization(organizationsAfterDelete, createdOrganization);
        }

        [Fact]
        public async Task List_should_filter_organization_by_inn()
        {
            var personInn = codesGenerator.PersonInn();
            await using var organizationScope1 = await Context.Organizations
                .AddIndividualEntrepreneurOrganization(DefaultAccount.Id, personInn, "the org");
            await using var organizationScope2 = await Context.Organizations
                .AddLegalEntityOrganization(DefaultAccount.Id, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org");
            
            var organizationsOfAccount = await Context.Organizations.FilterByInn(DefaultAccount.Id, personInn.ToString());
            
            organizationsOfAccount.Should().HaveCount(1);
            organizationsOfAccount.Select(x => x.Id).Should().Contain(organizationScope1.Entity.Id);
        }

        [Fact]
        public async Task List_should_filter_organization_by_inn_and_kpp()
        {
            await using var organizationScope1 = await Context.Organizations
                .AddIndividualEntrepreneurOrganization(DefaultAccount.Id, codesGenerator.PersonInn(), "the org");
            var legalEntityInn = codesGenerator.LegalEntityInn();
            await using var organizationScope2 = await Context.Organizations
                .AddLegalEntityOrganization(DefaultAccount.Id, legalEntityInn, codesGenerator.Kpp(), "the org");
            await using var organizationScope3 = await Context.Organizations
                .AddLegalEntityOrganization(DefaultAccount.Id, legalEntityInn, codesGenerator.Kpp(), "the org");
            
            var organizationsOfAccount = await Context.Organizations
                .FilterByInnKpp(DefaultAccount.Id, legalEntityInn.ToString(), organizationScope2.Entity.General.Kpp);
            
            organizationsOfAccount.Should().HaveCount(1);
            organizationsOfAccount.Select(x => x.Id).Should().Contain(organizationScope2.Entity.Id);
        }

        [Fact]
        public async Task Should_rename_an_organization()
        {
            await using var organizationScope = await Context.Organizations
                .AddLegalEntityOrganization(DefaultAccount.Id, codesGenerator.LegalEntityInn(), codesGenerator.Kpp(), "the org");

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
            
            await Context.Organizations.Rename(DefaultAccount.Id, organization.Id, newName);

            var loadedOrg = await Context.Organizations.GetOrganization(DefaultAccount.Id, organization.Id);
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
                DefaultAccount.Inn,
                DefaultAccount.Kpp,
                Name = DefaultAccount.OrganizationName,
                IsMainOrg = true
            };
        }
    }
}
using System;
using System.Threading.Tasks;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.End2EndTests.Client.TestAbstractions;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Model.Numbers;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Client
{
    public class OrganizationPathsExtensions_Tests : DefaultAccountPathsTests
    {
        public OrganizationPathsExtensions_Tests([NotNull] ITestOutputHelper output)
            : base(output)
        {
        }
        
        [Fact]
        public void Get_should_fail_if_the_organization_is_not_exist()
        {
            Func<Task> func = async () => await Context.Organizations.GetOrganization(DefaultAccount.Id, Guid.Parse("6A4F8D06-1CBC-4E63-BC1F-DB5AD91A720D"));

            func.Should().Throw<ApiException>();
        }

        [Fact]
        public async Task TryGet_should_return_null_if_the_organization_is_not_exist()
        {
            var organization = await Context.Organizations.GetOrganizationOrNull(DefaultAccount.Id, Guid.Parse("6A4F8D06-1CBC-4E63-BC1F-DB5AD91A720D"));

            organization.Should().BeNull();
        }

        [Fact(Skip = "somehow wrong")]
        public async Task Should_add_a_new_org_of_individual_entrepreneur()
        {
            await using var organizationScope = await Context.Organizations
                .AddIndividualEntrepreneurOrganization(DefaultAccount.Id, Inn.Parse("678050110389"), "the org");
            var organization = organizationScope.Entity;

            var loadedOrganization = await Context.Organizations.GetOrganization(DefaultAccount.Id, organization.Id);
            
            loadedOrganization.Should().BeEquivalentTo(organization);
        }

        [Fact(Skip = "somehow wrong")]
        public async Task Should_add_a_new_org_of_legal_entity()
        {
            await using var organizationScope = await Context.Organizations
                .AddLegalEntityOrganization(DefaultAccount.Id, LegalEntityInn.Parse("2015459048"), Kpp.Parse("442243862"), "the org");
            var organization = organizationScope.Entity;

            var loadedOrganization = await Context.Organizations.GetOrganization(DefaultAccount.Id, organization.Id);
            
            loadedOrganization.Should().BeEquivalentTo(organization);
        }

        [Fact]
        public async Task List_should_return_default_organization_of_the_account()
        {
            var expectedOrganization = new
            {
                DefaultAccount.Inn,
                DefaultAccount.Kpp,
                Name = DefaultAccount.OrganizationName,
                IsMainOrg = true
            };
            var organizationsOfAccount = await Context.Organizations.LoadAll(DefaultAccount.Id);

            organizationsOfAccount.Should().HaveCount(1);
            organizationsOfAccount[0].General.Should().BeEquivalentTo(expectedOrganization);
        }
    }
}
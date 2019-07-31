using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Organizations;
using ExternDotnetSDK.Organizations;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class OrganizationsClientShould : AllTestsShould
    {
        protected OrganizationsClient OrgClient;
        protected Organization Organization;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            OrgClient = new OrganizationsClient(Client);
            Organization = (await OrgClient.SearchOrganizationsAsync(Account.Id)).Organizations[0];
        }

        [TestCase]
        [TestCase("0606257678")]
        [TestCase("0606257678", "671145475")]
        public void GetOrganizations_WithValidParameters(string inn = null, string kpp = null, int skip = 0, int take = 1000)
        {
            Assert.DoesNotThrowAsync(
                async () => await OrgClient.SearchOrganizationsAsync(Account.Id, inn, kpp, skip, take));
        }

        [TestCase(null, null, 0, 0)]
        [TestCase(null, null, -1)]
        [TestCase(null, "not a kpp")]
        [TestCase("not an inn")]
        public void GetNoOrganizations_WithBadParameters(string inn = null, string kpp = null, int skip = 0, int take = 1000)
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await OrgClient.SearchOrganizationsAsync(Account.Id, inn, kpp, skip, take));
        }

        [Test]
        public void GetNoOrganizations_WithNonexistentAccount()
        {
            Assert.ThrowsAsync<ApiException>(
                async () =>
                    await OrgClient.SearchOrganizationsAsync(Guid.Empty));
        }

        [Test]
        public void GetAnOrganization_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(
                async () =>
                    await OrgClient.GetOrganizationAsync(Account.Id, Organization.Id));
        }

        [Test]
        public void FailToGetAnOrganization_WithBadParameters()
        {
            Assert.ThrowsAsync<ApiException>(
                async () =>
                    await OrgClient.GetOrganizationAsync(Account.Id, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(
                async () =>
                    await OrgClient.GetOrganizationAsync(Guid.Empty, Organization.Id));
        }

        [Test]
        public void UpdateOrganizationName()
        {
            Assert.DoesNotThrowAsync(
                async () =>
                    await OrgClient.UpdateOrganizationAsync(Account.Id, Organization.Id, Organization.General.Name));
        }

        [Test]
        public void FailToCreateNewOrganization_WithoutAccess()
        {
            Assert.ThrowsAsync<ApiException>(
                async () =>
                    await OrgClient.CreateOrganizationAsync(Account.Id, "9194113617", "335544394", "Good name"));
        }

        [Test]
        public void FailToDeleteOrganization_WithBadAccountId()
        {
            Assert.ThrowsAsync<ApiException>(
                async () =>
                    await OrgClient.DeleteOrganizationAsync(Guid.Empty, Organization.Id));
        }

        [Test]
        public void FailToDeleteOrganization_WithBadOrganizationId()
        {
            Assert.ThrowsAsync<ApiException>(
                async () =>
                    await OrgClient.DeleteOrganizationAsync(Account.Id, Guid.Empty));
        }
    }
}
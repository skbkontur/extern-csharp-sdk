using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Organizations;
using ExternDotnetSDK.Organizations;
using ExternDotnetSDKTests.SwaggerMethodsTests.Common;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class OrganizationsClientShould : AllTestsShould
    {
        private OrganizationsClient client;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            client = new OrganizationsClient(Client);
        }

        [TestCase]
        [TestCase("0606257678")]
        [TestCase("0606257678", "671145475")]
        public void GetOrganizations_WithValidParameters(string inn = null, string kpp = null, int skip = 0, int take = 1000)
        {
            var account = Data.FullAccountList.Accounts[0];
            Assert.DoesNotThrowAsync(async () => await client.SearchOrganizationsAsync(account.Id, inn, kpp, skip, take));
        }

        [TestCase(null, null, 0, 0)]
        [TestCase(null, null, -1)]
        [TestCase(null, "not a kpp")]
        [TestCase("not an inn")]
        public void GetNoOrganizations_WithBadParameters(string inn = null, string kpp = null, int skip = 0, int take = 1000)
        {
            var account = Data.FullAccountList.Accounts[0];
            Assert.ThrowsAsync<ApiException>(async () => await client.SearchOrganizationsAsync(account.Id, inn, kpp, skip, take));
        }

        [Test]
        public void GetNoOrganizations_WithNonexistentAccount()
        {
            Assert.ThrowsAsync<ApiException>(async () => await client.SearchOrganizationsAsync(Guid.Empty));
        }

        [Test]
        public void GetAnOrganization_WithValidParameters()
        {
            var accountId = Data.FullAccountList.Accounts[0].Id;
            var expected = Data.Organization;
            Assert.DoesNotThrowAsync(async () => await client.GetOrganizationAsync(accountId, expected.Id));
        }

        [Test]
        public void FailToGetAnOrganization_WithBadParameters()
        {
            var goodAccountId = Data.FullAccountList.Accounts[0].Id;
            var goodOrganizationId = Data.Organization.Id;
            Assert.ThrowsAsync<ApiException>(async () => await client.GetOrganizationAsync(goodAccountId, Guid.Empty));
            Assert.ThrowsAsync<ApiException>(async () => await client.GetOrganizationAsync(Guid.Empty, goodOrganizationId));
        }

        [Test]
        public void UpdateOrganizationName()
        {
            var accountId = Data.FullAccountList.Accounts[0].Id;
            var organizationId = Data.Organization.Id;
            var oldName = Data.Organization.General.Name;
            Assert.DoesNotThrowAsync(async () => await client.UpdateOrganizationAsync(accountId, organizationId, oldName));
        }

        [Test]
        public async Task CreateAndDeleteNewOrganization_WithValidParameters()
        {
            var authApi = RestService.For<IAuthApi>(Data.AuthAddress);
            var session = await authApi.ByPass("a776dbe1055b4", "testPassword", Data.ApiKey);
            var myClient = new HttpClient(new MyHttpClientHandler(Data.ApiKey, session.Sid, Data.BaseAddress))
            {
                BaseAddress = new Uri(Data.BaseAddress)
            };
            var testClient = new OrganizationsClient(myClient);
            const string inn = "9194113617";
            const string kpp = "335544394";
            const string name = "Good name";
            var accountId = new Guid("0b82e04d-b554-41d2-94cd-cda5e4e0015b");
            Organization result = null;
            Assert.DoesNotThrowAsync(async () =>
            {
                result = await testClient.CreateOrganizationAsync(accountId, inn, kpp, name);
                await testClient.DeleteOrganization(accountId, result.Id);
            });
        }

        [Test]
        public void FailToDeleteOrganization_WithBadAccountId()
        {
            var orgId = Data.Organization.Id;
            Assert.ThrowsAsync<ApiException>(async () => await client.DeleteOrganization(Guid.Empty, orgId));
        }

        [Test]
        public void FailToDeleteOrganization_WithBadOrganizationId()
        {
            var accountId = Data.FullAccountList.Accounts[0].Id;
            Assert.ThrowsAsync<ApiException>(async () => await client.DeleteOrganization(accountId, Guid.Empty));
        }
    }
}
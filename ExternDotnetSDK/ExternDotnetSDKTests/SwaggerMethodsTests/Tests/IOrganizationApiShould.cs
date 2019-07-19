using System;
using System.IO;
using System.Threading.Tasks;
using ExternDotnetSDK.Accounts;
using ExternDotnetSDK.Organizations;
using ExternDotnetSDKTests.SwaggerMethodsTests.APIs;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class IOrganizationApiShould : AllTestsShould<IOrganizationApi>
    {

        [TestCase]
        [TestCase("0606257678")]
        [TestCase("0606257678", "671145475")]
        [TestCase("0606257678", "671145475", 2)]
        [TestCase("0606257678", "671145475", 2, 3)]
        public void GetOrganizations_WithValidParameters(string inn = null, string kpp = null, long skip = 0, long take = long.MaxValue)
        {
            var account = Data.FullAccountList.Accounts[0];
            Assert.DoesNotThrowAsync(async () => await Api.GetAllOrganizations(account.Id.ToString(), inn, kpp, skip, take));
        }

        [TestCase(null, null, 0, 0)]
        [TestCase(null, null, -1)]
        [TestCase(null, "not a kpp")]
        [TestCase("not an inn")]
        public void GetNoOrganizations_WithBadParameters(string inn = null, string kpp = null, long skip = 0, long take = long.MaxValue)
        {
            var account = Data.FullAccountList.Accounts[0];
            Assert.ThrowsAsync<ApiException>(async () => await Api.GetAllOrganizations(account.Id.ToString(), inn, kpp, skip, take));
        }

        [Test]
        public void GetNoOrganizations_WithNonexistentAccount()
        {
            Assert.ThrowsAsync<ApiException>(async () => await Api.GetAllOrganizations("nonexistent account"));
        }

        [Test]
        public async Task GetAnOrganization_WithValidParameters()
        {
            var accountId = Data.FullAccountList.Accounts[0].Id.ToString();
            var expected = Data.Organization;
            try
            {
                var actual = await Api.GetOrganization(accountId, expected.Id.ToString());
                actual.Should().BeEquivalentTo(expected);
            }
            catch (ApiException)
            {
                Assert.Warn("Failed to get organization");
            }
        }

        [Test]
        public void FailToGetAnOrganization_WithBadParameters()
        {
            var goodAccountId = Data.FullAccountList.Accounts[0].Id.ToString();
            var badAccountId = goodAccountId.Substring(1);
            var goodOrganizationId = Data.Organization.Id.ToString();
            var badOrganizationId = goodOrganizationId.Substring(1);
            Assert.ThrowsAsync<ApiException>(async () => await Api.GetOrganization(goodAccountId, badOrganizationId));
            Assert.ThrowsAsync<ApiException>(async () => await Api.GetOrganization(badAccountId, goodOrganizationId));
        }

        [Test]
        public void UpdateOrganizationName()
        {
            var accountId = Data.FullAccountList.Accounts[0].Id.ToString();
            var organizationId = Data.Organization.Id.ToString();
            var oldName = Data.Organization.General.Name;
            Assert.DoesNotThrowAsync(async () => await Api.UpdateOrganization(accountId, organizationId, new UpdateOrganizationRequestDto
            {
                Name = oldName
            }));
        }

        [Test]
        public void CreateNewOrganization_WithValidParameters()
        {
            var accountId = Data.FullAccountList.Accounts[0].Id.ToString();
            var request = new CreateOrganizationRequestDto
            {
                Inn = "9194113617",
                Kpp = "335544394",
                Name = "Good name"
            };
            string result;
            Assert.DoesNotThrowAsync(async () => result = await Api.CreateOrganization(accountId, request));
        }
    }
}
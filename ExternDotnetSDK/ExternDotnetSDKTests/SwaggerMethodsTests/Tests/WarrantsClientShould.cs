using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Warrants;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class WarrantsClientShould : AllTestsShould
    {
        private WarrantsClient warrantsClient;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            warrantsClient = new WarrantsClient(Client);
        }

        [Test]
        public void FailToGetWarrants_WithBadAccountId()
        {
            Assert.ThrowsAsync<ApiException>(async () => await warrantsClient.GetWarrantsAsync(Guid.NewGuid()));
        }

        [Test]
        public void GetWarrants_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await warrantsClient.GetWarrantsAsync(Account.Id));
        }

        [TestCase(-1)]
        [TestCase(0, 0)]
        [TestCase(0, -1)]
        public void FailToGetWarrants_WithBadQueryParameters(int skip = 0, int take = int.MaxValue)
        {
            Assert.ThrowsAsync<ApiException>(async () => await warrantsClient.GetWarrantsAsync(Account.Id, skip, take));
        }
    }
}
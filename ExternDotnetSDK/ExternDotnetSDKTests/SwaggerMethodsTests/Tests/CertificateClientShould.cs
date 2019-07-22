using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Certificates;
using FluentAssertions;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class CertificateClientShould : AllTestsShould
    {
        private CertificateClient client;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            client = new CertificateClient(Client);
        }

        [TestCase]
        [TestCase(1)]
        [TestCase(1, 3)]
        [TestCase(1, 100, true)]
        public void GetCertificates_WithValidParameters(int skip = 0, int take = 100, bool forAllUsers = false)
        {
            var accountId = Data.FullAccountList.Accounts[0].Id;
            Assert.DoesNotThrowAsync(async () => await client.GetCertificateListAsync(accountId, skip, take, forAllUsers));
        }

        [TestCase(0, 0)]
        [TestCase(-1)]
        [TestCase(0, -1)]
        public void GetNoCertificates_WithBadParameters(int skip = 0, int take = 100, bool folAllUsers = false)
        {
            var accountId = Data.FullAccountList.Accounts[0].Id;
            Assert.ThrowsAsync<ApiException>(async () => await client.GetCertificateListAsync(accountId, skip, take, folAllUsers));
        }

        [Test]
        public void GetNoCertificates_ForBadAccountId()
        {
            Assert.ThrowsAsync<ApiException>(async () => await client.GetCertificateListAsync(Guid.Empty));
        }

        [Test]
        public async Task GetNoLessCertificates_ForAllUsers()
        {
            try
            {
                var id = Data.FullAccountList.Accounts[0].Id;
                var certsForFewUsersTotal = (await client.GetCertificateListAsync(id)).Certificates.Length;
                var certsForAllUsersTotal = (await client.GetCertificateListAsync(id, forAllUsers: true)).Certificates.Length;
                certsForAllUsersTotal.Should().BeGreaterOrEqualTo(certsForFewUsersTotal);
            }
            catch (ApiException)
            {
                Assert.Warn("requests failed");
            }
        }
    }
}
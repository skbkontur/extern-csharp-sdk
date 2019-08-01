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
        private CertificateClient certClient;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            certClient = new CertificateClient(Client);
        }

        [TestCase]
        [TestCase(1)]
        [TestCase(1, 3)]
        [TestCase(1, 100, true)]
        public void GetCertificates_WithValidParameters(int skip = 0, int take = 100, bool forAllUsers = false)
        {
            Assert.DoesNotThrowAsync(
                async () => await certClient.GetCertificatesAsync(Account.Id, skip, take, forAllUsers));
        }

        [TestCase(0, 0)]
        [TestCase(-1)]
        [TestCase(0, -1)]
        public void GetNoCertificates_WithBadParameters(int skip = 0, int take = 100, bool folAllUsers = false)
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await certClient.GetCertificatesAsync(Account.Id, skip, take, folAllUsers));
        }

        [Test]
        public void GetNoCertificates_ForBadAccountId()
        {
            Assert.ThrowsAsync<ApiException>(async () => await certClient.GetCertificatesAsync(Guid.Empty));
        }

        [Test]
        public async Task GetNoLessCertificates_ForAllUsers()
        {
            var certsForFewUsersTotal = (await certClient.GetCertificatesAsync(Account.Id)).Certificates.Length;
            var certsForAllUsersTotal = (await certClient.GetCertificatesAsync(Account.Id, forAllUsers: true)).Certificates.Length;
            certsForAllUsersTotal.Should().BeGreaterOrEqualTo(certsForFewUsersTotal);
        }
    }
}
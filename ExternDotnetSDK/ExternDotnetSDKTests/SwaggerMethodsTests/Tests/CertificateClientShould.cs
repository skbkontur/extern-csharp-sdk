using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class CertificateClientShould : AllTestsShould
    {
        [TestCase]
        [TestCase(1)]
        [TestCase(1, 3)]
        [TestCase(1, 100, true)]
        public void GetCertificates_WithValidParameters(int skip = 0, int take = 100, bool forAllUsers = false)
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Certificates.GetCertificatesAsync(Account.Id, skip, take, forAllUsers));
        }

        [TestCase(0, 0)]
        [TestCase(-1)]
        [TestCase(0, -1)]
        public void GetNoCertificates_WithBadParameters(int skip = 0, int take = 100, bool folAllUsers = false)
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Certificates.GetCertificatesAsync(Account.Id, skip, take, folAllUsers));
        }

        [Test]
        public void GetNoCertificates_ForBadAccountId()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Certificates.GetCertificatesAsync(Guid.Empty));
        }

        [Test]
        public async Task GetNoLessCertificates_ForAllUsers()
        {
            var certsForFewUsersTotal = (await Client.Certificates.GetCertificatesAsync(Account.Id)).Certificates.Length;
            var certsForAllUsersTotal = (await Client.Certificates.GetCertificatesAsync(Account.Id, forAllUsers: true))
                .Certificates.Length;
            certsForAllUsersTotal.Should().BeGreaterOrEqualTo(certsForFewUsersTotal);
        }
    }
}
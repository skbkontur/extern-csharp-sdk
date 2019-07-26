using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class CertificateClientShould : AllTestsShould
    {
        [TestCase]
        [TestCase(1)]
        [TestCase(1, 3)]
        [TestCase(1, 100, true)]
        public void GetCertificates_WithValidParameters(int skip = 0, int take = 100, bool forAllUsers = false) =>
            Assert.DoesNotThrowAsync(async () => await CertificateClient.GetCertificatesAsync(Account.Id, skip, take, forAllUsers));

        [TestCase(0, 0)]
        [TestCase(-1)]
        [TestCase(0, -1)]
        public void GetNoCertificates_WithBadParameters(int skip = 0, int take = 100, bool folAllUsers = false) =>
            Assert.ThrowsAsync<ApiException>(async () => await CertificateClient.GetCertificatesAsync(Account.Id, skip, take, folAllUsers));

        [Test]
        public void GetNoCertificates_ForBadAccountId() =>
            Assert.ThrowsAsync<ApiException>(async () => await CertificateClient.GetCertificatesAsync(Guid.Empty));

        [Test]
        public async Task GetNoLessCertificates_ForAllUsers()
        {
            var certsForFewUsersTotal = (await CertificateClient.GetCertificatesAsync(Account.Id)).Certificates.Length;
            var certsForAllUsersTotal = (await CertificateClient.GetCertificatesAsync(Account.Id, forAllUsers: true)).Certificates.Length;
            certsForAllUsersTotal.Should().BeGreaterOrEqualTo(certsForFewUsersTotal);
        }
    }
}
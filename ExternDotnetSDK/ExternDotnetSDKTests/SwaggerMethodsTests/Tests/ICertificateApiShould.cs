using System.Threading.Tasks;
using ExternDotnetSDKTests.SwaggerMethodsTests.APIs;
using FluentAssertions;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class ICertificateApiShould : AllTestsShould<ICertificateApi>
    {
        [TestCase()]
        [TestCase(1)]
        [TestCase(1, 3)]
        [TestCase(1, 100, true)]
        public void GetCertificates_WithValidParameters(long skip = 0, long take = long.MaxValue, bool folAllUsers = false)
        {
            var accountId = Data.FullAccountList.Accounts[0].Id;
            Assert.DoesNotThrowAsync(async () => await Api.GetCertificates(accountId.ToString(), skip, take, folAllUsers));
        }

        [TestCase(0, 0)]
        [TestCase(-1)]
        [TestCase(0, -1)]
        public void GetNoCertificates_WithBadParameters(long skip = 0, long take = long.MaxValue, bool folAllUsers = false)
        {
            var accountId = Data.FullAccountList.Accounts[0].Id;
            Assert.ThrowsAsync<ApiException>(async () => await Api.GetCertificates(accountId.ToString(), skip, take, folAllUsers));
        }

        [Test]
        public void GetNoCertificates_ForBadAccountId()
        {
            Assert.ThrowsAsync<ApiException>(async () => await Api.GetCertificates("bad account id"));
        }

        [Test]
        public async Task GetNoLessCertificates_ForAllUsers()
        {
            try
            {
                foreach (var account in Data.FullAccountList.Accounts)
                {
                    var id = account.Id.ToString();
                    var certsForFewUsersTotal = (await Api.GetCertificates(id)).Certificates.Length;
                    var certsForAllUsersTotal = (await Api.GetCertificates(id, forAllUsers: true)).Certificates.Length;
                    certsForAllUsersTotal.Should().BeGreaterOrEqualTo(certsForFewUsersTotal);
                }
            }
            catch (ApiException)
            {
                Assert.Warn("requests failed");
            }
        }
    }
}
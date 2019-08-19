using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class AccountClientShould : AllTestsShould
    {
        [TestCase(0, 2)]
        [TestCase(100, 100)]
        public void GetAccounts_WithValidParameters(int skip, int take)
        {
            Assert.DoesNotThrowAsync(async () => await Client.Accounts.GetAccountsAsync(skip, take));
        }

        [TestCase(0, 0)]
        [TestCase(-1, 1)]
        [TestCase(0, -1)]
        public void GetNoAccounts_WithBadParameters(int skip, int take)
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Accounts.GetAccountsAsync(skip, take));
        }

        [Test]
        public void GetAccount_ByValidId()
        {
            Assert.DoesNotThrowAsync(async () => await Client.Accounts.GetAccountAsync(Account.Id));
        }

        [Test]
        public void GetNoAccount_ByNonexistentId()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Accounts.GetAccountAsync(Guid.NewGuid()));
        }

        [Test]
        public void FailToDeleteAccount_ByNonexistentId()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Accounts.DeleteAccountAsync(Guid.NewGuid()));
        }

        [TestCase("not an inn", "not a kpp", "obvious case")]
        [TestCase("1754462781", "515744583", "wrong inn control digit")]
        public void FailToCreateAccount_WithBadParameters(string inn, string kpp, string orgName)
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Accounts.CreateAccountAsync(inn, kpp, orgName));
        }

        [TestCase]
        [TestCase(1)]
        [TestCase(1, 3)]
        [TestCase(1, 100, true)]
        public void GetCertificates_WithValidParameters(int skip = 0, int take = 100, bool forAllUsers = false)
        {
            Assert.DoesNotThrowAsync(
                async () => await Client.Accounts.GetAccountCertificatesAsync(Account.Id, skip, take, forAllUsers));
        }

        [TestCase(0, 0)]
        [TestCase(-1)]
        [TestCase(0, -1)]
        public void GetNoCertificates_WithBadParameters(int skip = 0, int take = 100, bool folAllUsers = false)
        {
            Assert.ThrowsAsync<HttpRequestException>(
                async () => await Client.Accounts.GetAccountCertificatesAsync(Account.Id, skip, take, folAllUsers));
        }

        [Test]
        public void GetNoCertificates_ForBadAccountId()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Accounts.GetAccountCertificatesAsync(Guid.Empty));
        }

        [Test]
        public async Task GetNoLessCertificates_ForAllUsers()
        {
            var certsForFewUsersTotal = (await Client.Accounts.GetAccountCertificatesAsync(Account.Id)).Certificates.Length;
            var certsForAllUsersTotal = (await Client.Accounts.GetAccountCertificatesAsync(Account.Id, forAllUsers: true))
                .Certificates.Length;
            certsForAllUsersTotal.Should().BeGreaterOrEqualTo(certsForFewUsersTotal);
        }

        [Test]
        public void FailToGetWarrants_WithBadAccountId()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Accounts.GetAccountWarrantsAsync(Guid.NewGuid()));
        }

        [Test]
        public void GetWarrants_WithValidParameters()
        {
            Assert.DoesNotThrowAsync(async () => await Client.Accounts.GetAccountWarrantsAsync(Account.Id));
        }

        [TestCase(-1)]
        [TestCase(0, 0)]
        [TestCase(0, -1)]
        public void FailToGetWarrants_WithBadQueryParameters(int skip = 0, int take = int.MaxValue)
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Accounts.GetAccountWarrantsAsync(Account.Id, skip, take));
        }
    }
}
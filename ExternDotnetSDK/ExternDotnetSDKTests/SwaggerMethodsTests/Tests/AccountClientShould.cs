using System;
using System.Net.Http;
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
    }
}
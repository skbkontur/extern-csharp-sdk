using System;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class AccountClientShould : AllTestsShould
    {
        [TestCase(0, 2)]
        [TestCase(100, 100)]
        public void GetAccounts_WithValidParameters(int skip, int take)
        {
            Assert.DoesNotThrowAsync(async () => await AccountClient.GetAccountsAsync(skip, take));
        }

        [TestCase(0, 0)]
        [TestCase(-1, 1)]
        [TestCase(0, -1)]
        public void GetNoAccounts_WithBadParameters(int skip, int take)
        {
            Assert.ThrowsAsync<ApiException>(async () => await AccountClient.GetAccountsAsync(skip, take));
        }

        [Test]
        public void GetAccount_ByValidId()
        {
            Assert.DoesNotThrowAsync(async () => await AccountClient.GetAccountAsync(Account.Id));
        }

        [Test]
        public void GetNoAccount_ByNonexistentId()
        {
            Assert.ThrowsAsync<ApiException>(async () => await AccountClient.GetAccountAsync(Guid.NewGuid()));
        }

        [Test]
        public void FailToDeleteAccount_ByNonexistentId()
        {
            Assert.ThrowsAsync<ApiException>(async () => await AccountClient.DeleteAccountAsync(Guid.NewGuid()));
        }

        [TestCase("1111111111", "111111111", "obvious case")]
        [TestCase("1754462781", "515744583", "wrong inn control digit")]
        public void FailToCreateAccount_WithBadParameters(string inn, string kpp, string orgName)
        {
            Assert.ThrowsAsync<ApiException>(async () => await AccountClient.CreateAccountAsync(inn, kpp, orgName));
        }
    }
}
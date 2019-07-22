using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Account;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class AccountClientShould : AllTestsShould
    {
        private AccountClient client;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            client = new AccountClient(Client);
        }

        [TestCase(0, 2)]
        [TestCase(100, 100)]
        public void GetAccounts_WithValidParameters(int skip, int take)
        {
            Assert.DoesNotThrowAsync(async () => await client.GetAccountsAsync(skip, take));
        }

        [TestCase(0, 0)]
        [TestCase(-1, 1)]
        [TestCase(0, -1)]
        public void GetNoAccounts_WithBadParameters(int skip, int take)
        {
            Assert.ThrowsAsync<ApiException>(async () => await client.GetAccountsAsync(skip, take));
        }

        [Test]
        public void GetAccount_ByValidId()
        {
            var id = Data.FullAccountList.Accounts[0].Id;
            Assert.DoesNotThrowAsync(async () => await client.GetAccountAsync(id));
        }

        [Test]
        public void GetNoAccount_ByNonexistentId()
        {
            Assert.ThrowsAsync<ApiException>(async () => await client.GetAccountAsync(Guid.Empty));
        }

        [Test]
        public void CreateAndDeleteValidAccount()
        {
            Assert.DoesNotThrowAsync(
                async () =>
                {
                    var created = await client.CreateAccountAsync(
                        "1754462785",
                        "515744582",
                        "NEW ACCOUNT WITH RANDOM BUT VALID PARAMETERS");
                    await client.DeleteAccountAsync(created.Id);
                });
        }

        [Test]
        public void FailToDeleteAccount_ByNonexistentId()
        {
            Assert.ThrowsAsync<ApiException>(async () => await client.DeleteAccountAsync(Guid.Empty));
        }

        [TestCase("1111111111", "111111111", "obvious case")]
        [TestCase("1754462781", "515744583", "wrong inn control digit")]
        public void FailToCreateAccount_WithBadParameters(string inn, string kpp, string orgName)
        {
            Assert.ThrowsAsync<ApiException>(async () => await client.CreateAccountAsync(inn, kpp, orgName));
        }
    }
}
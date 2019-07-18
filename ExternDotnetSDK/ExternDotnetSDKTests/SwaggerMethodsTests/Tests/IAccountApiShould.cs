using System.Threading.Tasks;
using ExternDotnetSDK.Accounts;
using ExternDotnetSDKTests.SwaggerMethodsTests.APIs;
using FluentAssertions;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class IAccountApiShould : AllTestsShould<IAccountApi>
    {
        [Test]
        public async Task GetRootIndex()
        {
            var rootIndex = await Api.GetRootIndex();
            rootIndex.Should().Contain(Data.RootIndexLinks);
        }

        [TestCase(0, 2)]
        [TestCase(100, 100)]
        public async Task GetAllAccounts(int skip, int take)
        {
            var allAccounts = await Api.GetAccounts(skip, take);
            foreach (var account in allAccounts.Accounts)
                Data.FullAccountList.Accounts.Should().ContainEquivalentOf(account);
            Data.FullAccountList.TotalCount.Should().Be(allAccounts.TotalCount);
        }

        [TestCase(0, 0)]
        [TestCase(-1, 1)]
        [TestCase(0, -1)]
        public void GetNoAccounts_WithBadParameters(int skip, int take)
        {
            Assert.ThrowsAsync<ApiException>(async () => await Api.GetAccounts(skip, take));
        }

        [Test]
        public async Task GetAccountById()
        {
            var expected = Data.FullAccountList.Accounts[0];
            var actual = await Api.GetAccount(expected.Id.ToString());
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetNoAccount_ByNonexistentId()
        {
            Assert.ThrowsAsync<ApiException>(async () => await Api.GetAccount("nonexistent id"));
        }

        [Test]
        public void CreateAndDeleteValidAccount()
        {
            var request = new CreateAccountRequestDto
            {
                Inn = "1754462785",
                Kpp = "515744582",
                OrganizationName = "NEW ACCOUNT WITH RANDOM BUT VALID PARAMETERS"
            };
            Assert.DoesNotThrowAsync(
                async () =>
                {
                    var created = await Api.CreateAccount(request);
                    await Api.DeleteAccount(created.Id.ToString());
                });
        }

        [Test]
        public void FailToDeleteAccount_ByNonexistentId()
        {
            Assert.ThrowsAsync<ApiException>(async () => await Api.DeleteAccount("nonexistent account"));
        }

        [TestCase("1111111111", "111111111", "obvious case")]
        [TestCase("1754462781", "515744583", "wrong inn control digit")]
        public void FailToCreateAccount_WithbadParameters(string inn, string kpp, string orgName)
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await Api.CreateAccount(
                    new CreateAccountRequestDto
                    {
                        Inn = inn,
                        Kpp = kpp,
                        OrganizationName = orgName
                    }));
        }
    }
}
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Accounts;
using ExternDotnetSDKTests.SwaggerMethodsTests.APIs;
using ExternDotnetSDKTests.SwaggerMethodsTests.Common;
using FluentAssertions;
using NUnit.Framework;
using Newtonsoft.Json;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests
{
    [TestFixture]
    internal class AccountApiShould
    {
        private const string DataPath = "Environment.txt";

        private EnvironmentData data;
        private IAuthApi authApi;
        private SessionResponse session;
        private HttpClient client;
        private IAccountApi api;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            using (var file = File.OpenText(DataPath))
                using (var reader = new JsonTextReader(file))
                    data = new JsonSerializer().Deserialize<EnvironmentData>(reader);
            authApi = RestService.For<IAuthApi>(data.AuthAddress);
            session = await authApi.ByPass(data.Login, data.Password, data.ApiKey);
            client = new HttpClient(new MyHttpClientHandler(data.ApiKey, session.Sid, data.BaseAddress))
            {
                BaseAddress = new Uri(data.BaseAddress)
            };
            api = RestService.For<IAccountApi>(client);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            client.Dispose();
            using (var writer = File.CreateText(DataPath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, data);
            }
        }

        [Test]
        public async Task GetRootIndex()
        {
            var rootIndex = await api.GetRootIndex();
            rootIndex.Should().Contain(data.RootIndexLinks);
        }

        [TestCase(0, 2)]
        [TestCase(100, 100)]
        public async Task GetAllAccounts(int skip, int take)
        {
            var allAccounts = await api.GetAccounts(skip, take);
            foreach (var account in allAccounts.Accounts)
                data.FullAccountList.Accounts.Should().ContainEquivalentOf(account);
            data.FullAccountList.TotalCount.Should().Be(allAccounts.TotalCount);
        }

        [TestCase(0, 0)]
        [TestCase(-1, 1)]
        [TestCase(0, -1)]
        public async Task GetNoAccounts_WithBadParameters(int skip, int take)
        {
            try
            {
                var unused = await api.GetAccounts(skip, take);
            }
            catch (ApiException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public async Task GetAccountById()
        {
            var expected = data.FullAccountList.Accounts[0];
            var actual = await api.GetAccount(expected.Id.ToString());
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetNoAccount_ByNonexistentId()
        {
            try
            {
                var unused = await api.GetAccount("nonexistent id");
            }
            catch (ApiException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test]
        public async Task CreateAndDeleteValidAccount()
        {
            try
            {
                var created = await api.CreateAccount(
                    new CreateAccountRequestDto
                    {
                        Inn = "1754462785",
                        Kpp = "515744582",
                        OrganizationName = "NEW ACCOUNT"
                    });
                await api.DeleteAccount(created.Id.ToString());
            }
            catch (ApiException)
            {
                Assert.Fail();
            }

            Assert.Pass();
        }

        [Test]
        public async Task FailToDeleteAccount_ByNonexistentId()
        {
            try
            {
                await api.DeleteAccount("nonexistent account");
            }
            catch (ApiException)
            {
                Assert.Pass();
            }
        }

        [TestCase("1111111111", "111111111", "obvious case")]
        [TestCase("1754462781", "515744583", "wrong inn control digit")]
        public async Task FailToCreateAccount_WithbadParameters(string inn, string kpp, string orgName)
        {
            var request = new CreateAccountRequestDto
            {
                Inn = inn,
                Kpp = kpp,
                OrganizationName = orgName
            };
            try
            {
                var unused = await api.CreateAccount(request);
            }
            catch (ApiException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }
    }
}
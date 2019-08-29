using System.IO;
using System.Threading.Tasks;
using KeApiClientOpenSdk;
using KeApiClientOpenSdk.Clients.Authentication;
using KeApiClientOpenSdk.Models.Accounts;
using KeApiClientOpenSdkTests.SwaggerMethodsTests.Common;
using Newtonsoft.Json;
using NUnit.Framework;

namespace KeApiClientOpenSdkTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class AllTestsShould
    {
        protected static string DataPath = "Environment4.txt";
        protected static EnvironmentData Data;

        static AllTestsShould()
        {
            using (var file = File.OpenText(DataPath))
            using (var reader = new JsonTextReader(file))
                Data = new JsonSerializer().Deserialize<EnvironmentData>(reader);
        }

        protected KeApiClient Client;
        protected Account Account;

        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            InitializeClient();
            Account = await Client.Accounts.CreateAccountAsync(
                "1754462785",
                "515744582",
                "TEST ACCOUNT");
        }

        [OneTimeTearDown]
        public virtual async Task TearDown() => await Client.Accounts.DeleteAccountAsync(Account.Id);

        protected void InitializeClient() => Client = new KeApiClient(
            Data.ApiKey,
            new AuthenticationProvider(Data.Login, Data.Password, Data.AuthBaseAddress),
            Data.BaseAddress);
    }
}
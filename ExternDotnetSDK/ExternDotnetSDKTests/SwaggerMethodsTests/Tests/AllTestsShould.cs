using System.IO;
using System.Threading.Tasks;
using ExternDotnetSDK;
using ExternDotnetSDK.Clients.Authentication;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Accounts;
using ExternDotnetSDKTests.SwaggerMethodsTests.Common;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    //todo I have a LOT of similar tests in this whole project that can be shortened and/or merged. It's not really necessary right now, but i don't like it, but i'm not sure if i have enough time to fix it. And i'm not a dedicated tester, sorry, da?
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
        public virtual async Task TearDown()
        {
            await Client.Accounts.DeleteAccountAsync(Account.Id);
        }

        protected void InitializeClient()
        {
            var authProvider = new DefaultAuthenticationProvider(Data.AuthAddress, Data.Login, Data.Password, Data.ApiKey);
            Client = new KeApiClient(new FakeLog(), Data.BaseAddress, authProvider);
        }
    }
}
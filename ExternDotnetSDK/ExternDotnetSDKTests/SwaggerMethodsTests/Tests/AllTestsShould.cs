using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Accounts;
using ExternDotnetSDK.Clients.Account;
using ExternDotnetSDK.Clients.Authentication;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDKTests.SwaggerMethodsTests.Common;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class AllTestsShould
    {
        protected static string DataPath = "Environment3.txt";
        protected static EnvironmentData Data;
        protected static AuthClient AuthClient;

        static AllTestsShould()
        {
            using (var file = File.OpenText(DataPath))
            {
                using (var reader = new JsonTextReader(file))
                {
                    Data = new JsonSerializer().Deserialize<EnvironmentData>(reader);
                }
            }
        }

        protected SessionResponse Session;
        protected HttpClient Client;

        protected Account Account;
        protected AccountClient AccountClient;

        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            await InitializeCommonHttpClient();
            AccountClient = new AccountClient(Client);
            Account = await AccountClient.CreateAccountAsync(
                "1754462785",
                "515744582",
                "NEW ACCOUNT WITH RANDOM BUT VALID PARAMETERS");
        }

        [OneTimeTearDown]
        public virtual async Task TearDown()
        {
            await AccountClient.DeleteAccountAsync(Account.Id);
        }

        protected async Task InitializeCommonHttpClient()
        {
            AuthClient = new AuthClient(Data.AuthAddress);
            Session = await AuthClient.ByPass(Data.Login, Data.Password, Data.ApiKey);
            Client = new HttpClient(new MyHttpClientHandler(Data.ApiKey, Session.Sid, Data.BaseAddress))
            {
                BaseAddress = new Uri(Data.BaseAddress)
            };
        }
    }
}
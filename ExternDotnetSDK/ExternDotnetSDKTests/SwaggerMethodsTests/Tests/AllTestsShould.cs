using System.IO;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests;
using Kontur.Extern.Client.Clients.Authentication.TokenAuth.Kontur.Extern.Client.Clients.Authentication;
using Kontur.Extern.Client.Models.Accounts;
using Kontur.Extern.Client.Tests.SwaggerMethodsTests.Common;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.SwaggerMethodsTests.Tests
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
                    "TEST ACCOUNT")
                .ConfigureAwait(false);
        }

        [OneTimeTearDown]
        public virtual async Task TearDown() => await Client.Accounts.DeleteAccountAsync(Account.Id).ConfigureAwait(false);

        protected void InitializeClient() => Client = new KeApiClient(
            Data.ApiKey,
            new OpenIdPasswordAuthenticationProvider(Data.AuthBaseAddress, new PasswordTokenRequest() {UserName = Data.Login, Password = Data.Password, ClientId = Data.ClientId, ClientSecret = Data.ApiKey}),
            Data.BaseAddress);
    }
}
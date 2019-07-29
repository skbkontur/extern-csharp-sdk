using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Accounts;
using ExternDotnetSDK.Clients.Account;
using ExternDotnetSDK.Clients.Authentication;
using ExternDotnetSDK.Clients.Certificates;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Docflows;
using ExternDotnetSDK.Clients.Organizations;
using ExternDotnetSDK.Organizations;
using ExternDotnetSDKTests.SwaggerMethodsTests.Common;
using Newtonsoft.Json;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class AllTestsShould
    {
        protected static string DataPath = "Environment3.txt";
        protected static EnvironmentData Data;
        protected static IAuthClientRefit AuthClientRefit;

        static AllTestsShould()
        {
            using (var file = File.OpenText(DataPath))
            {
                using (var reader = new JsonTextReader(file))
                {
                    Data = new JsonSerializer().Deserialize<EnvironmentData>(reader);
                }
            }

            AuthClientRefit = RestService.For<IAuthClientRefit>(Data.AuthAddress);
        }

        protected SessionResponse Session;
        protected HttpClient Client;

        protected bool ReadyToTest = true;
        protected Account Account;
        protected Organization Organization;

        protected CertificateClient CertificateClient;
        protected AccountClient AccountClient;
        protected OrganizationsClient OrganizationsClient;
        protected DocflowsClient docflowsClient;

        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            try
            {
                Session = await AuthClientRefit.ByPass(Data.Login, Data.Password, Data.ApiKey);
                Client = new HttpClient(new MyHttpClientHandler(Data.ApiKey, Session.Sid, Data.BaseAddress))
                {
                    BaseAddress = new Uri(Data.BaseAddress)
                };
                AccountClient = new AccountClient(Client);
                CertificateClient = new CertificateClient(Client);
                OrganizationsClient = new OrganizationsClient(Client);
                docflowsClient = new DocflowsClient(Client);

                Account = await AccountClient.CreateAccountAsync(
                    "1754462785",
                    "515744582",
                    "NEW ACCOUNT WITH RANDOM BUT VALID PARAMETERS");
                Organization = (await OrganizationsClient.SearchOrganizationsAsync(Account.Id))
                    .Organizations[0];
            }
            catch (Exception)
            {
                ReadyToTest = false;
            }
        }

        [OneTimeTearDown]
        public async Task TearDown()
        {
            await AccountClient.DeleteAccountAsync(Account.Id);
        }

        [SetUp]
        public void CheckInitialData()
        {
            if (!ReadyToTest)
                Assert.Fail("There was an exception while initializing data used for tests");
        }
    }
}
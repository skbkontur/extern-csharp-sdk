//using System;
//using System.Threading.Tasks;
//using ExternDotnetSDK.Clients.TestApi;
//using NUnit.Framework;
//using Refit;

//namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
//{
//    [TestFixture]
//    internal class TestApiClientShould : AllTestsShould
//    {
//        private TestApiClient client;

//        [OneTimeSetUp]
//        public override async Task SetUp()
//        {
//            await InitializeCommonHttpClient();
//            client = new TestApiClient(Client);
//        }

//        [Test]
//        public void FailPoke_ByBadRequestedIds()
//        {
//            Assert.ThrowsAsync<ApiException>(async () => 
//                await client.PokeAsync(Guid.NewGuid(),Guid.NewGuid()));
//        }
//    }
//}

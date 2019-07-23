using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Docflows;
using ExternDotnetSDK.Docflows;
using NUnit.Framework;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class DocflowsClientShould : AllTestsShould
    {
        private DocflowsClient client;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            client = new DocflowsClient(Client);
        }

        [Test]
        public void GetDocflows_WithValidParameters()
        {
            var filter = new DocflowFilter
            {
                Take = 1
            };
            var accountId = Data.FullAccountList.Accounts[1].Id;
            Assert.DoesNotThrowAsync(async () =>
            {
                var page = await client.GetDocflowsAsync(accountId, filter);
            });
        }
    }
}
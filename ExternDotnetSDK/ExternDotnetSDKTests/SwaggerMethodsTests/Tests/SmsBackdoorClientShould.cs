using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.SmsBackdoor;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class SmsBackdoorClientShould : AllTestsShould
    {
        private SmsBackdoorClient client;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            client = new SmsBackdoorClient(Client);
        }

        [TestCase(null)]
        [TestCase("not a request id at all")]
        public void GetNoConfirmationCode_WithBasRequestId(string requestId)
        {
            Assert.ThrowsAsync<ApiException>(async () => await client.GetConfirmationCodeAsync(requestId));
        }

        [Test]
        public void GetNoConfirmationCode_WithRandomRequestId()
        {
            Assert.ThrowsAsync<ApiException>(
                async () => await client.GetConfirmationCodeAsync(Guid.NewGuid().ToString()));
        }
    }
}
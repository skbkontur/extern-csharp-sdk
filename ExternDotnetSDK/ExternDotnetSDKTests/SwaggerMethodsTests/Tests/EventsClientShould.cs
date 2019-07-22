using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Events;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class EventsClientShould : AllTestsShould
    {
        private EventsClient client;

        [OneTimeSetUp]
        public override async Task SetUp()
        {
            await base.SetUp();
            client = new EventsClient(Client);
        }

        [TestCase(1)]
        [TestCase(1, "123")]
        public void GetEvents_WithValidParameters(int take, string fromId = "0_0")
        {
            Assert.DoesNotThrowAsync(async () => await client.GetEventsAsync(take, fromId));
        }

        [TestCase(-1)]
        [TestCase(1, null)]
        [TestCase(1, "very bad fromId")]
        public void GetNoEvents_WithBadParameters(int take, string fromId = "0_0")
        {
            Assert.ThrowsAsync<ApiException>(async () => await client.GetEventsAsync(take, fromId));
        }
    }
}
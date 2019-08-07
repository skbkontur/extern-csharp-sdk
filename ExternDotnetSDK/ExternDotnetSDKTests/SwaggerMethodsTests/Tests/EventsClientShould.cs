using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class EventsClientShould : AllTestsShould
    {
        [TestCase(1)]
        [TestCase(1, "123")]
        public void GetEvents_WithValidParameters(int take, string fromId = "0_0")
        {
            Assert.DoesNotThrowAsync(async () => await Client.Events.GetEventsAsync(take, fromId));
        }

        [TestCase(-1)]
        [TestCase(1, null)]
        public void GetNoEvents_WithBadParameters(int take, string fromId = "0_0")
        {
            Assert.ThrowsAsync<ApiException>(async () => await Client.Events.GetEventsAsync(take, fromId));
        }
    }
}
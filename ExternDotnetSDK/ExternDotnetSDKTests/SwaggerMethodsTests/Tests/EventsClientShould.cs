using System;
using System.Net.Http;
using NUnit.Framework;

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

        [Test]
        public void GetNoEvents_WithNegativeTake()
        {
            Assert.ThrowsAsync<HttpRequestException>(async () => await Client.Events.GetEventsAsync(-1));
        }

        [Test]
        public void GetNoEvents_WithNullFromId()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () => await Client.Events.GetEventsAsync(1, null));
        }
    }
}
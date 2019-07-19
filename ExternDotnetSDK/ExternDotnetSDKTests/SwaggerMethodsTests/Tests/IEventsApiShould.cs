using System.Threading.Tasks;
using ExternDotnetSDK.Events;
using ExternDotnetSDKTests.SwaggerMethodsTests.APIs;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class IEventsApiShould : AllTestsShould<IEventsApi>
    {
        [TestCase(1)]
        [TestCase(1, "123")]
        public void GetEvents_WithValidParameters(int take, string fromId = "0_0")
        {
            Assert.DoesNotThrowAsync(async () => await Api.GetEvents(take, fromId));
        }

        [TestCase(-1)]
        [TestCase(1, null)]
        [TestCase(1, "asffewewfewfew")]
        public void GetNoEvents_WithBadParameters(int take, string fromId = "0_0")
        {
            Assert.ThrowsAsync<ApiException>(async () => await Api.GetEvents(take, fromId));
        }
    }
}
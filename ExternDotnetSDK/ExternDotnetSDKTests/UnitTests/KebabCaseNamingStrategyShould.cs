using System.Linq;
using ExternDotnetSDK.Models.Accounts;
using ExternDotnetSDK.Models.Drafts.Requests;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ExternDotnetSDKTests.UnitTests
{
    [TestFixture]
    internal class KebabCaseNamingStrategyShould
    {
        [Test]
        public void SerializeClassesCorrectly()
        {
            var serialized = JsonConvert.SerializeObject(new AccountList());
            serialized.Should().Match(s => !s.Any(char.IsUpper));
            serialized.Should().Contain("-");
        }

        [Test]
        public void SerializeSenderRequest_AccordingToDocumentation()
        {
            var request = new SenderRequest();
            var serialized = JsonConvert.SerializeObject(request);
            serialized.Should().Contain("ipaddress");
        }
    }
}
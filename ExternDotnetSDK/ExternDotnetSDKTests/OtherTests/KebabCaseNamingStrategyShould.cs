using System.Linq;
using FluentAssertions;
using KeApiOpenSdk.Models.Accounts;
using KeApiOpenSdk.Models.Drafts.Requests;
using Newtonsoft.Json;
using NUnit.Framework;

namespace KeApiOpenSdkTests.OtherTests
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
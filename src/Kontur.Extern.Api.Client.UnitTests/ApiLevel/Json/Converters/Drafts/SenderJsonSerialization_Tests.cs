using System.Net;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.Drafts.Meta;
using Kontur.Extern.Api.Client.Models.Numbers;
using NUnit.Framework;
using Vostok.Logging.Console;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.Drafts
{
    [TestFixture]
    internal class SenderJsonSerialization_Tests
    {
        private const string JsonWithoutCert = @"{
  ""inn"": ""3077668269"",
  ""kpp"": ""561650781"",
  ""is-representative"": true,
  ""ipaddress"": ""8.8.8.8""
}";
        private IJsonSerializer serializer = null!;

        [SetUp]
        public void SetUp() => serializer = JsonSerializerFactory.CreateJsonSerializer(new ConsoleLog());

        [Test]
        public void Should_serialize_sender_to_json()
        {
            var json = serializer.SerializeToIndentedString(CreateSenderWithoutCert());

            json.Should().Be(JsonWithoutCert);
        }

        private static Sender CreateSenderWithoutCert()
        {
            return new Sender
            {
                Inn = "3077668269",
                Kpp = Kpp.Parse("561650781"),
                Name = null,
                IsRepresentative = true,
                IpAddress = IPAddress.Parse("8.8.8.8")
            };
        }
    }
}
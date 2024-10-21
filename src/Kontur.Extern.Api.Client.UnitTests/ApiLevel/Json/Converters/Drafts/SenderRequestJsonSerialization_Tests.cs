using System.Net;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.Numbers;
using NUnit.Framework;
using Vostok.Logging.Console;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.Drafts
{
    [TestFixture]
    internal class SenderRequestJsonSerialization_Tests
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
        public void Should_serialize_sender_request_to_json()
        {
            var json = serializer.SerializeToIndentedString(CreateSenderRequestWithoutCert());

            json.Should().Be(JsonWithoutCert);
        }

        private static SenderRequest CreateSenderRequestWithoutCert()
        {
            return new SenderRequest
            {
                Inn = "3077668269",
                Kpp = Kpp.Parse("561650781"),
                IsRepresentative = true,
                IpAddress = IPAddress.Parse("8.8.8.8")
            };
        }
    }
}
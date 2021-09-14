using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.Drafts.Meta;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.Tests.ApiLevel.Clients.Models.JsonConverters
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
        private IJsonSerializer serializer;

        [SetUp]
        public void SetUp() => serializer = JsonSerializerFactory.CreateJsonSerializer();

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
                Kpp = "561650781",
                Name = null,
                IsRepresentative = true,
                IpAddress = "8.8.8.8"
            };
        }
    }
}
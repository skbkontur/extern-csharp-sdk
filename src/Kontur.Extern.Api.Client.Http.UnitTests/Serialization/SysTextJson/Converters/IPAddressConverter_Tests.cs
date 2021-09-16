using System.Net;
using FluentAssertions;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson;
using Xunit;

namespace Kontur.Extern.Api.Client.Http.UnitTests.Serialization.SysTextJson.Converters
{
    public class IPAddressConverter_Tests
    {
        private readonly IJsonSerializer serializer;

        public IPAddressConverter_Tests() => serializer = new SystemTextJsonSerializerFactory().CreateSerializer();

        [Fact]
        public void Should_serialize_ip_address_to_json()
        {
            var json = serializer.SerializeToIndentedString(IPAddress.Parse("8.8.8.8"));

            json.Should().Be("\"8.8.8.8\"");
        }

        [Fact]
        public void Should_deserialize_null_json_value()
        {
            var ipAddress = serializer.TryDeserialize<IPAddress>("null").EnsureSuccess().GetResultOrNull();

            ipAddress.Should().BeNull();
        }
    }
}
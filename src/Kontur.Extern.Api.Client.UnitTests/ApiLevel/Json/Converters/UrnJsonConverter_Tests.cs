using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Testing.Helpers;
using NUnit.Framework;
using Vostok.Logging.Console;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters
{
    internal class UrnJsonConverter_Tests
    {
        private IJsonSerializer serializer = null!;

        [SetUp]
        public void SetUp() =>
            serializer = JsonSerializerFactory.CreateJsonSerializer(new ConsoleLog());

        [TestCase("urn:ns:val")]
        [TestCase("urn:ns")]
        public void Should_serialize_URN_to_the_string_representation(string value)
        {
            var urn = Urn.Parse(value);
            var json = serializer.SerializeToIndentedString(urn);

            json.Should().Be(value.ToQuoted());
        }

        [TestCase("urn:ns:val")]
        [TestCase("urn:ns")]
        public void Should_deserialize_URN_from_string_value(string value)
        {
            var json = value.ToQuoted();

            var deserialized = serializer.Deserialize<Urn>(json);

            deserialized.ToString().Should().BeEquivalentTo(value);
        }
    }
}
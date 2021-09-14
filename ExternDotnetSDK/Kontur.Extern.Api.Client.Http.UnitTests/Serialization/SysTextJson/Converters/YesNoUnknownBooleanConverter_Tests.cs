using FluentAssertions;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Converters;
using Xunit;

namespace Kontur.Extern.Api.Client.Http.UnitTests.Serialization.SysTextJson.Converters
{
    public class YesNoUnknownBooleanConverter_Tests
    {
        private readonly IJsonSerializer serializer;

        public YesNoUnknownBooleanConverter_Tests() => serializer = new SystemTextJsonSerializerFactory()
            .AddConverter(new YesNoUnknownBooleanConverter())
            .CreateSerializer();
        
        [Theory]
        [InlineData(true, "yes")]
        [InlineData(false, "no")]
        [InlineData(null, "unknown")]
        public void Should_serialize_nullable_bool_value_in_alternative_basis(bool? value, string expectedValue)
        {
            var json = serializer.SerializeToIndentedString(value);

            json.Should().Be($"\"{expectedValue}\"");
        }
        
        [Theory]
        [InlineData("true", true)]
        [InlineData("\"true\"", true)]
        [InlineData("\"True\"", true)]
        [InlineData("\"TRUE\"", true)]
        [InlineData("\"yes\"", true)]
        [InlineData("\"Yes\"", true)]
        [InlineData("\"YES\"", true)]
        [InlineData("false", false)]
        [InlineData("\"false\"", false)]
        [InlineData("\"False\"", false)]
        [InlineData("\"FALSE\"", false)]
        [InlineData("\"no\"", false)]
        [InlineData("\"No\"", false)]
        [InlineData("\"NO\"", false)]
        [InlineData("null", null)]
        [InlineData("\"unknown\"", null)]
        [InlineData("\"Unknown\"", null)]
        [InlineData("\"UNKNOWN\"", null)]
        public void Should_deserialize_nullable_bool_value_from_regular_basis_and_alternative_basis_both(string jsonText, bool? expectedValue)
        {
            var result = serializer.TryDeserialize<bool?>(jsonText);

            result.EnsureSuccess().GetResultOrNull().Should().Be(expectedValue);
        }
    }
}
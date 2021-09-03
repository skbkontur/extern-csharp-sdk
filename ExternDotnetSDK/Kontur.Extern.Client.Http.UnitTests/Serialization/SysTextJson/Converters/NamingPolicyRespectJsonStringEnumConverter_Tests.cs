using System.Text.Json;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.Converters.EnumConverters;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingPolicies;
using Kontur.Extern.Client.Testing.Helpers;
using Xunit;

namespace Kontur.Extern.Client.Http.UnitTests.Serialization.SysTextJson.Converters
{
    public static class NamingPolicyRespectJsonStringEnumConverter_Tests
    {
        public class NonNullableEnums
        {
            public static TheoryData<(TestEnum testEnum, string expectedJson, JsonNamingPolicy? namingPolicy)> NamingPoliciesEnumCases => new()
            {
                (TestEnum.Value, nameof(TestEnum.Value).ToSnakeCase().ToQuoted(), new KebabCaseNamingPolicy()),
                (TestEnum.ManyWordsValue, nameof(TestEnum.ManyWordsValue).ToKebabCase().ToQuoted(), new KebabCaseNamingPolicy()),
                (TestEnum.Value, nameof(TestEnum.Value).ToSnakeCase().ToQuoted(), new SnakeCaseNamingPolicy()),
                (TestEnum.ManyWordsValue, nameof(TestEnum.ManyWordsValue).ToSnakeCase().ToQuoted(), new SnakeCaseNamingPolicy()),
                (TestEnum.Value, nameof(TestEnum.Value).ToQuoted(), null),
                (TestEnum.ManyWordsValue, nameof(TestEnum.ManyWordsValue).ToQuoted(), null)
            };

            [Theory]
            [MemberData(nameof(NamingPoliciesEnumCases))]
            public void Should_serialize_as_string_according_naming_policy((TestEnum testEnum, string expectedJson, JsonNamingPolicy? namingPolicy) theCase) =>
                ShouldSerializeDeserializeJsonCorrectly(theCase.testEnum, theCase.namingPolicy, theCase.expectedJson);

            public static TheoryData<(TestIntEnum testEnum, string expectedJson)> IntEnumCases => new()
            {
                (TestIntEnum.Zero, "0"),
                (TestIntEnum.One, "1")
            };

            [Theory]
            [MemberData(nameof(IntEnumCases))]
            public void Should_serialize_as_integer_number((TestIntEnum testEnum, string expectedJson) theCase)
            {
                var serializer = CreateSerializerWithNumberEnums();
                ShouldSerializeDeserializeJsonCorrectly(theCase.testEnum, serializer, theCase.expectedJson);
            }

            public static TheoryData<(TestLongEnum testEnum, string expectedJson)> LongEnumCases => new()
            {
                (TestLongEnum.Zero, "0"),
                (TestLongEnum.One, "1")
            };

            [Theory]
            [MemberData(nameof(LongEnumCases))]
            public void Should_serialize_as_long_number((TestLongEnum testEnum, string expectedJson) theCase)
            {
                var serializer = CreateSerializerWithNumberEnums();
                ShouldSerializeDeserializeJsonCorrectly(theCase.testEnum, serializer, theCase.expectedJson);
            }
        }
        
        public class NullableEnums
        {
            public static TheoryData<(TestEnum? testEnum, string expectedJson, JsonNamingPolicy? namingPolicy)> NamingPoliciesNullableEnumCases => new()
            {
                (null, "null", new KebabCaseNamingPolicy()),
                (TestEnum.Value, nameof(TestEnum.Value).ToSnakeCase().ToQuoted(), new KebabCaseNamingPolicy()),
                (TestEnum.ManyWordsValue, nameof(TestEnum.ManyWordsValue).ToKebabCase().ToQuoted(), new KebabCaseNamingPolicy()),
                (null, "null", new KebabCaseNamingPolicy()),
                (TestEnum.Value, nameof(TestEnum.Value).ToSnakeCase().ToQuoted(), new SnakeCaseNamingPolicy()),
                (TestEnum.ManyWordsValue, nameof(TestEnum.ManyWordsValue).ToSnakeCase().ToQuoted(), new SnakeCaseNamingPolicy()),
                (null, "null", null),
                (TestEnum.Value, nameof(TestEnum.Value).ToQuoted(), null),
                (TestEnum.ManyWordsValue, nameof(TestEnum.ManyWordsValue).ToQuoted(), null)
            };

            [Theory]
            [MemberData(nameof(NamingPoliciesNullableEnumCases))]
            public void Should_serialize_as_string_according_naming_policy((TestEnum? testEnum, string expectedJson, JsonNamingPolicy? namingPolicy) theCase) =>
                ShouldSerializeDeserializeJsonCorrectly(theCase.testEnum, theCase.namingPolicy, theCase.expectedJson);

            public static TheoryData<(TestIntEnum? testEnum, string expectedJson)> NullableIntEnumCases => new()
            {
                (null, "null"),
                (TestIntEnum.Zero, "0"),
                (TestIntEnum.One, "1")
            };

            [Theory]
            [MemberData(nameof(NullableIntEnumCases))]
            public void Should_serialize_as_integer_number((TestIntEnum? testEnum, string expectedJson) theCase)
            {
                var serializer = CreateSerializerWithNumberEnums();
                ShouldSerializeDeserializeJsonCorrectly(theCase.testEnum, serializer, theCase.expectedJson);
            }

            public static TheoryData<(TestLongEnum? testEnum, string expectedJson)> NullableLongEnumCases => new()
            {
                (null, "null"),
                (TestLongEnum.Zero, "0"),
                (TestLongEnum.One, "1")
            };

            [Theory]
            [MemberData(nameof(NullableLongEnumCases))]
            public void Should_serialize_as_long_number((TestLongEnum? testEnum, string expectedJson) theCase)
            {
                var serializer = CreateSerializerWithNumberEnums();
                ShouldSerializeDeserializeJsonCorrectly(theCase.testEnum, serializer, theCase.expectedJson);
            }
        }

        public class OverridenNamingPolicy
        {
            [Fact]
            public void Should_override_naming_policy_for_particular_enum()
            {
                var serializer = new SystemTextJsonSerializerFactory()
                    .WithNamingPolicy(new KebabCaseNamingPolicy())
                    .IgnoreIndentation()
                    .IgnoreNullValues(false)
                    .SetCustomNamingPolicyForSerializationEnumOf<AnotherTestEnum>(JsonNamingPolicy.CamelCase)
                    .CreateSerializer();

                ShouldSerializeDeserializeJsonCorrectly(
                    AnotherTestEnum.ManyWordsMember,
                    serializer,
                    "\"manyWordsMember\""
                );
                ShouldSerializeDeserializeJsonCorrectly(
                    TestEnum.ManyWordsValue,
                    serializer,
                    nameof(TestEnum.ManyWordsValue).ToKebabCase().ToQuoted()
                );
            }
        }

        private static void ShouldSerializeDeserializeJsonCorrectly<TEnum>(TEnum testEnum, JsonNamingPolicy? namingPolicy, string expectedJson)
        {
            var serializer = new SystemTextJsonSerializerFactory()
                .WithNamingPolicy(namingPolicy)
                .IgnoreIndentation()
                .IgnoreNullValues(false)
                .CreateSerializer();
            ShouldSerializeDeserializeJsonCorrectly(testEnum, serializer, expectedJson);
        }

        private static void ShouldSerializeDeserializeJsonCorrectly<TEnum>(TEnum testEnum, IJsonSerializer serializer, string expectedJson)
        {
            var dto = new Dto<TEnum> {value = testEnum};
            var expectedDtoJson = $"{{\"{nameof(dto.value)}\":{expectedJson}}}";
            
            var json = serializer.SerializeToIndentedString(dto);
            json.Should().BeEquivalentTo(expectedDtoJson);

            var deserializedDto = serializer.Deserialize<Dto<TEnum>>(json);
            deserializedDto.Should().BeEquivalentTo(dto);
        }

        private class Dto<TEnum>
        {
            [UsedImplicitly]
            // ReSharper disable once InconsistentNaming
            public TEnum value { get; set; } = default!;
        }

        private static IJsonSerializer CreateSerializerWithNumberEnums() =>
            new SystemTextJsonSerializerFactory()
                .AddConverter(new NamingPolicyRespectJsonStringEnumConverter(false))
                .IgnoreIndentation()
                .IgnoreNullValues(false)
                .CreateSerializer();

        public enum TestEnum
        {
            Value,
            ManyWordsValue
        }

        public enum AnotherTestEnum
        {
            Member,
            ManyWordsMember
        }
        
        public enum TestIntEnum
        {
            Zero = 0,
            One = 1
        }
        
        public enum TestLongEnum : long
        {
            Zero = 0L,
            One = 1L
        }
    }
}
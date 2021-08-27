using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.Converters;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingStrategies;
using Xunit;

namespace Kontur.Extern.Client.Http.UnitTests.Serialization.SysTextJson.Converters
{
    public class HttpStatusCodeConverter_Tests
    {
        [Theory]
        [MemberData(nameof(ValidStatusCodes))]
        public void Should_deserialize_http_status_code_as_int(HttpStatusCode statusCode)
        {
            var json = $@"{{""statusCode"": {(int) statusCode}}}";

            ShouldDeserializeJsonCorrectly(json, statusCode);
        }
        
        [Theory]
        [MemberData(nameof(ValidStatusCodes))]
        public void Should_deserialize_http_status_code_as_quoted_int(HttpStatusCode statusCode)
        {
            var json = $@"{{""statusCode"": ""{(int) statusCode}""}}";

            ShouldDeserializeJsonCorrectly(json, statusCode);
        }
        
        [Theory]
        [MemberData(nameof(ValidStatusCodes))]
        public void Should_deserialize_http_status_code_as_string(HttpStatusCode statusCode)
        {
            var json = $@"{{""statusCode"": ""{statusCode.ToString()}""}}";
            
            ShouldDeserializeJsonCorrectly(json, statusCode);
        }
        
        [Theory]
        [MemberData(nameof(ValidStatusCodes))]
        public void Should_deserialize_http_status_code_as_lower_case_string(HttpStatusCode statusCode)
        {
            var json = $@"{{""statusCode"": ""{statusCode.ToString().ToLowerInvariant()}""}}";
            
            ShouldDeserializeJsonCorrectly(json, statusCode);
        }
        
        [Theory]
        [MemberData(nameof(ValidStatusCodes))]
        public void Should_deserialize_http_status_code_as_UPPER_case_string(HttpStatusCode statusCode)
        {
            var json = $@"{{""statusCode"": ""{statusCode.ToString().ToUpperInvariant()}""}}";
            
            ShouldDeserializeJsonCorrectly(json, statusCode);
        }

        [Theory]
        [MemberData(nameof(ValidStatusCodes))]
        public void Should_deserialize_http_status_code_as_string_with_kebab_naming_policy(HttpStatusCode statusCode)
        {
            var json = $@"{{""status-code"": ""{statusCode.ToString().ToKebabCase()}""}}";
            
            ShouldDeserializeJsonCorrectly(json, statusCode, new KebabCaseNamingPolicy());
        }

        [Theory]
        [MemberData(nameof(ValidStatusCodes))]
        public void Should_deserialize_http_status_code_as_string_with_snake_naming_policy(HttpStatusCode statusCode)
        {
            var json = $@"{{""status_code"": ""{statusCode.ToString().ToSnakeCase()}""}}";
            
            ShouldDeserializeJsonCorrectly(json, statusCode, new SnakeCaseNamingPolicy());
        }

        [Theory]
        [MemberData(nameof(ValidStatusCodes))]
        public void Should_serialize_as_int_by_default(HttpStatusCode statusCode) => ShouldSerializeCorrectly(
            new SystemTextJsonSerializer(true),
            statusCode,
            $@"{{""StatusCode"":{((int) statusCode).ToString()}}}"
        );

        [Theory]
        [MemberData(nameof(ValidStatusCodes))]
        public void Should_serialize_as_string(HttpStatusCode statusCode) => ShouldSerializeCorrectly(
            CreateSerializerWithOverridenConverter(new HttpStatusCodeConverter(null, true)),
            statusCode,
            $@"{{""StatusCode"":""{statusCode.ToString()}""}}"
        );

        [Theory]
        [MemberData(nameof(ValidStatusCodes))]
        public void Should_serialize_as_string_according_naming_policy(HttpStatusCode statusCode) => ShouldSerializeCorrectly(
            CreateSerializerWithOverridenConverter(new HttpStatusCodeConverter(new KebabCaseNamingPolicy(), true)),
            statusCode,
            $@"{{""StatusCode"":""{statusCode.ToString().ToKebabCase()}""}}"
        );

        private static void ShouldSerializeCorrectly(SystemTextJsonSerializer serializer, HttpStatusCode statusCode, string? expectedJson)
        {
            var dto = new Dto {StatusCode = statusCode};

            var json = serializer.SerializeToIndentedString(dto);

            json.Should().Be(expectedJson);
        }

        private static void ShouldDeserializeJsonCorrectly(string json, HttpStatusCode statusCode, JsonNamingPolicy? namingPolicy = null)
        {
            var serializer = new SystemTextJsonSerializer(namingPolicy);
            var expectedDto = new Dto {StatusCode = statusCode};

            var dto = serializer.DeserializeFromJson<Dto>(json);

            dto.Should().BeEquivalentTo(expectedDto);
        }

        private static SystemTextJsonSerializer CreateSerializerWithOverridenConverter(JsonConverter converter) => 
            new(null, new[] {converter}, true);

        public static TheoryData<HttpStatusCode> ValidStatusCodes
        {
            get
            {
                var statusCodes = new TheoryData<HttpStatusCode>();
                foreach (var statusCode in Enum.GetValues<HttpStatusCode>())
                {
                    statusCodes.Add(statusCode);
                }
                return statusCodes;
            }
        }

        private class Dto
        {
            public HttpStatusCode StatusCode { [UsedImplicitly] get; set; }
        }
    }
}
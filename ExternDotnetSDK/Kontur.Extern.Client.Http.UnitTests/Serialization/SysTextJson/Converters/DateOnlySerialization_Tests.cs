using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using FluentAssertions;
using Kontur.Extern.Client.Common.Time;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Http.Serialization.SysTextJson;
using Kontur.Extern.Client.Testing.xUnit;
using Xunit;

namespace Kontur.Extern.Client.Http.UnitTests.Serialization.SysTextJson.Converters
{
    public class DateOnlySerialization_Tests
    {
        private readonly IJsonSerializer serializer;

        public DateOnlySerialization_Tests() => serializer = new SystemTextJsonSerializer();

        [Fact]
        public void Should_serialize_date_to_json_in_iso_format()
        {
            var json = serializer.SerializeToIndentedString(new DateOnly(2021, 8, 27));

            json.Should().Be("\"2021-08-27\"");
        }

        [Fact]
        public void Should_fail_when_deserialize_null_to_non_nullable_date()
        {
            Action action = () => serializer.TryDeserialize<DateOnly>("null").EnsureSuccess();

            action.Should().Throw<JsonException>();
        }

        [Theory]
        [MemberData(nameof(DeserializationCases))]
        public void Should_deserialize_date_from_different_formats((string json, DateOnly? expectedDate) theCase)
        {
            var (json, expectedDate) = theCase;
            
            var actualDate = serializer.TryDeserialize<DateOnly?>(json).GetResultOrNull();

            actualDate.Should().Be(expectedDate);
        }

        public static TheoryData<(string json, DateOnly? expectedDate)> DeserializationCases
        {
            get
            {
                var expectedDate = new DateOnly(2021, 8, 27);
                return StringValues()
                    .Select(x => ($"\"{x}\"", (DateOnly?) expectedDate))
                    .Prepend(("null", null))
                    .ToTheoryData();

                IEnumerable<string> StringValues()
                {
                    yield return expectedDate.ToLongDateString();
                    yield return expectedDate.ToString();
                    yield return expectedDate.ToShortDateString();
                    yield return expectedDate.ToString("yyyy-MM-dd");
                    yield return expectedDate.ToString("yyyy-MM-ddThh:mm:ss");
                }
            }
        }
    }
}
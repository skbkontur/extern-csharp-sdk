using System;
using FluentAssertions;
using Xunit;
using Kontur.Extern.Client.Http.Models.Headers;

namespace Kontur.Extern.Client.Http.UnitTests.Models.Headers
{
    public class ContentType_Tests
    {
        [Fact]
        public void Should_fail_when_given_null_content_type()
        {
            Action action = () => _ = new ContentType(null!);

            action.Should().Throw<ArgumentException>();
        }
        
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("application/json")]
        public void Should_initialize_content_type(string value)
        {
            var contentType = new ContentType(value);

            contentType.ToString().Should().Be(value);
        }

        [Theory]
        [InlineData("application/json", true)]
        [InlineData("application/json;charset=utf-8", true)]
        [InlineData("plain/json;charset=utf-8", false)]
        [InlineData("json", false)]
        [InlineData("", false)]
        [InlineData("application/xml", false)]
        public void IsJson_should_indicate_that_content_type_is_json(string value, bool expectedResult)
        {
            var contentType = new ContentType(value);

            contentType.IsJson.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("plain/text", true)]
        [InlineData("plain/text;charset=utf-8", true)]
        [InlineData("application/text", false)]
        [InlineData("application/json", false)]
        [InlineData("text", false)]
        [InlineData("", false)]
        [InlineData("application/xml", false)]
        public void IsPlainText_should_indicate_that_content_type_is_text(string value, bool expectedResult)
        {
            var contentType = new ContentType(value);

            contentType.IsPlainText.Should().Be(expectedResult);
        }
    }
}
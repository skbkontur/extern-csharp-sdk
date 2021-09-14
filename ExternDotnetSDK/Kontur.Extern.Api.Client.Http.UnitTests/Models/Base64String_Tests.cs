using System;
using System.Text;
using FluentAssertions;
using Kontur.Extern.Api.Client.Http.Models;
using Xunit;

namespace Kontur.Extern.Api.Client.Http.UnitTests.Models
{
    public class Base64String_Tests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void FromEncoded_should_fail_when_given_an_invalid_base_64_string(string value)
        {
            Action action = () => Base64String.FromEncoded(value);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Encode_should_fail_when_given_null_bytes()
        {
            Action action = () => Base64String.Encode((byte[])null!);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Encode_should_fail_when_given_null_string()
        {
            Action action = () => Base64String.Encode((string)null!);

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Decode_should_decode_encoded_bytes()
        {
            var bytes = new byte[] {0, 1, 2, 3};
            var base64String = Base64String.Encode(bytes);

            base64String.Decode().Should().BeEquivalentTo(bytes);
        }

        [Fact]
        public void Decode_should_decode_encoded_string()
        {
            const string? plainString = "0123";
            var expectedBytes = Encoding.UTF8.GetBytes(plainString);
            var base64String = Base64String.Encode(plainString);

            base64String.Decode().Should().BeEquivalentTo(expectedBytes);
        }

        [Fact]
        public void ToString_should_return_encoded_bytes()
        {
            var bytes = new byte[] {0, 1, 2, 3};
            var encodedString = Convert.ToBase64String(bytes);
            var base64String = Base64String.Encode(bytes);

            base64String.ToString().Should().BeEquivalentTo(encodedString);
        }

        [Fact]
        public void ToString_should_return_encoded_string()
        {
            const string? plainString = "0123";
            var encodedString = Convert.ToBase64String(Encoding.UTF8.GetBytes(plainString));
            var base64String = Base64String.Encode(plainString);

            base64String.ToString().Should().BeEquivalentTo(encodedString);
        }
    }
}
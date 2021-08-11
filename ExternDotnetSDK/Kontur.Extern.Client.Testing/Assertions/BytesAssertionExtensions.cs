using FluentAssertions;
using FluentAssertions.Execution;

namespace Kontur.Extern.Client.Testing.Assertions
{
    public static class BytesAssertionExtensions
    {
        public static void ShouldHaveExpectedBytes(this byte[] bytes, byte[] expectedBytes)
        {
            using var scope = new AssertionScope();

            bytes.Should().HaveSameCount(expectedBytes);
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i].Should().Be(expectedBytes[i], $"{i} bytes should be equal to expected");
            }
        }
    }
}
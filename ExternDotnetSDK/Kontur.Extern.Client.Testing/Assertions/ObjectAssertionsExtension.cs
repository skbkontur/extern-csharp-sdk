using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Kontur.Extern.Client.Testing.Assertions
{
    public static class ObjectAssertionsExtension
    {
        public static void BeEqualAndHasSameHashCode(this ObjectAssertions assertions, object expected)
        {
            using var scope = new AssertionScope("object");

            assertions.Subject.Should().Be(expected);
            assertions.Subject.GetHashCode().Should().Be(expected.GetHashCode());
        }
        
        public static void NotBeEqualAndHasDifferentHashCode(this ObjectAssertions assertions, object expected)
        {
            using var scope = new AssertionScope("object");

            assertions.Subject.Should().NotBe(expected);
            assertions.Subject.GetHashCode().Should().NotBe(expected.GetHashCode());
        }
    }
}
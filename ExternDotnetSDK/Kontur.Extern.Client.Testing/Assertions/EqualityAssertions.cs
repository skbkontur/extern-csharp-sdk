using FluentAssertions;
using FluentAssertions.Execution;

namespace Kontur.Extern.Client.Testing.Assertions
{
    internal static class EqualityAssertions
    {
        public static void BeEqualAndHasSameHashCode(object subject, object expected)
        {
            using var scope = new AssertionScope("object");

            subject.Should().Be(expected);
            subject.GetHashCode().Should().Be(expected.GetHashCode());
        }
        
        public static void NotBeEqualAndHasDifferentHashCode(object subject, object notExpected)
        {
            using var scope = new AssertionScope("object");

            subject.Should().NotBe(notExpected);
            subject.GetHashCode().Should().NotBe(notExpected.GetHashCode());
        }
    }
}
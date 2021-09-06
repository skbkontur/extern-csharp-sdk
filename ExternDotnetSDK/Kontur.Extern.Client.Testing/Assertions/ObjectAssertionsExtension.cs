using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Kontur.Extern.Client.Testing.Assertions
{
    public static class ObjectAssertionsExtension
    {
        public static void BeEqualAndHasSameHashCode(this ObjectAssertions assertions, object expected) => 
            EqualityAssertions.BeEqualAndHasSameHashCode(assertions.Subject, expected);

        public static void NotBeEqualAndHasDifferentHashCode(this ObjectAssertions assertions, object expected) => 
            EqualityAssertions.NotBeEqualAndHasDifferentHashCode(assertions.Subject, expected);
    }
}
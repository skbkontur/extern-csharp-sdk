using FluentAssertions.Numeric;

namespace Kontur.Extern.Client.Testing.Assertions
{
    public static class ComparableAssertionsExtension
    {
        public static void BeEqualAndHasSameHashCode<T>(this ComparableTypeAssertions<T> assertions, object expected) => 
            EqualityAssertions.BeEqualAndHasSameHashCode(assertions.Subject, expected);

        public static void NotBeEqualAndHasDifferentHashCode<T>(this ComparableTypeAssertions<T> assertions, object expected) => 
            EqualityAssertions.NotBeEqualAndHasDifferentHashCode(assertions.Subject, expected);
    }
}
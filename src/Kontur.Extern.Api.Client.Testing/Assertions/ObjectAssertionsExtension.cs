using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Kontur.Extern.Api.Client.Testing.Assertions
{
    public static class ObjectAssertionsExtension
    {
        public static void HasInitializedAtLeastOneProperty(this ObjectAssertions assertions)
        {
            using var scope = new AssertionScope("object");

            var subject = assertions.Subject;
            subject.Should().NotBeNull();

            var properties = subject.GetType().GetProperties();
            if (properties.Length == 0)
                return;

            properties.Select(p => p.GetValue(subject)).Should().Contain(x => x != null);
        }
        
        public static void BeEqualAndHasSameHashCode(this ObjectAssertions assertions, object expected) => 
            EqualityAssertions.BeEqualAndHasSameHashCode(assertions.Subject, expected);

        public static void NotBeEqualAndHasDifferentHashCode(this ObjectAssertions assertions, object expected) => 
            EqualityAssertions.NotBeEqualAndHasDifferentHashCode(assertions.Subject, expected);
    }
}
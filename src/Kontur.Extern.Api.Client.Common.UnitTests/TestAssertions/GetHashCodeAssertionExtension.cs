using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Kontur.Extern.Api.Client.Common.UnitTests.TestAssertions
{
    public static class GetHashCodeAssertionExtension
    {
        public static void BeEqualAndHaveSameHashCode(this ObjectAssertions objectAssertions, object value)
        {
            using (new AssertionScope(objectAssertions.Subject.ToString()))
            {
                objectAssertions.Subject.Should().Be(value);
                objectAssertions.Subject.GetHashCode().Should().Be(value.GetHashCode(), "should have same hash code");
            }
        }
        
        public static void NotBeEqualAndHaveDifferentHashCodes(this ObjectAssertions objectAssertions, object value)
        {
            using (new AssertionScope(objectAssertions.Subject.ToString()))
            {
                objectAssertions.Subject.Should().NotBe(value);
                objectAssertions.Subject.GetHashCode().Should().NotBe(value.GetHashCode(), "should have different hash codes");
            }
        }
    }
}
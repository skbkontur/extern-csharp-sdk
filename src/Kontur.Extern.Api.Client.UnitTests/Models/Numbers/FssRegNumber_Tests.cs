using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Api.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Models.Numbers
{
    [TestFixture]
    internal class FssRegNumber_Tests
    {
        [TestCaseSource(nameof(InvalidStrings))]
        public void Should_fail_when_the_given_number_string_is_invalid(string value)
        {
            Action action = () => FssRegNumber.Parser.Parse(value);

            action.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(ValidStrings))]
        public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
        {
            var parsedValue = FssRegNumber.Parser.Parse(value);

            parsedValue.Kind.Should().Be(AuthorityNumberKind.FssRegNumber);
            parsedValue.Value.Should().Be(value);
        }
            
        private static IEnumerable<string> InvalidStrings
        {
            get
            {
                yield return "-1234567890";
                yield return "1234567890-";
                yield return "12345-67890";
                yield return "12345678901";
                yield return "234567890";
                yield return "z234567890";
                yield return "123456789x";
            }
        }
        
        private static IEnumerable<string> ValidStrings
        {
            get
            {
                yield return "1234567890";
            }
        }
    }
}
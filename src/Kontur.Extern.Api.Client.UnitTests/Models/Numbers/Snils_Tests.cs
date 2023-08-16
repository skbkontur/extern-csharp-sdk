using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Api.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Models.Numbers
{
    [TestFixture]
    public class Snils_Tests
    {
        [TestCaseSource(nameof(InvalidStrings))]
        public void Should_fail_when_the_given_number_string_is_invalid_snils(string value)
        {
            Action action = () => Snils.Parse(value);

            action.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(ValidStrings))]
        public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
        {
            var parsedValue = Snils.Parse(value);

            parsedValue.Kind.Should().Be(AuthorityNumberKind.Snils);
            parsedValue.Value.Should().Be(value);
        }

        private static IEnumerable<string> InvalidStrings
        {
            get
            {
                yield return "123456789012";
                yield return "1234567890";
                yield return "1234567890x";
                yield return "12345-678901";
                yield return " 12345678901";
                yield return "12345678901 ";
                yield return "123-456-789-01";
            }
        }

        private static IEnumerable<string> ValidStrings
        {
            get
            {
                yield return "12345678901";
                yield return "123-456-789 01";
            }
        }
    }
}
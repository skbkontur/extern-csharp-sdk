using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Models.Numbers
{
    [TestFixture]
    internal class Knd_Tests
    {
        [TestCaseSource(nameof(InvalidStrings))]
        public void Should_fail_when_the_given_number_string_is_invalid(string value)
        {
            Action action = () => Knd.Parser.Parse(value);

            action.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(ValidStrings))]
        public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
        {
            var parsedValue = Knd.Parser.Parse(value);

            parsedValue.Kind.Should().Be(AuthorityNumberKind.Knd);
            parsedValue.Value.Should().Be(value);
        }
            
        private static IEnumerable<string> InvalidStrings
        {
            get
            {
                yield return "12345678";
                yield return "123456";
                yield return "123456x";
                yield return "123-4567";
                yield return " 1234567";
            }
        }
        
        private static IEnumerable<string> ValidStrings
        {
            get
            {
                yield return "1234567";
                yield return "8901234";
            }
        }
    }
}
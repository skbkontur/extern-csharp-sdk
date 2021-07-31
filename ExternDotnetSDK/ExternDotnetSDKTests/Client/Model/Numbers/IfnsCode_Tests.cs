using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Client.Model.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Model.Numbers
{
    [TestFixture]
    internal class IfnsCode_Tests
    {
        [TestCaseSource(nameof(InvalidStrings))]
        public void Should_fail_when_the_given_number_string_is_invalid(string value)
        {
            Action action = () => IfnsCode.Parser.Parse(value);

            action.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(ValidStrings))]
        public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
        {
            var parsedValue = IfnsCode.Parser.Parse(value);

            parsedValue.Kind.Should().Be(AuthorityNumberKind.IfnsCode);
            parsedValue.Value.Should().Be(value);
        }
            
        private static IEnumerable<string> InvalidStrings
        {
            get
            {
                yield return "12345";
                yield return "123";
                yield return "123-4";
                yield return "123x";
                yield return "x234";
                yield return " 1234";
                yield return "1234 ";
            }
        }
        
        private static IEnumerable<string> ValidStrings
        {
            get
            {
                yield return "1234";
            }
        }
    }
}
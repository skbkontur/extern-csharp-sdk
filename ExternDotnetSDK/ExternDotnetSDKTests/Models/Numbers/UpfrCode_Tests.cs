using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Models.Numbers
{
    [TestFixture]
    internal class UpfrCode_Tests
    {
        [TestCaseSource(nameof(InvalidStrings))]
        public void Should_fail_when_the_given_number_string_is_invalid(string value)
        {
            Action action = () => UpfrCode.Parser.Parse(value);

            action.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(ValidStrings))]
        public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
        {
            var parsedValue = UpfrCode.Parser.Parse(value);

            parsedValue.Kind.Should().Be(AuthorityNumberKind.UpfrCode);
            parsedValue.Value.Should().Be(value);
        }
            
        private static IEnumerable<string> InvalidStrings
        {
            get
            {
                yield return "123-4561";
                yield return "0123-456";
                yield return "12-3456";
                yield return "123456";
                yield return "123-456-";
                yield return "123--456";
                yield return "123-45x";
                yield return " 123-456";
                yield return "123-456 ";
            }
        }
        
        private static IEnumerable<string> ValidStrings
        {
            get
            {
                yield return "123-456";
                yield return "111-222";
            }
        }
    }
}
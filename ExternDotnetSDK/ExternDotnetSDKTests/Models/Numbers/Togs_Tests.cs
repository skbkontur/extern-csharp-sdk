using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Api.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.Tests.Models.Numbers
{
    [TestFixture]
    internal class Togs_Tests
    {
        [TestCaseSource(nameof(InvalidStrings))]
        public void Should_fail_when_the_given_number_string_is_invalid(string value)
        {
            Action action = () => TogsCode.Parser.Parse(value);

            action.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(ValidStrings))]
        public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
        {
            var parsedValue = TogsCode.Parser.Parse(value);

            parsedValue.Kind.Should().Be(AuthorityNumberKind.Togs);
            parsedValue.Value.Should().Be(value);
        }
            
        private static IEnumerable<string> InvalidStrings
        {
            get
            {
                yield return "12-345";
                yield return "012-34";
                yield return "1234";
                yield return "12-34-";
                yield return "12--34";
                yield return "-12-34";
                yield return "-1234";
                yield return "1234-";
                yield return "12-34 ";
                yield return " 12-34";
                yield return "x2-34";
                yield return "12-3x";
            }
        }
        
        private static IEnumerable<string> ValidStrings
        {
            get
            {
                yield return "12-34";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Api.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.Tests.Models.Numbers
{
    [TestFixture]
    internal class PfrRegNumber_Tests
    {
        [TestCaseSource(nameof(InvalidStrings))]
        public void Should_fail_when_the_given_number_string_is_invalid(string value)
        {
            Action action = () => PfrRegNumber.Parser.Parse(value);

            action.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(ValidStrings))]
        public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
        {
            var parsedValue = PfrRegNumber.Parser.Parse(value);

            parsedValue.Kind.Should().Be(AuthorityNumberKind.PfrRegNumber);
            parsedValue.Value.Should().Be(value);
        }
            
        private static IEnumerable<string> InvalidStrings
        {
            get
            {
                yield return "123-456123456";
                yield return "123456-123456";
                yield return "123456123456";
                yield return "123-456--123456";
                yield return "123--456-123456";
                yield return "123-456-12345x";
                yield return " 123-456-123456";
                yield return "123-456-123456 ";
            }
        }
        
        private static IEnumerable<string> ValidStrings
        {
            get
            {
                yield return "123-456-123456";
                yield return "111-222-333333";
            }
        }
    }
}
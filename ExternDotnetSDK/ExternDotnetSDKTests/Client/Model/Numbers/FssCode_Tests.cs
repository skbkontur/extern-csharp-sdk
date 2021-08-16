using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Client.Model.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Model.Numbers
{
    [TestFixture]
    internal class FssCode_Tests
    {
        [TestCaseSource(nameof(InvalidStrings))]
        public void Should_fail_when_the_given_number_string_is_invalid(string value)
        {
            Action action = () => FssCode.Parser.Parse(value);

            action.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(ValidStrings))]
        public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
        {
            var parsedValue = FssCode.Parser.Parse(value);

            parsedValue.Kind.Should().Be(AuthorityNumberKind.FssCode);
            parsedValue.Value.Should().Be(value);
        }
            
        private static IEnumerable<string> InvalidStrings
        {
            get
            {
                yield return "12345";
                yield return "12343";
                yield return "12340";
                yield return "123451";
                yield return "1234";
                yield return "123-41";
                yield return "1234x";
                yield return "x2341";
                yield return " 12342";
                yield return "12342 ";
            }
        }
        
        private static IEnumerable<string> ValidStrings
        {
            get
            {
                yield return "12341";
                yield return "12342";
            }
        }
    }
}
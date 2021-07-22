using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Client.Model.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Model.Numbers
{
    [TestFixture]
    internal class Kpp_Tests
    {
        [TestCaseSource(nameof(InvalidStrings))]
        public void Should_fail_when_the_given_number_string_is_invalid(string value)
        {
            Action action = () => Kpp.Parser.Parse(value);

            action.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(nameof(ValidStrings))]
        public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
        {
            var parsedValue = Kpp.Parser.Parse(value);

            parsedValue.Kind.Should().Be(AuthorityNumberKind.Kpp);
            parsedValue.Value.Should().Be(value);
        }

        private static IEnumerable<string> InvalidStrings
        {
            get
            {
                yield return "-123456789";
                yield return "123456789-";
                yield return "12345678";
                yield return "12345678z";
                yield return "1234567890";
                yield return "x23456789";
                yield return "12345-6789";
            }
        }

        private static IEnumerable<string> ValidStrings
        {
            get { yield return "123456789"; }
        }
    }
}
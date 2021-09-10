using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Models.Numbers
{
    [TestFixture]
    internal class Kpp_Tests
    {
        [TestCaseSource(nameof(InvalidStrings))]
        public void Should_fail_when_the_given_number_string_is_invalid((string value, string reason) theCase)
        {
            var (value, reason) = theCase;
            
            Action action = () => Kpp.Parser.Parse(value);

            action.Should().Throw<ArgumentException>(reason);
        }

        [TestCaseSource(nameof(ValidStrings))]
        public void Should_successfully_return_a_number_when_the_given_value_is_valid((string value, string reason) theCase)
        {
            var (value, reason) = theCase;
            
            var parsedValue = Kpp.Parser.Parse(value);

            parsedValue.Kind.Should().Be(AuthorityNumberKind.Kpp);
            parsedValue.Value.Should().Be(value, reason);
        }

        private static IEnumerable<(string value, string reason)> InvalidStrings
        {
            get
            {
                yield return ("-123456789", "minus is not allowed at the begging");
                yield return ("123456789-", "minus is not allowed at the end");
                yield return ("12345678", "the code have to have 9 symbols, but given 8");
                yield return ("12345678z", "the code cannot cannot have a letter");
                yield return ("1234567890", "the code have to have 9 symbols");
                yield return ("x23456789", "the code have to have 9 symbols, but given 10");
                yield return ("12345-6789", "minus is not allowed in the middle");
                yield return ("4516az694", "the code cannot have LOWER case letters in 5th and 6th positions");
                yield return ("451AAZ694", "the code cannot have any letters in 4th position");
                yield return ("451601AZ4", "the code cannot have any letters in 7th and 8th positions");
                yield return ("4516016AZ", "the code cannot have any letters in 8th and 9th positions");
                yield return ("45AZ01694", "the code cannot have any letters in 3th and 4th positions");
                yield return ("AZ1601694", "the code cannot have any letters in 1st and 2nd positions");
                yield return ("451AAZZ94", "the code cannot have any letters in 4th and 7th positions");
            }
        }

        private static IEnumerable<(string value, string reason)> ValidStrings
        {
            get
            {
                yield return ("123456789", "code with 9 digits");
                yield return ("451601694", "code with 9 digits");
                yield return ("4516AZ694", "code 9 symbols, all digits except 5th and 6th symbols, which are be UPPER case latin letters");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Api.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Models.Numbers
{
    [TestFixture]
    internal class Okpo_Tests
    {
        [TestFixture]
        internal class IndividualEntrepreneurOkpo
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid(string value)
            {
                Action action = () => Okpo.IndividualEntrepreneur.Parse(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
            {
                var parsedValue = Okpo.IndividualEntrepreneur.Parse(value);

                parsedValue.Kind.Should().Be(AuthorityNumberKind.Okpo);
                parsedValue.Value.Should().Be(value);
            }

            public static IEnumerable<string> InvalidStrings
            {
                get
                {
                    yield return "12345678901";
                    yield return "123456789";
                    yield return "123456789x";
                    yield return "12345-67890";
                    yield return " 1234567890";
                    yield return "1234567890 ";
                }
            }

            public static IEnumerable<string> ValidStrings
            {
                get
                {
                    yield return "1234567890";
                }
            }
        }
        
        [TestFixture]
        internal class LegalEntityOkpo
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid(string value)
            {
                Action action = () => Okpo.LegalEntity.Parse(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
            {
                var parsedValue = Okpo.LegalEntity.Parse(value);

                parsedValue.Kind.Should().Be(AuthorityNumberKind.Okpo);
                parsedValue.Value.Should().Be(value);
            }

            public static IEnumerable<string> InvalidStrings
            {
                get
                {
                    yield return "123456789";
                    yield return "1234567";
                    yield return "1234567x";
                    yield return "123-45678";
                    yield return " 12345678";
                    yield return "12345678 ";
                }
            }

            public static IEnumerable<string> ValidStrings
            {
                get
                {
                    yield return "12345678";
                    yield return "89012345";
                }
            }
        }
        
        [TestFixture]
        internal class ParseAny
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid_okpo(string value)
            {
                Action action = () => Okpo.ParseAny(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
            {
                var parsedValue = Okpo.ParseAny(value);

                parsedValue.Kind.Should().Be(AuthorityNumberKind.Okpo);
                parsedValue.Value.Should().Be(value);
            }
            
            private static IEnumerable<string> InvalidStrings => 
                IndividualEntrepreneurOkpo.InvalidStrings.Concat(LegalEntityOkpo.InvalidStrings);

            private static IEnumerable<string> ValidStrings =>
                IndividualEntrepreneurOkpo.ValidStrings.Concat(LegalEntityOkpo.ValidStrings);
        }
    }
}
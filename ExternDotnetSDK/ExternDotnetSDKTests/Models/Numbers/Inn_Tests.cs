using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Models.Numbers
{
    [TestFixture]
    internal class Inn_Tests
    {
        [TestFixture]
        internal class InnKppOfLegalEntity
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid(string value)
            {
                Action action = () => InnKpp.Parser.Parse(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
            {
                var parsedValue = InnKpp.Parser.Parse(value);

                parsedValue.Kind.Should().Be(AuthorityNumberKind.InnKpp);
                parsedValue.Value.Should().Be(value);
            }

            private static IEnumerable<string> InvalidStrings
            {
                get
                {
                    yield return "1234567890--123456789";
                    yield return "1234567890-12345678";
                    yield return "123456789-123456789";
                    yield return "12345678901-123456789";
                    yield return "1234567890-1234567891";
                    yield return "1234-567890-12345-6789";
                    yield return "1234567890-12345678x";
                    yield return "1234x67890-123456789";
                    yield return "1234567890-123456789 ";
                    yield return " 1234567890-123456789";
                }
            }

            private static IEnumerable<string> ValidStrings
            {
                get { yield return "1234567890-123456789"; }
            }
        }
        
        [TestFixture]
        internal class InnOfLegalEntity
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid(string value)
            {
                Action action = () => LegalEntityInn.Parser.Parse(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
            {
                var parsedValue = LegalEntityInn.Parser.Parse(value);

                parsedValue.Kind.Should().Be(AuthorityNumberKind.LegalEntityInn);
                parsedValue.Value.Should().Be(value);
            }

            private static IEnumerable<string> InvalidStrings
            {
                get
                {
                    yield return "1234567890-";
                    yield return "-1234567890";
                    yield return "123456-7890";
                    yield return "123456789z";
                    yield return "x234567890";
                    yield return "123456789012";
                    yield return "12345678901";
                }
            }

            private static IEnumerable<string> ValidStrings
            {
                get { yield return "1234567890"; }
            }
        }

        [TestFixture]
        internal class IndividualEntrepreneurInn
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid(string value)
            {
                Action action = () => Inn.Parser.Parse(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
            {
                var parsedValue = Inn.Parser.Parse(value);

                parsedValue.Kind.Should().Be(AuthorityNumberKind.Inn);
                parsedValue.Value.Should().Be(value);
            }

            private static IEnumerable<string> InvalidStrings
            {
                get
                {
                    yield return "1234567890121";
                    yield return "12345678901";
                    yield return "12345-6789012";
                    yield return "12345678901x";
                    yield return "123456789012 ";
                    yield return " 123456789012";
                }
            }

            private static IEnumerable<string> ValidStrings
            {
                get { yield return "123456789012"; }
            }
        }
    }
}
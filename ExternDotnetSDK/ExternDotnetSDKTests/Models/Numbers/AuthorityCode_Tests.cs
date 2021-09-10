using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Models.Numbers
{
    [TestFixture]
    internal class AuthorityCode_Tests
    {
        [TestFixture]
        internal class Pfr
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid(string value)
            {
                Action action = () => AuthorityCode.Pfr.Parse(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
            {
                var parsedValue = AuthorityCode.Pfr.Parse(value);

                parsedValue.Kind.Should().Be(AuthorityNumberKind.PfrAuthorityCode);
                parsedValue.Value.Should().Be(value);
            }

            private static IEnumerable<string> InvalidStrings
            {
                get
                {
                    yield return "123456";
                    yield return "12345";
                    yield return "123-45";
                    yield return "23-456";
                    yield return "12x-456";
                    yield return "123-45-6";
                    yield return "123--456";
                }
            }

            private static IEnumerable<string> ValidStrings
            {
                get
                {
                    yield return "123-456";
                    yield return "789-012";
                }
            }
        }

        [TestFixture]
        internal class Fns
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid(string value)
            {
                Action action = () => AuthorityCode.Fns.Parse(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
            {
                var parsedValue = AuthorityCode.Fns.Parse(value);

                parsedValue.Kind.Should().Be(AuthorityNumberKind.FnsAuthorityCode);
                parsedValue.Value.Should().Be(value);
            }

            private static IEnumerable<string> InvalidStrings
            {
                get
                {
                    yield return "12345";
                    yield return "123";
                    yield return "123x";
                    yield return "xxxx";
                    yield return "12-34";
                    yield return "12-3";
                }
            }

            private static IEnumerable<string> ValidStrings
            {
                get
                {
                    yield return "1234";
                    yield return "5678";
                    yield return "9012";
                }
            }
        }

        [TestFixture]
        internal class Fss
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid(string value)
            {
                Action action = () => AuthorityCode.Fss.Parse(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
            {
                var parsedValue = AuthorityCode.Fss.Parse(value);

                parsedValue.Kind.Should().Be(AuthorityNumberKind.FssAuthorityCode);
                parsedValue.Value.Should().Be(value);
            }

            private static IEnumerable<string> InvalidStrings
            {
                get
                {
                    yield return "1234";
                    yield return "123456";
                    yield return "12-345";
                    yield return "12-34-5";
                    yield return "123";
                    yield return "123xx";
                }
            }

            private static IEnumerable<string> ValidStrings
            {
                get
                {
                    yield return "12345";
                    yield return "56789";
                    yield return "89012";
                }
            }
        }

        [TestFixture]
        internal class Rosstat
        {
            [TestCaseSource(nameof(InvalidStrings))]
            public void Should_fail_when_the_given_number_string_is_invalid(string value)
            {
                Action action = () => AuthorityCode.Rosstat.Parse(value);

                action.Should().Throw<ArgumentException>();
            }

            [TestCaseSource(nameof(ValidStrings))]
            public void Should_successfully_return_a_number_when_the_given_value_is_valid(string value)
            {
                var parsedValue = AuthorityCode.Rosstat.Parse(value);

                parsedValue.Kind.Should().Be(AuthorityNumberKind.RosstatAuthorityCode);
                parsedValue.Value.Should().Be(value);
            }

            private static IEnumerable<string> InvalidStrings
            {
                get
                {
                    yield return "11-1";
                    yield return "1-11";
                    yield return "11-111";
                    yield return "1111";
                    yield return "111-111";
                    yield return "111-11";
                    yield return "xx-xx";
                }
            }

            private static IEnumerable<string> ValidStrings
            {
                get
                {
                    for (var iFirst = 0; iFirst < 9; iFirst++)
                    {
                        for (var iSecond = 0; iSecond < 9; iSecond++)
                        {
                            yield return $"{iFirst}1-0{iSecond}";
                        }
                    }
                }
            }
        }
    }
}
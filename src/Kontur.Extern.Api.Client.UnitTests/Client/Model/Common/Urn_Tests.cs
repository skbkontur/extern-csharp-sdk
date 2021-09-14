#nullable enable
using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Testing.Assertions;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.Common
{
    internal class Urn_Tests
    {
        private static IEnumerable<string?> InvalidUrnStrings
        {
            get
            {
                yield return null;
                yield return "";
                yield return " ";
                yield return "value";
                yield return " urn:value";
                yield return "urn:value ";
            }
        }
        
        private static IEnumerable<string?> InvalidSegments
        {
            get
            {
                yield return null;
                yield return "";
                yield return " ";
            }
        }

        [TestFixture]
        internal class TryParse
        {
            [TestCaseSource(typeof(Urn_Tests), nameof(InvalidUrnStrings))]
            public void Should_return_error_when_given_an_invalid_string(string value)
            {
                var success = Urn.TryParse(value, out var urn);

                success.Should().BeFalse();
                urn.Should().BeNull();
            }
            
            [Test]
            public void Should_return_successfully_parse_full_URN()
            {
                var success = Urn.TryParse("urn:ns:val", out var urn);

                success.Should().BeTrue();
                urn.Nid.Should().Be("ns");
                urn.Nss.Should().Be("val");
            }
            
            [Test]
            public void Should_return_successfully_parse_URN_with_namespace_specific_string_only()
            {
                var success = Urn.TryParse("urn:ns:val", out var urn);

                success.Should().BeTrue();
                urn.Nid.Should().Be("ns");
                urn.Nss.Should().Be("val");
            }
        }

        [TestFixture]
        internal class Parse
        {
            [TestCaseSource(typeof (Urn_Tests), nameof(InvalidUrnStrings))]
            public void Should_fail_when_given_an_invalid_string_value(string? invalidValue)
            {
                Action action = () => Urn.Parse(invalidValue!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_initialize_with_full_urn_string()
            {
                var urn = Urn.Parse("urn:ns:val");

                urn.Nid.Should().Be("ns");
                urn.Nss.Should().Be("val");
            }

            [Test]
            public void Should_initialize_with_namespace_specific_string_only()
            {
                var urn = Urn.Parse("urn:ns");

                urn.Nid.Should().BeEmpty();
                urn.Nss.Should().Be("ns");
            }
        }

        [TestFixture]
        internal class Ctor
        {
            [TestFixture]
            internal class ByParts 
            {
                [TestCaseSource(typeof(Urn_Tests), nameof(InvalidSegments))]
                public void Should_fail_when_given_invalid_namespace_identifier(string? value)
                {
                    Action action = () => _ = new Urn(value!, "nss");

                    action.Should().Throw<ArgumentException>();
                }
            
                [TestCaseSource(typeof(Urn_Tests), nameof(InvalidSegments))]
                public void Should_fail_when_given_invalid_Namespace_specific_string(string? value)
                {
                    Action action = () => _ = new Urn("nid", value!);

                    action.Should().Throw<ArgumentException>();
                }

                [Test]
                public void Should_initialize_urn_full_urn()
                {
                    var urn = new Urn("ns", "val");

                    urn.Nid.Should().Be("ns");
                    urn.Nss.Should().Be("val");
                }
            }

            [TestFixture]
            internal class Combinatorial
            {
                [Test]
                public void Should_fail_when_given_null_parent()
                { 
                    Action action = () => _ = new Urn((Urn?) null!, "child");

                    action.Should().Throw<ArgumentException>();
                }

                [TestCaseSource(typeof(Urn_Tests), nameof(InvalidSegments))]
                public void Should_fail_when_given_invalid_child(string child)
                {
                    var parent = Urn.Parse("urn:ns");
                
                    Action action = () => _ = new Urn(parent, child);

                    action.Should().Throw<ArgumentException>();
                }
            }
        }

        [TestFixture]
        internal class IsParentOf
        {
            [TestCase("urn:ns", "urn:ns:val", ExpectedResult = true)]
            [TestCase("urn:other", "urn:ns:val", ExpectedResult = false)]
            [TestCase("urn:ns:val", "urn:ns:val", ExpectedResult = false)]
            public bool Should_indicate_that_urn_is_parent(string parentValue, string childValue)
            {
                var parent = Urn.Parse(parentValue);
                var child = Urn.Parse(childValue);

                return parent.IsParentOf(child);
            }

            [Test]
            public void Should_fail_when_given_null_child()
            {
                var parent = Urn.Parse("urn:ns");

                Action action = () => parent.IsParentOf(null!);

                action.Should().Throw<ArgumentException>();
            }
        }

        [TestFixture]
        internal class IsChildOf
        {
            [TestCase("urn:ns", "urn:ns:val", ExpectedResult = true)]
            [TestCase("urn:other", "urn:ns:val", ExpectedResult = false)]
            [TestCase("urn:ns:val", "urn:ns:val", ExpectedResult = false)]
            public bool Should_indicate_that_urn_is_child(string parentValue, string childValue)
            {
                var parent = Urn.Parse(parentValue);
                var child = Urn.Parse(childValue);

                return child.IsChildOf(parent);
            }

            [Test]
            public void Should_fail_when_given_null_parent()
            {
                var child = Urn.Parse("urn:ns:val");

                Action action = () => child.IsChildOf(null!);

                action.Should().Throw<ArgumentException>();
            }
        }

        [TestFixture]
        internal class Append
        {
            [Test]
            public void Should_combine_urn_to_new_urn()
            {
                var parent = Urn.Parse("urn:ns");
                
                var child = parent.Append("val");

                child.ToString().Should().Be("urn:ns:val");
            }

            [TestCaseSource(typeof(Urn_Tests), nameof(InvalidSegments))]
            public void Should_fail_when_given_invalid_segment(string? segment)
            {
                var parent = Urn.Parse("urn:ns");

                Action action = () => parent.Append(segment!);

                action.Should().Throw<ArgumentException>();
            }
        }

        [TestFixture]
        internal class Equality
        {
            [Test]
            public void Should_be_equality_comparable()
            {
                var urn = Urn.Parse("urn:ns:val");
                var equal = Urn.Parse("urn:ns:val");
                var notEqual = Urn.Parse("urn:ns:another");
                var parent = Urn.Parse("urn:ns");
                var anotherNs = Urn.Parse("urn:other:val");

                urn.Should().BeEqualAndHasSameHashCode(equal);
                urn.Should().NotBeEqualAndHasDifferentHashCode(notEqual);
                urn.Should().NotBeEqualAndHasDifferentHashCode(parent);
                urn.Should().NotBeEqualAndHasDifferentHashCode(anotherNs);
            }
        }
    }
}
#nullable enable
using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Testing.Assertions;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.Common
{
    [TestFixture]
    internal class Link_Tests
    {
        private const string DefaultRel = "index";
        private static readonly Uri DefaultHref = new Uri("https://htmlbook.ru/");

        [TestFixture]
        internal class Ctor
        {
            [Test]
            public void Should_fail_when_given_null_href()
            {
                Action action = () => _ = new Link(null!, DefaultRel);

                action.Should().Throw<ArgumentException>(); 
            }
            
            [Test]
            public void Should_fail_when_given_null_rel()
            {
                Action action = () => _ = new Link(DefaultHref, null!);

                action.Should().Throw<ArgumentException>(); 
            }

            [Test]
            public void Should_initialize_short_link()
            {
                var link = new Link(DefaultHref, DefaultRel);

                link.Should().BeEquivalentTo(new
                {
                    Href = DefaultHref,
                    Rel = DefaultRel,
                    Name = (string?) null,
                    Title = (string?) null,
                    Profile = (string?) null,
                    Templated = false
                });
            }

            [Test]
            public void Should_initialize_full_link()
            {
                const string name = "name";
                const string title = "title";
                const string profile = "profile";
                
                var link = new Link(DefaultHref, DefaultRel, name, title, profile, true);

                link.Should().BeEquivalentTo(new
                {
                    Href = DefaultHref,
                    Rel = DefaultRel,
                    Name = name, 
                    Title = title,
                    Profile = profile,
                    Templated = true
                });
            }
        }

        [TestFixture]
        internal class Equality
        {
            [Test]
            public void Should_be_equal_to_short_link()
            {
                var shortLink = new Link(DefaultHref, DefaultRel);
                var equalLink = new Link(DefaultHref, DefaultRel);
                var anotherRelLink = new Link(DefaultHref, "some");
                var anotherHrefLink = new Link(new Uri("https://google.com/"), DefaultRel);
                var fullLink = new Link(DefaultHref, DefaultRel, "name", "title", "profile", true);

                shortLink.Should().BeEqualAndHasSameHashCode(shortLink);
                shortLink.Should().BeEqualAndHasSameHashCode(equalLink);
                shortLink.Should().NotBeEqualAndHasDifferentHashCode(fullLink);
                shortLink.Should().NotBeEqualAndHasDifferentHashCode(anotherHrefLink);
                shortLink.Should().NotBeEqualAndHasDifferentHashCode(anotherRelLink);
            }
            
            [Test]
            public void Should_be_equal_to_full_link()
            {
                const string profile = "profile";
                const string name = "name";
                const string title = "title";
                const bool templated = true;
                var fullLink = new Link(DefaultHref, DefaultRel, name, title, profile, templated);
                var shortLink = new Link(DefaultHref, DefaultRel);
                var equalLink = new Link(DefaultHref, DefaultRel, name, title, profile, templated);
                var anotherRelLink = new Link(DefaultHref, "some", name, title, profile, templated);
                var anotherHrefLink = new Link(new Uri("https://google.com/"), DefaultRel, name, title, profile, templated);
                var anotherName = new Link(DefaultHref, DefaultRel, "another", title, profile, templated);
                var anotherTitle = new Link(DefaultHref, DefaultRel, name, "another", profile, templated);
                var anotherProfile = new Link(DefaultHref, DefaultRel, name, title, "another", templated);
                var anotherTemplated = new Link(DefaultHref, DefaultRel, name, title, profile);

                fullLink.Should().BeEqualAndHasSameHashCode(fullLink);
                fullLink.Should().BeEqualAndHasSameHashCode(equalLink);
                fullLink.Should().NotBeEqualAndHasDifferentHashCode(shortLink);
                fullLink.Should().NotBeEqualAndHasDifferentHashCode(anotherHrefLink);
                fullLink.Should().NotBeEqualAndHasDifferentHashCode(anotherRelLink);
                fullLink.Should().NotBeEqualAndHasDifferentHashCode(anotherName);
                fullLink.Should().NotBeEqualAndHasDifferentHashCode(anotherTitle);
                fullLink.Should().NotBeEqualAndHasDifferentHashCode(anotherProfile);
                fullLink.Should().NotBeEqualAndHasDifferentHashCode(anotherTemplated);
            }
        }

        [TestFixture]
        internal class Formatting
        {
            [TestCaseSource(nameof(FormattingCases))]
            public void Should_format_to_sting_as_XML_node((string? name, string? title, string? profile, bool templated, string expected) theCase)
            {
                var (name, title, profile, templated, expected) = theCase;
                var link = new Link(DefaultHref, DefaultRel, name, title, profile, templated);

                var formatted = link.ToString();
                
                formatted.Should().Be(expected);
            }

            private static IEnumerable<(string? name, string? title, string? profile, bool templated, string expected)> FormattingCases
            {
                get
                {
                    yield return ("NAME", "TITLE", "PROFILE", true, 
                        $"<link rel=\"{DefaultRel}\" name=\"NAME\" profile=\"PROFILE\" title=\"TITLE\" templated=\"true\" href=\"{DefaultHref}\" />");
                    yield return ("NAME", "TITLE", "PROFILE", false, 
                        $"<link rel=\"{DefaultRel}\" name=\"NAME\" profile=\"PROFILE\" title=\"TITLE\" href=\"{DefaultHref}\" />");
                    yield return ("NAME", "TITLE", null, false, 
                        $"<link rel=\"{DefaultRel}\" name=\"NAME\" title=\"TITLE\" href=\"{DefaultHref}\" />");
                    yield return ("NAME", null, null, false, 
                        $"<link rel=\"{DefaultRel}\" name=\"NAME\" href=\"{DefaultHref}\" />");
                    yield return (null, null, null, false, 
                        $"<link rel=\"{DefaultRel}\" href=\"{DefaultHref}\" />");
                }
            }
        }
    }
}
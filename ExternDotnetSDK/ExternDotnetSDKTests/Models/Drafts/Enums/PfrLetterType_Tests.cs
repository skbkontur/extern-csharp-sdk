using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Drafts.Enums;
using Kontur.Extern.Client.Tests.Client.Model.TestAssertions;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Models.Drafts.Enums
{
    [TestFixture]
    internal class PfrLetterType_Tests
    {
        [TestFixture]
        internal class StaticCtor
        {
            [Test]
            public void Should_initialize_namespace_field()
            {
                var ns = PfrLetterType.Namespace;

                ns.Should().NotBeNull();
            }
        }
        
        [TestFixture]
        internal class Ctor
        {
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            [TestCase("not a urn")]
            public void Should_fail_when_given_invalid_urn(string urn)
            {
                Action action = () => _ = new PfrLetterType(urn);

                action.Should().Throw<Exception>();
            }

            [Test]
            public void Should_fail_when_given_null_urn()
            {
                Action action = () => _ = new PfrLetterType((null as Urn)!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_fail_when_urn_not_belong_to_docflow_type_namespace()
            {
                Action action = () => _ = new PfrLetterType("urn:document:fss-sedo-error-exchange-error");

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_initialize_with_given_urn()
            {
                var letterType = new PfrLetterType("urn:pfr-letter-category:letter");

                letterType.ToString().Should().Be("urn:pfr-letter-category:letter");
            }
        }
        
        [TestFixture]
        internal class PredefinedStates
        {
            [TestCaseSource(nameof(AllPredefinedPfrLetterTypes))]
            public void Should_have_not_empty_values((FieldInfo field, PfrLetterType? letterType) predefinedType)
            {
                var (_, letterType) = predefinedType;
                
                letterType.Should().NotBeNull();
                letterType!.Value.ToUrn().Should().NotBeNull();
            }

            [Test]
            public void Should_be_unique() => 
                AllPredefinedPfrLetterTypes.MembersShouldHaveUniqueValues();

            private static IEnumerable<(FieldInfo field, PfrLetterType? status)> AllPredefinedPfrLetterTypes => 
                EnumLikeType.AllEnumMembersOfStruct<PfrLetterType>();
        }
    }
}
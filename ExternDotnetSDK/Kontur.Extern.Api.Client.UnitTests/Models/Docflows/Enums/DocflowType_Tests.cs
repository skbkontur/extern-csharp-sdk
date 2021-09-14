using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Tests.Client.Model.TestAssertions;
using Kontur.Extern.Api.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.Tests.Models.Docflows.Enums
{
    [TestFixture]
    internal class DocflowType_Tests
    {
        [TestFixture]
        internal class StaticCtor
        {
            [Test]
            public void Should_initialize_namespace_field()
            {
                var ns = DocflowType.Namespace;

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
                Action action = () => _ = new DocflowType(urn);

                action.Should().Throw<Exception>();
            }

            [Test]
            public void Should_fail_when_given_null_urn()
            {
                Action action = () => _ = new DocflowType((null as Urn)!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_fail_when_urn_not_belong_to_docflow_type_namespace()
            {
                Action action = () => _ = new DocflowType("urn:document:fss-sedo-error-exchange-error");

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_initialize_with_given_urn()
            {
                var docflowType = new DocflowType("urn:docflow:fss-sedo-sick-report-change-notification");

                docflowType.ToString().Should().Be("urn:docflow:fss-sedo-sick-report-change-notification");
            }
        }
        
        [TestFixture]
        internal class PredefinedTypes
        {
            [TestCaseSource(nameof(AllPredefinedDocflowTypes))]
            public void Should_have_not_empty_values((FieldInfo field, DocflowType? docflowType) predefinedType)
            {
                var (_, docflowType) = predefinedType;
                
                docflowType.Should().NotBeNull();
                docflowType!.Value.ToUrn().Should().NotBeNull();
            }

            [Test]
            public void Should_be_unique() => 
                AllPredefinedDocflowTypes.MembersShouldHaveUniqueValues();

            private static IEnumerable<(FieldInfo field, DocflowType? docflowType)> AllPredefinedDocflowTypes => 
                EnumLikeType.AllEnumMembersFromNestedTypesOfStruct<DocflowType>();
        }
    }
}
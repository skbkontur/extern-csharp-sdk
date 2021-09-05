#nullable enable
using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Kontur.Extern.Client.Model.DraftBuilders;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Tests.Client.Model.TestAssertions;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Model.DraftBuilders
{
    [TestFixture]
    internal class DraftBuilderType_Tests
    {
        [TestFixture]
        internal class StaticCtor
        {
            [Test]
            public void Should_initialize_namespace_field()
            {
                var ns = DraftBuilderType.Namespace;

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
            public void Should_fail_when_given_an_invalid_urn(string urn)
            {
                Action action = () => _ = new DraftBuilderType(urn);

                action.Should().Throw<Exception>();
            }
            
            [Test]
            public void Should_fail_when_given_null_urn()
            {
                Action action = () => _ = new DraftBuilderType((null as Urn)!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_fail_when_urn_not_belong_to_draft_builder_type_namespace()
            {
                Action action = () => _ = new DraftBuilderType("urn:docflow:fss-sedo-sick-report-change-notification");

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_initialize_with_given_urn()
            {
                var documentType = new DraftBuilderType("urn:drafts-builder:fss-sedo-error-exchange-error");

                documentType.ToString().Should().Be("urn:drafts-builder:fss-sedo-error-exchange-error");
            }
        }
        
        [TestFixture]
        internal class PredefinedTypes
        {
            [TestCaseSource(nameof(AllPredefinedDocumentBuilderTypes))]
            public void Should_have_not_empty_document_types((FieldInfo field, DraftBuilderType? draftBuilderType) predefinedType)
            {
                var (_, draftBuilderType) = predefinedType;
                
                draftBuilderType.Should().NotBeNull();
                draftBuilderType!.Value.ToUrn().Should().NotBeNull();
            }

            [Test]
            public void Should_have_unique_document_types() =>
                AllPredefinedDocumentBuilderTypes.MembersShouldHaveUniqueValues();

            private static IEnumerable<(FieldInfo field, DraftBuilderType? draftBuilderType)> AllPredefinedDocumentBuilderTypes => 
                EnumLikeType.AllEnumMembersFromNestedTypesOfStruct<DraftBuilderType>();
        }
    }
}
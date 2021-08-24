using System;
using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Tests.Client.Model.TestAssertions;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Model.Documents
{
    [TestFixture]
    internal class DocumentType_Tests
    {
        [TestFixture]
        internal class StaticCtor
        {
            [Test]
            public void Should_initialize_namespace_field()
            {
                var ns = DocumentType.Namespace;

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
                Action action = () => _ = new DocumentType(urn);

                action.Should().Throw<Exception>();
            }
            
            [Test]
            public void Should_fail_when_given_null_urn()
            {
                Action action = () => _ = new DocumentType((null as Urn)!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_fail_when_urn_not_belong_to_document_type_namespace()
            {
                Action action = () => _ = new DocumentType("urn:docflow:fss-sedo-sick-report-change-notification");

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_initialize_with_given_urn()
            {
                var documentType = new DocumentType("urn:document:fss-sedo-error-exchange-error");

                documentType.ToString().Should().Be("urn:document:fss-sedo-error-exchange-error");
            }
        }

        [TestFixture]
        internal class PredefinedTypes
        {
            [TestCaseSource(nameof(AllPredefinedDocumentTypes))]
            public void Should_have_not_empty_values((FieldInfo field, DocumentType? documentType) predefinedType)
            {
                var (_, documentType) = predefinedType;
                
                documentType.Should().NotBeNull();
                documentType!.Value.ToUrn().Should().NotBeNull();
            }

            [Test]
            public void Should_be_unique() =>
                AllPredefinedDocumentTypes.MembersShouldHaveUniqueValuesExcept(
                    DocumentType.Pfr.PfrReportV2.Attachment,
                    DocumentType.Pfr.PfrReportV2.ProtocolAppendix
                );

            private static IEnumerable<(FieldInfo field, DocumentType? documentType)> AllPredefinedDocumentTypes => 
                EnumLikeType.AllEnumMembersFromNestedTypesOfStruct<DocumentType>();
        }
    }
}
#nullable enable
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
        internal class IsBelongTo
        {
            [TestCase("some-doc", "urn:document:some-doc", true)]
            [TestCase("some-doc", "urn:document:some", true)]
            [TestCase("some-doc-of-something", "urn:document:some-doc", true)]
            [TestCase("doc-of-something", "urn:document:some-doc", false)]
            [TestCase("doc", "urn:document:some-doc", false)]
            [TestCase("some-doc", "urn:some-doc", false)]
            [TestCase("some-doc", "urn:docflow:some-doc", false)]
            [TestCase("some-doc", null, false)]
            public void Should_indicate_that_document_type_is_belong_to_namespace(string documentSuffix, string? namespaceValue, bool expectedResult)
            {
                var @namespace = namespaceValue is null ? null : new Urn(namespaceValue);
                
                DocumentType documentType = $"urn:document:{documentSuffix}";

                documentType.IsBelongTo(@namespace!).Should().Be(expectedResult);
            }
        }
        
        [TestFixture]
        internal class PredefinedTypes
        {
            [TestCaseSource(nameof(AllPredefinedDocumentTypes))]
            public void Should_have_not_empty_document_types((FieldInfo field, DocumentType? documentType) predefinedType)
            {
                var (_, documentType) = predefinedType;
                
                documentType.Should().NotBeNull();
                documentType!.Value.ToUrn().Should().NotBeNull();
            }
            
            [TestCaseSource(nameof(AllPredefinedNamespaces))]
            public void Should_have_not_empty_namespaces((FieldInfo field, Urn? @namespace) predefinedType)
            {
                var (_, @namespace) = predefinedType;
                
                @namespace.Should().NotBeNull();
            }

            [Test]
            public void Should_have_unique_document_types() =>
                AllPredefinedDocumentTypes.MembersShouldHaveUniqueValuesExcept(
                    DocumentType.Pfr.PfrReportV2.Attachment,
                    DocumentType.Pfr.PfrReportV2.ProtocolAppendix
                );

            [Test]
            public void Should_have_unique_namespaces() =>
                AllPredefinedNamespaces.MembersShouldHaveUniqueValues();

            private static IEnumerable<(FieldInfo field, DocumentType? documentType)> AllPredefinedDocumentTypes => 
                EnumLikeType.AllEnumMembersFromNestedTypesOfStruct<DocumentType>();
            
            private static IEnumerable<(FieldInfo field, Urn? @namespace)> AllPredefinedNamespaces => 
                EnumLikeType.AllEnumMembersFromNestedTypes<DocumentType, Urn>();
        }
    }
}
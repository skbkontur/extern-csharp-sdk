using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Requisites;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.Docflows;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers;
using NUnit.Framework;
using Vostok.Logging.Console;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.Docflows
{
    [TestFixture]
    internal class DocflowDocumentDescriptionConverter_Tests
    {
        private static IJsonSerializer serializer = null!;
        private DocflowDocumentDescriptionGenerator descriptionGenerator = null!;

        [SetUp]
        public void SetUp()
        {
            serializer = JsonSerializerFactory.CreateJsonSerializer(new ConsoleLog(), ignoreNullValues: false);
            descriptionGenerator = new DocflowDocumentDescriptionGenerator();
        }

        [TestCaseSource(nameof(DocumentTypeToRequisitesCases))]
        public void Should_deserialize_document_description_with_requisites_by_it_document_type(RequisitesCase requisitesCase)
        {
            var (json, expectedDescription) = requisitesCase.GenerateDescription(serializer, descriptionGenerator);
            Console.WriteLine($"Generated JSON: {json}");

            var description = serializer.Deserialize<DocflowDocumentDescription>(json);

            description.Type.Should().Be(requisitesCase.DocumentType);
            DescriptionShouldBeEqual(description, expectedDescription);
        }

        [Test]
        public void Should_deserialize_common_requisites_in_case_of_unknown_document_type()
        {
            var unknownDocumentType = new DocumentType(DocumentType.Namespace.Append("unknown-document"));
            var documentDescription = descriptionGenerator.GenerateWithoutRequisites(unknownDocumentType);
            documentDescription.Requisites = new PfrReportRequisites
            {
                CorrectionType = PfrReportCorrectionType.PensionAssignment
            };
            var json = serializer.SerializeToIndentedString(documentDescription);
            Console.WriteLine($"Generated JSON: {json}");

            var description = serializer.Deserialize<DocflowDocumentDescription>(json);

            description.Type.Should().Be(unknownDocumentType);
            description.Requisites.Should().BeOfType<CommonDocflowDocumentRequisites>();
        }

        private static void DescriptionShouldBeEqual(DocflowDocumentDescription description, DocflowDocumentDescription expectedDescription)
        {
            description.Should().BeEquivalentTo(expectedDescription);
            description.Requisites.Should().BeOfType(expectedDescription.Requisites?.GetType());
        }

        public static IEnumerable<RequisitesCase> DocumentTypeToRequisitesCases =>
            EnumLikeType
                .AllEnumValuesFromNestedTypesOfStruct<DocumentType>()
                .Select(dt => new RequisitesCase(dt, DocumentDescriptionRequisitesTypes.GetRequisiteType(dt)));

        public record RequisitesCase
        {
            private readonly Type requisitesType;

            public RequisitesCase(DocumentType documentType, Type requisitesType)
            {
                DocumentType = documentType;
                this.requisitesType = requisitesType;
            }

            public (string json, DocflowDocumentDescription expectedDescription) GenerateDescription(IJsonSerializer serializer, DocflowDocumentDescriptionGenerator descriptionGenerator)
            {
                var docflow = descriptionGenerator.GenerateWithRequisites(requisitesType, DocumentType);
                var json = serializer.SerializeToIndentedString(docflow);
                return (json, docflow);
            }

            public DocumentType DocumentType { get; }

            public override string ToString() => $"{DocumentType} -> {requisitesType.Name}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.Docflows;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.Docflows
{
    [TestFixture]
    internal class DocflowConverter_Tests
    {
        private static IJsonSerializer serializer = null!;
        private DocflowDescriptionGenerator descriptionGenerator = null!;
            
        [SetUp]
        public void SetUp()
        {
            serializer = JsonSerializerFactory.CreateJsonSerializer();
            descriptionGenerator = new DocflowDescriptionGenerator();
        }

        [TestCaseSource(nameof(TypeToDescriptionCases))]
        public void Should_deserialize_docflow_with_documents(DescriptionCase descriptionCase)
        {
            var (json, expectedDocflow) = descriptionCase.GenerateWithDocuments(serializer, descriptionGenerator);
            Console.WriteLine($"Generated JSON: {json}");
            
            var docflow = serializer.Deserialize<IDocflowWithDocuments>(json);

            docflow.Type.Should().Be(descriptionCase.DocflowType);
            DocflowShouldHaveExpectedDescription(docflow, expectedDocflow);
        }

        [TestCaseSource(nameof(TypeToDescriptionCases))]
        public void Should_deserialize_docflow_without_documents(DescriptionCase descriptionCase)
        {
            var (json, expectedDocflow) = descriptionCase.GenerateWithoutDocuments(serializer, descriptionGenerator);
            Console.WriteLine($"Generated JSON: {json}");
            
            var docflow = serializer.Deserialize<IDocflow>(json);

            docflow.Type.Should().Be(descriptionCase.DocflowType);
            DocflowShouldHaveExpectedDescription(docflow, expectedDocflow);
        }

        [Test]
        public void Should_return_null_description_if_its_known_but_missed()
        {
            var docflowType = DocflowType.Fns.Fns534Report;
            var originalDocflow = descriptionGenerator.GenerateDocflowWithoutDescription(docflowType);
            var json = serializer.SerializeToIndentedString(originalDocflow);
            Console.WriteLine($"Generated JSON: {json}");
            
            var docflow = serializer.Deserialize<Docflow>(json);

            docflow.Type.Should().Be(docflowType);
            docflow.Description.Should().BeNull();
        }

        [Test]
        public void Should_return_unknown_description_in_case_of_unknown_docflow_type()
        {
            var unknownDocflowType = new DocflowType(DocflowType.Namespace.Append("unknown-docflow"));
            var originalDocflow = descriptionGenerator.GenerateDocflowWithoutDescription(unknownDocflowType);
            originalDocflow.Description = new ReportDescription
            {
                FinalRecipient = "123"
            };
            var json = serializer.SerializeToIndentedString(originalDocflow);
            Console.WriteLine($"Generated JSON: {json}");
            
            var docflow = serializer.Deserialize<Docflow>(json);

            docflow.Type.Should().Be(unknownDocflowType);
            docflow.Description.Should().BeOfType<UnknownDescription>();
        }

        [Test]
        public void Should_return_unknown_description_in_case_of_null_docflow_type()
        {
            var dummyDocflowType = DocflowType.Fns.Fns534Report;
            var originalDocflow = descriptionGenerator.GenerateDocflowWithoutDescription(dummyDocflowType);
            originalDocflow.Type = default;
            originalDocflow.Description = new ReportDescription
            {
                FinalRecipient = "123"
            };
            var json = serializer.SerializeToIndentedString(originalDocflow);
            Console.WriteLine($"Generated JSON: {json}");
            
            var docflow = serializer.Deserialize<Docflow>(json);

            docflow.Type.Should().Be(default(DocflowType));
            docflow.Description.Should().BeOfType<UnknownDescription>();
        }

        private static void DocflowShouldHaveExpectedDescription<T>(T docflow, T expectedDocflow)
            where T : IDocflow
        {
            docflow.Should().BeEquivalentTo(expectedDocflow);
            var expectedDescription = expectedDocflow.Description;
            if (expectedDescription is not null)
            {
                docflow.Description.Should().BeOfType(expectedDescription.GetType());
                docflow.Description.Should().BeEquivalentTo(expectedDescription);
            }
        }

        public static IEnumerable<DescriptionCase> TypeToDescriptionCases
        {
            get
            {
                var allDocflows = EnumLikeType.AllEnumValuesFromNestedTypesOfStruct<DocflowType>();
                return allDocflows.Select(type =>
                {
                    var descriptionType = DocflowDescriptionTypes.TryGetDescriptionType(type);
                    if (descriptionType == null)
                        return DescriptionCase.WithoutDescriptionAtAll(type);
                    return DescriptionCase.WithDescription(descriptionType, type);
                });
            }
        }

        public record DescriptionCase
        {
            private readonly Type? descriptionType;
            private readonly Func<DocflowDescriptionGenerator, Docflow> expectedDescriptionFactory;
            
            public static DescriptionCase WithDescription(Type descriptionClass, DocflowType docflowType)
            {
                return new DescriptionCase(
                    docflowType,
                    g => g.GenerateDocflowWithDescription(descriptionClass, docflowType),
                    descriptionClass
                );
            }

            public static DescriptionCase WithoutDescriptionAtAll(DocflowType docflowType)
            {
                return new DescriptionCase(
                    docflowType,
                    g => g.GenerateDocflowWithoutDescription(docflowType),
                    null
                );
            }
            
            private DescriptionCase(DocflowType docflowType, Func<DocflowDescriptionGenerator, Docflow> expectedDescriptionFactory, Type? descriptionType)
            {
                this.expectedDescriptionFactory = expectedDescriptionFactory;
                DocflowType = docflowType;
                this.descriptionType = descriptionType;
            }

            public (string json, IDocflowWithDocuments expectedDocflow) GenerateWithDocuments(IJsonSerializer serializer, DocflowDescriptionGenerator descriptionGenerator)
            {
                var docflow = expectedDescriptionFactory(descriptionGenerator);
                var json = serializer.SerializeToIndentedString(docflow);
                return (json, docflow);
            }

            public (string json, IDocflow expectedDocflow) GenerateWithoutDocuments(IJsonSerializer serializer, DocflowDescriptionGenerator descriptionGenerator)
            {
                var docflow = expectedDescriptionFactory(descriptionGenerator);
                docflow.Documents = null!;
                var json = serializer.SerializeToIndentedString(docflow);
                return (json, docflow);
            }

            public DocflowType DocflowType { get; }

            public override string ToString() => $"{DocflowType} -> {descriptionType?.Name ?? "<null>"}";
        }
    }
}
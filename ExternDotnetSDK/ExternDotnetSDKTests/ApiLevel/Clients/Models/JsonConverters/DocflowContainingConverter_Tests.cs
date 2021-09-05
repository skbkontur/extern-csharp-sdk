#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Json;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Model.Docflows;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Docflows.Descriptions;
using Kontur.Extern.Client.Models.Docflows.Descriptions.Fns;
using Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.JsonConverters
{
    [TestFixture]
    internal class DocflowContainingConverter_Tests
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
        public void Should_deserialize_docflow_description_by_its_docflow_type(DescriptionCase descriptionCase)
        {
            var (json, expectedDocflow) = descriptionCase.Generate(serializer, descriptionGenerator);
            Console.WriteLine($"Generated JSON: {json}");
            
            var docflow = serializer.Deserialize<Docflow>(json);

            docflow.Type.Should().Be(descriptionCase.DocflowType.ToUrn());
            DocflowShouldHaveExpectedDescription(docflow, expectedDocflow);
        }

        [Test]
        public void Should_return_null_description_if_its_known_but_missed()
        {
            var unknownDocflowType = new DocflowType(DocflowType.Namespace.Append("unknown-docflow"));
            var originalDocflow = descriptionGenerator.GenerateDocflowWithoutDescription(unknownDocflowType);
            var json = serializer.SerializeToIndentedString(originalDocflow);
            Console.WriteLine($"Generated JSON: {json}");
            
            var docflow = serializer.Deserialize<Docflow>(json);

            docflow.Type.Should().Be(unknownDocflowType.ToUrn());
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

            docflow.Type.Should().Be(unknownDocflowType.ToUrn());
            docflow.Description.Should().BeOfType<UnknownDescription>();
        }

        [Test]
        public void Should_return_unknown_description_in_case_of_null_docflow_type()
        {
            var dummyDocflowType = DocflowType.Fns.Fns534.Report;
            var originalDocflow = descriptionGenerator.GenerateDocflowWithoutDescription(dummyDocflowType);
            originalDocflow.Type = null;
            originalDocflow.Description = new ReportDescription
            {
                FinalRecipient = "123"
            };
            var json = serializer.SerializeToIndentedString(originalDocflow);
            Console.WriteLine($"Generated JSON: {json}");
            
            var docflow = serializer.Deserialize<Docflow>(json);

            docflow.Type.Should().BeNull();
            docflow.Description.Should().BeOfType<UnknownDescription>();
        }

        private static void DocflowShouldHaveExpectedDescription(Docflow docflow, Docflow expectedDocflow)
        {
            docflow.Should().BeEquivalentTo(expectedDocflow);
            var expectedDescription = expectedDocflow.Description;
            if (expectedDescription is not null)
            {
                docflow.Description.Should().BeOfType(expectedDescription.GetType());
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
                DescriptionType = descriptionType;
            }

            public (string json, Docflow expectedDocflow) Generate(IJsonSerializer serializer, DocflowDescriptionGenerator descriptionGenerator)
            {
                var docflow = expectedDescriptionFactory(descriptionGenerator);
                var json = serializer.SerializeToIndentedString(docflow);
                return (json, docflow);
            }

            public DocflowType DocflowType { get; }
            public Type? DescriptionType { get; }

            public override string ToString() => $"{DocflowType} -> {DescriptionType?.Name ?? "<null>"}";
        }
    }
}
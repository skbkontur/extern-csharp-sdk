#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Json;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Model.Docflows;
using Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.JsonConverters
{
    [TestFixture]
    internal class DocflowJsonSerialization_Tests
    {
        private static IJsonSerializer serializer = null!;
        private DocflowDescriptionGenerator descriptionGenerator = null!;
            
        [SetUp]
        public void SetUp()
        {
            serializer = new JsonSerializerFactory().CreateApiJsonSerializer();
            descriptionGenerator = new DocflowDescriptionGenerator();
        }

        [TestCaseSource(nameof(TypeToDescriptionCases))]
        public void Should_deserialize_docflow_description_by_it_docflow_type(DescriptionCase descriptionCase)
        {
            var (json, expectedDocflow) = descriptionCase.Generate(serializer, descriptionGenerator);
            
            var docflow = serializer.DeserializeFromJson<Docflow>(json);

            docflow.Type.Should().Be(descriptionCase.DocflowType.ToUrn());
            DocflowShouldHaveExpectedDescription(docflow, expectedDocflow);
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
                    var description = DocflowDescriptionsFactory.TryCreateEmptyDescription(type);
                    if (description == null)
                        return DescriptionCase.WithoutDescriptionAtAll(type);
                    return DescriptionCase.WithDescription(description.GetType(), type);
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
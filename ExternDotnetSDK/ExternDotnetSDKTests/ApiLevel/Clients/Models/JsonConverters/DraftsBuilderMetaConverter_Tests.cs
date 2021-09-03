#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Json;
using Kontur.Extern.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Model.DraftBuilders;
using Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.JsonConverters
{
    [TestFixture]
    internal class DraftsBuilderMetaConverter_Tests
    {
        private static IJsonSerializer serializer = null!;
        private DraftsBuilderMetaGenerator builderMetaGenerator = null!;
            
        [SetUp]
        public void SetUp()
        {
            serializer = JsonSerializerFactory.CreateJsonSerializer(ignoreNullValues: false);
            builderMetaGenerator = new DraftsBuilderMetaGenerator();
        }
        
        [TestCaseSource(nameof(BuilderTypeToBuilderDataCases))]
        public void Should_deserialize_builder_meta_with_data_by_its_builder_type(BuilderDataCase builderDataCase)
        {
            var (json, expectedBuilderMeta) = builderDataCase.GenerateDescription(serializer, builderMetaGenerator);
            Console.WriteLine($"Generated JSON: {json}");
            
            var builderMeta = serializer.Deserialize<DraftsBuilderMeta>(json);

            builderMeta.BuilderType.Should().Be(builderDataCase.BuilderType.ToUrn());
            DraftsBuilderMetaShouldBeEqual(builderMeta, expectedBuilderMeta);
        }

        [Test]
        public void Should_deserialize_unknown_data_in_case_of_unknown_builder_type()
        {
            var unknownBuilderType = new DraftBuilderType(DraftBuilderType.Namespace.Append("unknown-document"));
            var originalBuilderMeta = builderMetaGenerator.GenerateWithoutData(unknownBuilderType);
            originalBuilderMeta.BuilderData = new FnsInventoryDraftsBuilderData
            {
                IdFileOsn = "123"
            };
            var json = serializer.SerializeToIndentedString(originalBuilderMeta);
            Console.WriteLine($"Generated JSON: {json}");
        
            var builderMeta = serializer.Deserialize<DraftsBuilderMeta>(json);
        
            builderMeta.BuilderType.Should().Be(unknownBuilderType.ToUrn());
            builderMeta.BuilderData.Should().BeOfType<UnknownDraftsBuilderData>();
            builderMeta.Should().BeEquivalentTo(originalBuilderMeta, c => c.Excluding(x => x.BuilderData));
        }
        
        [Test]
        public void Should_deserialize_unknown_data_in_case_of_null_builder_type()
        {
            var dummyKnownBuilderType = DraftBuilderType.Fns.BusinessRegistration.Registration;
            var originalBuilderMeta = builderMetaGenerator.GenerateWithData(dummyKnownBuilderType);
            originalBuilderMeta.BuilderType = null;
            var json = serializer.SerializeToIndentedString(originalBuilderMeta);
            Console.WriteLine($"Generated JSON: {json}");
        
            var builderMeta = serializer.Deserialize<DraftsBuilderMeta>(json);
        
            builderMeta.BuilderType.Should().BeNull();
            builderMeta.BuilderData.Should().BeOfType<UnknownDraftsBuilderData>();
            builderMeta.Should().BeEquivalentTo(originalBuilderMeta, c => c.Excluding(x => x.BuilderData));
        }
        
        [TestCaseSource(nameof(BuilderTypesWithoutDataCases))]
        public void Should_deserialize_empty_data_in_case_of_builder_type_have_no_data(BuilderDataCase builderDataCase)
        {
            var (json, _) = builderDataCase.GenerateDescription(serializer, builderMetaGenerator);
            Console.WriteLine($"Generated JSON: {json}");
            
            var builderMeta = serializer.Deserialize<DraftsBuilderMeta>(json);

            builderMeta.BuilderData.Should().BeOfType<UnknownDraftsBuilderData>();
        }

        private static void DraftsBuilderMetaShouldBeEqual(DraftsBuilderMeta description, DraftsBuilderMeta expectedDescription)
        {
            description.Should().BeEquivalentTo(expectedDescription);
            description.BuilderData.Should().BeOfType(expectedDescription.BuilderData.GetType());
        }

        public static IEnumerable<BuilderDataCase> BuilderTypesWithoutDataCases =>
            BuilderTypeToBuilderDataCases.Where(x => x.BuilderDataType == null);

        public static IEnumerable<BuilderDataCase> BuilderTypeToBuilderDataCases =>
            EnumLikeType
                .AllEnumValuesFromNestedTypesOfStruct<DraftBuilderType>()
                .Select(bt => new BuilderDataCase(bt, DraftBuilderDataTypes.TryGetBuilderDataType(bt)));
        
        public record BuilderDataCase
        {
            public BuilderDataCase(DraftBuilderType builderType, Type? builderDataType)
            {
                BuilderType = builderType;
                BuilderDataType = builderDataType;
            }

            public (string json, DraftsBuilderMeta expectedMeta) GenerateDescription(IJsonSerializer serializer, DraftsBuilderMetaGenerator descriptionGenerator)
            {
                var builderMeta = descriptionGenerator.GenerateWithData(BuilderDataType, BuilderType);
                var json = serializer.SerializeToIndentedString(builderMeta);
                return (json, builderMeta);
            }

            public DraftBuilderType BuilderType { get; }
            public Type? BuilderDataType { get; }

            public override string ToString() => $"{BuilderType} -> {BuilderDataType?.Name ?? "<null>"}";
        }
    }
}
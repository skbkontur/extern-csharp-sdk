using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.DraftBuilders
{
    [TestFixture]
    internal class DraftsBuilderDocumentFileMetaConverter_Tests
    {
        private DraftsBuilderMetasSerializationTester<DraftsBuilderDocumentFileMeta, DraftsBuilderDocumentFileData> tester = null!;

        [SetUp]
        public void SetUp() => 
            tester = new(() => new UnknownDraftsBuilderDocumentFileData());

        [TestCaseSource(nameof(BuilderTypeToBuilderDataCases))]
        public void Should_deserialize_builder_meta_with_data_by_its_builder_type((DraftBuilderType builderType, Type? builderDataType) builderDataCase)
        {
            var (builderType, builderDataType) = builderDataCase;
            var (json, expectedBuilderMeta) = tester.GenerateWithData(builderDataType, builderType);
            
            var builderMeta = tester.Deserialize(json);

            builderMeta.BuilderType.Should().Be(builderType);
            DraftsBuilderDocumentMetaShouldBeEqual(builderMeta, expectedBuilderMeta);
        }

        [Test]
        public void Should_deserialize_unknown_data_in_case_of_unknown_builder_type()
        {
            var (json, originalBuilderMeta) = tester.GenerateWithUnknownTypeAndDataOf<FnsInventoryDraftsBuilderDocumentFileData>();
        
            var builderMeta = tester.Deserialize(json);
        
            builderMeta.BuilderType.Should().Be(originalBuilderMeta.BuilderType);
            builderMeta.BuilderData.Should().BeOfType<UnknownDraftsBuilderDocumentFileData>();
            builderMeta.Should().BeEquivalentTo(originalBuilderMeta, c => c.Excluding(x => x.BuilderData));
        }
        
        [Test]
        public void Should_deserialize_unknown_data_in_case_of_empty_builder_type()
        {
            var (json, originalBuilderMeta) = tester.GenerateWithoutBuilderType(null);
        
            var builderMeta = tester.Deserialize(json);
        
            builderMeta.BuilderType.Should().Be(new DraftBuilderType());
            builderMeta.BuilderData.Should().BeOfType<UnknownDraftsBuilderDocumentFileData>();
            builderMeta.Should().BeEquivalentTo(originalBuilderMeta, c => c.Excluding(x => x.BuilderData));
        }
        
        [TestCaseSource(nameof(BuilderTypesWithoutData))]
        public void Should_deserialize_empty_data_in_case_of_builder_type_have_no_data((DraftBuilderType builderType, Type? builderDataType) builderDataCase)
        {
            var (builderType, builderDataType) = builderDataCase;
            var (json, _) = tester.GenerateWithData(builderDataType, builderType);
            
            var builderMeta = tester.Deserialize(json);

            builderMeta.BuilderData.Should().BeOfType<UnknownDraftsBuilderDocumentFileData>();
        }

        private static void DraftsBuilderDocumentMetaShouldBeEqual(DraftsBuilderDocumentFileMeta meta, DraftsBuilderDocumentFileMeta expectedMeta)
        {
            meta.Should().BeEquivalentTo(expectedMeta);
            meta.BuilderData.Should().BeOfType(expectedMeta.BuilderData.GetType());
        }

        public static IEnumerable<(DraftBuilderType builderType, Type? builderDataType)> BuilderTypesWithoutData =>
            BuilderTypeToBuilderDataCases.Where(x => x.builderDataType == null);

        public static IEnumerable<(DraftBuilderType builderType, Type? builderDataType)> BuilderTypeToBuilderDataCases =>
            EnumLikeType
                .AllEnumValuesFromNestedTypesOfStruct<DraftBuilderType>()
                .Select(bt => (bt, DraftBuilderMetasDataTypes.TryGetBuilderDocumentFileDataType(bt)));
    }
}
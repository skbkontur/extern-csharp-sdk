using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data.FnsInventory;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.DraftsBuilders
{
    [TestFixture]
    internal class DraftsBuilderDocumentMetaConverter_Tests
    {
        private DraftsBuilderMetasSerializationTester<DraftsBuilderDocumentMeta, DraftsBuilderDocumentData> tester = null!;

        [SetUp]
        public void SetUp() => 
            tester = new(() => new UnknownBuilderDocumentData());

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
            var (json, originalBuilderMeta) = tester.GenerateWithUnknownTypeAndDataOf<FnsInventoryDraftsBuilderDocumentData>();
        
            var builderMeta = tester.Deserialize(json);
        
            builderMeta.BuilderType.Should().Be(originalBuilderMeta.BuilderType);
            builderMeta.BuilderData.Should().BeOfType<UnknownBuilderDocumentData>();
            builderMeta.Should().BeEquivalentTo(originalBuilderMeta, c => c.Excluding(x => x.BuilderData));
        }
        
        [TestCaseSource(nameof(BuilderTypesWithoutData))]
        public void Should_deserialize_empty_data_in_case_of_builder_type_have_no_data((DraftBuilderType builderType, Type? builderDataType) builderDataCase)
        {
            var (builderType, builderDataType) = builderDataCase;
            var (json, _) = tester.GenerateWithData(builderDataType, builderType);
            
            var builderMeta = tester.Deserialize(json);

            builderMeta.BuilderData.Should().BeOfType<UnknownBuilderDocumentData>();
        }

        private static void DraftsBuilderDocumentMetaShouldBeEqual(DraftsBuilderDocumentMeta meta, DraftsBuilderDocumentMeta expectedMeta)
        {
            meta.Should().BeEquivalentTo(expectedMeta);
            if (expectedMeta.BuilderData is null)
            {
                meta.BuilderData.Should().BeNull();
            }
            else
            {
                meta.BuilderData.Should().BeOfType(expectedMeta.BuilderData.GetType());
            }
        }

        public static IEnumerable<(DraftBuilderType builderType, Type? builderDataType)> BuilderTypesWithoutData =>
            BuilderTypeToBuilderDataCases.Where(x => x.builderDataType == null);

        public static IEnumerable<(DraftBuilderType builderType, Type? builderDataType)> BuilderTypeToBuilderDataCases =>
            EnumLikeType
                .AllEnumValuesFromNestedTypesOfStruct<DraftBuilderType>()
                .Select(bt => (bt, DraftBuilderMetasDataTypes.TryGetBuilderDocumentDataType(bt)));
    }
}
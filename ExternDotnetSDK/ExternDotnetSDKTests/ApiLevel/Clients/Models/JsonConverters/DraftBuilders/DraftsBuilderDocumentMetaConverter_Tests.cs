#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Client.Model.DraftBuilders;
using Kontur.Extern.Client.Models.DraftsBuilders.Documents;
using Kontur.Extern.Client.Models.DraftsBuilders.Documents.Data;
using Kontur.Extern.Client.Models.DraftsBuilders.Documents.Data.FnsInventory;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.JsonConverters.DraftBuilders
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

            builderMeta.BuilderType.Should().Be(builderType.ToUrn());
            DraftsBuilderDocumentMetaShouldBeEqual(builderMeta, expectedBuilderMeta);
        }

        [Test]
        public void Should_deserialize_unknown_data_in_case_of_unknown_builder_type()
        {
            var (json, originalBuilderMeta) = tester.GenerateUnknownBuilder(new FnsInventoryDraftsBuilderDocumentData
            {
                ClaimItemNumber = "123"
            });
        
            var builderMeta = tester.Deserialize(json);
        
            builderMeta.BuilderType.Should().Be(originalBuilderMeta.BuilderType);
            builderMeta.BuilderData.Should().BeOfType<UnknownBuilderDocumentData>();
            builderMeta.Should().BeEquivalentTo(originalBuilderMeta, c => c.Excluding(x => x.BuilderData));
        }
        
        [Test]
        public void Should_deserialize_unknown_data_in_case_of_null_builder_type()
        {
            var (json, originalBuilderMeta) = tester.GenerateWithoutBuilderType(null);
        
            var builderMeta = tester.Deserialize(json);
        
            builderMeta.BuilderType.Should().BeNull();
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
            meta.BuilderData.Should().BeOfType(expectedMeta.BuilderData.GetType());
        }

        public static IEnumerable<(DraftBuilderType builderType, Type? builderDataType)> BuilderTypesWithoutData =>
            BuilderTypeToBuilderDataCases.Where(x => x.builderDataType == null);

        public static IEnumerable<(DraftBuilderType builderType, Type? builderDataType)> BuilderTypeToBuilderDataCases =>
            EnumLikeType
                .AllEnumValuesFromNestedTypesOfStruct<DraftBuilderType>()
                .Select(bt => (bt, DraftBuilderMetasDataTypes.TryGetBuilderDocumentDataType(bt)));
    }
}
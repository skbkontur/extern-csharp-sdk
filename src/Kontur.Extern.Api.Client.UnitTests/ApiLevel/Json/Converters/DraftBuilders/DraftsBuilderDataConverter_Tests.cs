using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoBogus;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBulders.Builders;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.AutoFaker;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.DraftBuilders;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers.BogusExtensions;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.DraftBuilders
{
    [TestFixture]
    internal class DraftsBuilderMetaRequestSerialization_Tests
    {
        private IJsonSerializer serializer = null!;
        private IAutoFaker autoFaker = null!;

        [SetUp]
        public void SetUp()
        {
            serializer = JsonSerializerFactory.CreateJsonSerializer(ignoreNullValues: false);
            autoFaker = new AutoFakerFactory().AddDraftsBuilderEntitiesGeneration().Create();
        }

        [TestCaseSource(nameof(BuilderTypeToDataType))]
        public void Should_serialize_request_with_given_data_type((DraftBuilderType builderType, Type? dataType) theCase)
        {
            var (builderType, dataType) = theCase;
            var request = GenerateDraftsBuilderMetaRequest(builderType, dataType);

            var json = serializer.SerializeToIndentedString(request);
            Console.WriteLine($"Serialized JSON: {json}");

            ShouldHaveExpectedBuilderTypeAndData(json, builderType, dataType);
        }

        private static void ShouldHaveExpectedBuilderTypeAndData(string json, DraftBuilderType builderType, Type? dataType)
        {
            using var jsonDocument = JsonDocument.Parse(json);

            var serializedBuilderType = jsonDocument.RootElement.GetProperty("builder-type").GetString();
            serializedBuilderType.Should().Be(builderType.ToString());

            var builderDataElement = jsonDocument.RootElement.GetProperty("builder-data");
            if (dataType is null)
            {
                builderDataElement.GetString().Should().BeNull();
            }
            else
            {
                var dataValues = builderDataElement.EnumerateObject();
                if (dataType != typeof(PfrReportDraftsBuilderData))
                {
                    dataValues.Should().NotBeEmpty();
                    dataValues.Should().Contain(x => x.Value.GetRawText() != "null");
                }
            }
        }

        private DraftsBuilderMetaRequest GenerateDraftsBuilderMetaRequest(DraftBuilderType builderType, Type? dataType)
        {
            var payerInn = autoFaker.Generate<Inn>();
            var senderInn = autoFaker.Generate<Inn>();
            var senderCert = autoFaker.Generate<byte[]>();
            var ifnsCode = autoFaker.Generate<IfnsCode>();
            var mriCode = autoFaker.Generate<MriCode>();

            var request = new DraftsBuilderMetaRequest(
                new SenderRequest
                {
                    Inn = senderInn.ToString(),
                    Certificate = new CertificateRequest
                    {
                        PublicKey = senderCert
                    }
                },
                new AccountInfoRequest
                {
                    Inn = payerInn.ToString()
                },
                new RecipientInfoRequest
                {
                    IfnsCode = ifnsCode,
                    MriCode = mriCode
                },
                builderType,
                (DraftsBuilderData?) autoFaker.Generate(dataType)
            );
            return request;
        }

        private static IEnumerable<(DraftBuilderType builderType, Type? dataType)> BuilderTypeToDataType
        {
            get
            {
                return EnumLikeType.AllEnumValuesFromNestedTypesOfStruct<DraftBuilderType>()
                    .Select(x => (x, DraftBuilderMetasDataTypes.TryGetBuildersDataType(x)));
            }
        }
    }
}
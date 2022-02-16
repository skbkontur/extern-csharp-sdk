using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.Builders;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Testing.Assertions;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.AutoFaker;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.DraftsBuilders;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers.BogusExtensions;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.ApiLevel.Models.Requests.DraftBuilders.Builders
{
    [TestFixture]
    internal class DraftsBuilderMetaRequest_Tests
    {
        [TestCaseSource(nameof(BuilderTypeToDataType))]
        public void Should_initialize_request_with_given_properties((DraftBuilderType builderType, Type? dataType) theCase)
        {
            var autoFaker = new AutoFakerFactory()
                .AddDraftsBuilderEntitiesGeneration()
                .Create();
            
            var payerInn = autoFaker.Generate<Inn>();
            var senderInn = autoFaker.Generate<Inn>();
            var senderCert = autoFaker.Generate<byte[]>();
            var ifnsCode = autoFaker.Generate<IfnsCode>();
            var mriCode = autoFaker.Generate<MriCode>();

            var (builderType, dataType) = theCase;
            var request = new DraftsBuilderMetaRequest(
                new SenderRequest
                {
                    Inn = senderInn.ToString(),
                    Certificate = new PublicKeyCertificateRequest
                    {
                        Content = senderCert
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

            request.Sender.Inn.Should().Be(senderInn.ToString());
            request.Payer.Inn.Should().Be(payerInn.ToString());
            request.Recipient.IfnsCode.Should().Be(ifnsCode);
            request.Recipient.MriCode.Should().Be(mriCode);
            request.BuilderType.Should().Be(builderType);
            if (dataType == null)
            {
                request.BuilderData.Should().BeNull();
            }
            else
            {
                request.BuilderData.Should().BeOfType(dataType);
                request.BuilderData.Should().HasInitializedAtLeastOneProperty();
            }
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
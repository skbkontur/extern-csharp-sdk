using AutoBogus;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.Builders;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Model.DraftBuilders;
using Kontur.Extern.Api.Client.Model.Drafts;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.AutoFaker;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.DraftsBuilders;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.DraftBuilders
{
    [TestFixture]
    internal class DraftsBuilderMetadata_Tests
    {
        private DraftPayer payer = null!;
        private DraftSender sender = null!;
        private DraftRecipient recipient = null!;
        private DraftsBuilderMetaRequest expectedRequestPrototype = null!;
        private IAutoFaker autoFaker = null!;

        [SetUp]
        public void SetUp()
        {
            autoFaker = new AutoFakerFactory()
                .AddDraftsBuilderEntitiesGeneration()
                .Create();
            
            var payerInn = autoFaker.Generate<Inn>();
            var senderInn = autoFaker.Generate<Inn>();
            var senderCert = autoFaker.Generate<byte[]>();
            var ifnsCode = autoFaker.Generate<IfnsCode>();
            var mriCode = autoFaker.Generate<MriCode>();

            payer = DraftPayer.IndividualEntrepreneur(payerInn);
            sender = DraftSender.IndividualEntrepreneur(senderInn, senderCert);
            recipient = DraftRecipient.Ifns(ifnsCode, mriCode);

            expectedRequestPrototype = new DraftsBuilderMetaRequest(
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
                default,
                null
            );
        }
        
        [Test]
        public void BusinessRegistrationDraftsBuilder_should_initialise_with_given_parameters()
        {
            var data = autoFaker.Generate<BusinessRegistrationDraftsBuilderData>();
            var expectedRequest = expectedRequestPrototype.ChangeBuilderType(DraftBuilderType.Fns.BusinessRegistration.Registration, data);
            
            var request = DraftsBuilderMetadata
                .BusinessRegistrationDraftsBuilder(payer, sender, recipient, data)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public void FnsInventoryDraftsBuilder_should_initialise_with_given_parameters()
        {
            var data = autoFaker.Generate<FnsInventoryDraftsBuilderData>();
            var expectedRequest = expectedRequestPrototype.ChangeBuilderType(DraftBuilderType.Fns.Fns534.Inventory, data);

            var request = DraftsBuilderMetadata
                .FnsInventoryDraftsBuilder(payer, sender, recipient, data)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void FnsLetterDraftsBuilder_should_initialise_with_given_parameters()
        {
            var expectedRequest = expectedRequestPrototype.ChangeBuilderType(DraftBuilderType.Fns.Fns534.Letter, new UnknownDraftsBuilderData());

            var request = DraftsBuilderMetadata
                .FnsLetterDraftsBuilder(payer, sender, recipient)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void PfrIosDraftsBuilder_should_initialise_with_given_parameters()
        {
            var expectedRequest = expectedRequestPrototype.ChangeBuilderType(DraftBuilderType.Pfr.Ios, new UnknownDraftsBuilderData());

            var request = DraftsBuilderMetadata
                .PfrIosDraftsBuilder(payer, sender, recipient)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void PfrLetterDraftsBuilder_should_initialise_with_given_parameters()
        {
            var expectedRequest = expectedRequestPrototype.ChangeBuilderType(DraftBuilderType.Pfr.Letter, new UnknownDraftsBuilderData());

            var request = DraftsBuilderMetadata
                .PfrLetterDraftsBuilder(payer, sender, recipient)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void PfrReportDraftsBuilder_should_initialise_with_given_parameters()
        {
            var data = new PfrReportDraftsBuilderData();
            var expectedRequest = expectedRequestPrototype.ChangeBuilderType(DraftBuilderType.Pfr.Report, data);

            var request = DraftsBuilderMetadata
                .PfrReportDraftsBuilder(payer, sender, recipient, data)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void RosstatLetterDraftsBuilder_should_initialise_with_given_parameters()
        {
            var expectedRequest = expectedRequestPrototype.ChangeBuilderType(DraftBuilderType.Rosstat.Letter, new UnknownDraftsBuilderData());

            var request = DraftsBuilderMetadata
                .RosstatLetterDraftsBuilder(payer, sender, recipient)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
    }
}
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBulders.Builders;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Model.DraftBuilders;
using Kontur.Extern.Api.Client.Model.Drafts;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Testing.Generators;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.DraftBuilders
{
    [TestFixture]
    internal class DraftsBuilderMetadata_Tests
    {
        private readonly Randomizer randomizer = new();
        private readonly AuthoritiesCodesGenerator codesGenerator = new();
        private DraftPayer payer = null!;
        private DraftSender sender = null!;
        private DraftRecipient recipient = null!;
        private DraftsBuilderMetaRequest expectedRequest = null!;

        [SetUp]
        public void SetUp()
        {
            var payerInn = codesGenerator.PersonInn();
            var senderInn = codesGenerator.PersonInn();
            var senderCert = randomizer.Bytes(10);

            payer = DraftPayer.IndividualEntrepreneur(payerInn);
            sender = DraftSender.IndividualEntrepreneur(senderInn, senderCert);
            recipient = DraftRecipient.Ifns(IfnsCode.Parse("1234"), MriCode.Parse("5678"));
            
            expectedRequest = new DraftsBuilderMetaRequest
            {
                Payer = new AccountInfoRequest
                {
                    Inn = payerInn.ToString()
                },
                Sender = new SenderRequest
                {
                    Inn = senderInn.ToString(),
                    Certificate = new CertificateRequest
                    {
                        PublicKey = senderCert
                    }
                },
                Recipient = new RecipientInfoRequest
                {
                    IfnsCode = IfnsCode.Parse("1234"),
                    MriCode = MriCode.Parse("5678")
                }
            };
        }
        
        [Test]
        public void BusinessRegistrationDraftsBuilder_should_initialise_with_given_parameters()
        {
            var data = new BusinessRegistrationDraftsBuilderData();
            expectedRequest.BuilderData = data;
            expectedRequest.BuilderType = DraftBuilderType.Fns.BusinessRegistration.Registration;
            
            var request = DraftsBuilderMetadata
                .BusinessRegistrationDraftsBuilder(payer, sender, recipient, data)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void BusinessRegistrationDraftsBuilderLegacy_should_initialise_with_given_parameters()
        {
            var data = new BusinessRegistrationDraftsBuilderData();
            expectedRequest.BuilderData = data;
            expectedRequest.BuilderType = DraftBuilderType.Fns.BusinessRegistration.RegistrationLegacy;

            var request = DraftsBuilderMetadata
                .BusinessRegistrationDraftsBuilderLegacy(payer, sender, recipient, data)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void FnsInventoryDraftsBuilder_should_initialise_with_given_parameters()
        {
            var data = new FnsInventoryDraftsBuilderData();
            expectedRequest.BuilderData = data;
            expectedRequest.BuilderType = DraftBuilderType.Fns.Fns534.Inventory;

            var request = DraftsBuilderMetadata
                .FnsInventoryDraftsBuilder(payer, sender, recipient, data)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void FnsLetterDraftsBuilder_should_initialise_with_given_parameters()
        {
            expectedRequest.BuilderData = new UnknownDraftsBuilderData();
            expectedRequest.BuilderType = DraftBuilderType.Fns.Fns534.Letter;

            var request = DraftsBuilderMetadata
                .FnsLetterDraftsBuilder(payer, sender, recipient)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void PfrIosDraftsBuilder_should_initialise_with_given_parameters()
        {
            expectedRequest.BuilderData = new UnknownDraftsBuilderData();
            expectedRequest.BuilderType = DraftBuilderType.Pfr.Ios;

            var request = DraftsBuilderMetadata
                .PfrIosDraftsBuilder(payer, sender, recipient)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void PfrLetterDraftsBuilder_should_initialise_with_given_parameters()
        {
            expectedRequest.BuilderData = new UnknownDraftsBuilderData();
            expectedRequest.BuilderType = DraftBuilderType.Pfr.Letter;

            var request = DraftsBuilderMetadata
                .PfrLetterDraftsBuilder(payer, sender, recipient)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void PfrReportDraftsBuilder_should_initialise_with_given_parameters()
        {
            var data = new PfrReportDraftsBuilderData();
            expectedRequest.BuilderData = data;
            expectedRequest.BuilderType = DraftBuilderType.Pfr.Report;

            var request = DraftsBuilderMetadata
                .PfrReportDraftsBuilder(payer, sender, recipient, data)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void RosstatLetterDraftsBuilder_should_initialise_with_given_parameters()
        {
            expectedRequest.BuilderData = new UnknownDraftsBuilderData();
            expectedRequest.BuilderType = DraftBuilderType.Rosstat.Letter;

            var request = DraftsBuilderMetadata
                .RosstatLetterDraftsBuilder(payer, sender, recipient)
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
    }
}
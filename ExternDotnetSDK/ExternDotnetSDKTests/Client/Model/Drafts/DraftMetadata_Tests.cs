#nullable enable
using System;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Client.Model.Drafts;
using Kontur.Extern.Client.Models.Numbers;
using Kontur.Extern.Client.Testing.Generators;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Model.Drafts
{
    [TestFixture]
    internal class DraftMetadata_Tests
    {
        private readonly Randomizer randomizer = new();
        private readonly AuthoritiesCodesGenerator codesGenerator = new();
        
        [Test]
        public void Should_initialise_with_required_parameters()
        {
            var payerInn = codesGenerator.PersonInn();
            var senderInn = codesGenerator.PersonInn();
            var senderCert = randomizer.Bytes(10);
            var expectedRequest = new DraftMetaRequest
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
                    IfnsCode = "1234",
                    MriCode = "5678"
                }
            };

            var request = new DraftMetadata(
                    DraftPayer.IndividualEntrepreneur(payerInn),
                    DraftSender.IndividualEntrepreneur(senderInn, senderCert),
                    DraftRecipient.Ifns(IfnsCode.Parse("1234"), MriCode.Parse("5678"))
                )
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [Test]
        public void Should_fail_when_given_null_payer()
        {
            var sender = RandomSender();
            var recipient = RandomRecipient();

            Action action = () => _ = new DraftMetadata(null!, sender, recipient);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Should_fail_when_given_null_sender()
        {
            var payer = RandomPayer();
            var recipient = RandomRecipient();

            Action action = () => _ = new DraftMetadata(payer, null!, recipient);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Should_fail_when_given_null_recipient()
        {
            var payer = RandomPayer();
            var sender = RandomSender();
            
            Action action = () => _ = new DraftMetadata(payer, sender, null!);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void WithSubject_should_set_subject()
        {
            var (newDraft, expectedRequest) = RandomDraft();
            expectedRequest.AdditionalInfo = new AdditionalInfoRequest
            {
                Subject = "the subject"
            };
            
            var request = newDraft.WithSubject("the subject").ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
        
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void WithSubject_should_fail_when_given_an_invalid_subject(string subject)
        {
            var (newDraft, _) = RandomDraft();

            Action action = () => newDraft.WithSubject(subject);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void WithAdditionalCertificates_should_set_additional_certificates()
        {
            var certificates = new []
            {
                randomizer.DigitsString(3),
                randomizer.DigitsString(3)
            };
            var (newDraft, expectedRequest) = RandomDraft();
            expectedRequest.AdditionalInfo = new AdditionalInfoRequest
            {
                AdditionalCertificates = certificates
            };
            
            var request = newDraft.WithAdditionalCertificates(certificates).ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public void WithAdditionalCertificates_should_fail_when_given_certificates_is_null()
        {
            var (newDraft, _) = RandomDraft();
            
            Action action = () => newDraft.WithAdditionalCertificates(null!);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WithAdditionalCertificates_should_fail_when_given_empty_certificates_array()
        {
            var (newDraft, _) = RandomDraft();
            
            Action action = () => newDraft.WithAdditionalCertificates(new string[0]);

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void WithAdditionalCertificates_should_fail_when_a_given_certificate_is_invalid(string invalidCert)
        {
            var validCert = randomizer.DigitsString(4);
            var (newDraft, _) = RandomDraft();

            Action action = () => newDraft.WithAdditionalCertificates(new[] {validCert, invalidCert});

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WithAdditionalCertificates_should_set_certificates_in_combination_with_a_subject()
        {
            var certificates = new []
            {
                randomizer.DigitsString(3),
                randomizer.DigitsString(3)
            };
            var (newDraft, expectedRequest) = RandomDraft();
            expectedRequest.AdditionalInfo = new AdditionalInfoRequest
            {
                Subject = "the subject",
                AdditionalCertificates = certificates
            };

            var request = newDraft.WithSubject("the subject").WithAdditionalCertificates(certificates).ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public void WithRelatedDocument_should_set_related_document()
        {
            var (newDraft, expectedRequest) = RandomDraft();
            var documentId = Guid.NewGuid();
            var docflowId = Guid.NewGuid();
            expectedRequest.RelatedDocument = new RelatedDocumentRequest
            {
                RelatedDocflowId = docflowId,
                RelatedDocumentId = documentId
            };

            var request = newDraft.WithRelatedDocument(docflowId, documentId).ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }

        private (DraftMetadata newDraft, DraftMetaRequest expectedRequest) RandomDraft()
        {
            var payerInn = codesGenerator.PersonInn();
            var senderInn = codesGenerator.PersonInn();
            var senderCert = randomizer.Bytes(10);
            var expectedRequest = new DraftMetaRequest
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
                    IfnsCode = "1234"
                }
            };

            var newDraft = new DraftMetadata(
                DraftPayer.IndividualEntrepreneur(payerInn),
                DraftSender.IndividualEntrepreneur(senderInn, senderCert),
                DraftRecipient.Ifns(IfnsCode.Parse("1234"))
            );
            return (newDraft, expectedRequest);
        }

        private DraftRecipient RandomRecipient() => DraftRecipient.Ifns(codesGenerator.IfnsCode());

        private DraftSender RandomSender() => DraftSender.IndividualEntrepreneur(codesGenerator.PersonInn(), randomizer.Bytes(10));

        private DraftPayer RandomPayer() => DraftPayer.IndividualEntrepreneur(codesGenerator.PersonInn());
    }
}
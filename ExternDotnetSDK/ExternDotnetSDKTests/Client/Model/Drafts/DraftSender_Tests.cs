using System;
using System.Net;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Client.Model;
using Kontur.Extern.Client.Model.Drafts;
using Kontur.Extern.Client.Testing.Generators;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Client.Model.Drafts
{
    public class DraftSender_Tests
    {
        private readonly Randomizer randomizer = new();
        private readonly AuthoritiesCodesGenerator codesGenerator = new();

        [Test]
        public void IndividualEntrepreneur_should_fail_when_given_certificate_is_null()
        {
            var inn = codesGenerator.PersonInn();
            
            Action action = () => DraftSender.IndividualEntrepreneur(inn, null!);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void IndividualEntrepreneur_should_fail_when_given_inn_is_null()
        {
            CertificateContent certificate = randomizer.Bytes(10);
            
            Action action = () => DraftSender.IndividualEntrepreneur(null!, certificate);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void IndividualEntrepreneur_should_create_a_sender_with_person_inn()
        {
            var certificate = randomizer.Bytes(10);
            var inn = codesGenerator.PersonInn();
            var expectedRequest = new SenderRequest
            {
                Certificate = new CertificateRequest
                {
                    PublicKey = certificate
                },
                Inn = inn.ToString()
            };

            var request = DraftSender.IndividualEntrepreneur(inn, certificate).ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public void LegalEntity_should_fail_when_given_certificate_is_null()
        {
            var inn = codesGenerator.LegalEntityInn();
            var kpp = codesGenerator.Kpp();
            
            Action action = () => DraftSender.LegalEntity(inn, kpp, null!);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void LegalEntity_should_fail_when_given_inn_is_null()
        {
            var kpp = codesGenerator.Kpp();
            CertificateContent certificate = randomizer.Bytes(10);
            
            Action action = () => DraftSender.LegalEntity(null!, kpp, certificate);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void LegalEntity_should_fail_when_given_kpp_is_null()
        {
            var inn = codesGenerator.LegalEntityInn();
            CertificateContent certificate = randomizer.Bytes(10);
            
            Action action = () => DraftSender.LegalEntity(inn, null!, certificate);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void LegalEntity_should_create_a_sender_with_person_inn()
        {
            var certificate = randomizer.Bytes(10);
            var inn = codesGenerator.LegalEntityInn();
            var kpp = codesGenerator.Kpp();
            var expectedRequest = new SenderRequest
            {
                Certificate = new CertificateRequest
                {
                    PublicKey = certificate
                },
                Inn = inn.ToString(),
                Kpp = kpp.ToString()
            };

            var request = DraftSender.LegalEntity(inn, kpp, certificate).ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public void WithIpAddress_should_set_ip_address()
        {
            var certificate = randomizer.Bytes(10);
            var inn = codesGenerator.LegalEntityInn();
            var kpp = codesGenerator.Kpp();
            var expectedRequest = new SenderRequest
            {
                Certificate = new CertificateRequest
                {
                    PublicKey = certificate
                },
                Inn = inn.ToString(),
                Kpp = kpp.ToString(),
                IpAddress = "127.0.0.1"
            };

            var request = DraftSender
                .LegalEntity(inn, kpp, certificate)
                .WithIpAddress(IPAddress.Parse("127.0.0.1"))
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public void WithIpAddress_should_fail_if_given_ip_address_is_null()
        {
            var certificate = randomizer.Bytes(10);
            var inn = codesGenerator.LegalEntityInn();
            var kpp = codesGenerator.Kpp();

            var sender = DraftSender
                .LegalEntity(inn, kpp, certificate);

            Action action = () => sender.WithIpAddress(null!);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Representative_should_set_representative_flag()
        {
            var certificate = randomizer.Bytes(10);
            var inn = codesGenerator.PersonInn();
            var expectedRequest = new SenderRequest
            {
                Certificate = new CertificateRequest
                {
                    PublicKey = certificate
                },
                Inn = inn.ToString(),
                IsRepresentative = true
            };

            var request = DraftSender.IndividualEntrepreneur(inn, certificate)
                .Representative()
                .ToRequest();

            request.Should().BeEquivalentTo(expectedRequest);
        }
    }
}
using System;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Model.Drafts;
using Kontur.Extern.Api.Client.Testing.Generators;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.Drafts
{
    public class DraftPayer_Tests
    {
        private readonly AuthoritiesCodesGenerator codesGenerator = new();

        [Test]
        public void IndividualEntrepreneur_should_fail_when_given_null_inn()
        {
            Action action = () => DraftPayer.IndividualEntrepreneur(null!);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void IndividualEntrepreneur_should_create_payer_with_person_inn()
        {
            var inn = codesGenerator.PersonInn();
            var expectedRequest = new AccountInfoRequest
            {
                Inn = inn.ToString()
            };

            var request = DraftPayer.IndividualEntrepreneur(inn).ToRequest();
            
            request.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public void LegalEntityPayer_should_fail_when_given_null_inn()
        {
            var kpp = codesGenerator.Kpp();
            
            Action action = () => DraftPayer.LegalEntityPayer(null!, kpp);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void LegalEntityPayer_should_fail_when_given_null_kpp()
        {
            var inn = codesGenerator.LegalEntityInn();
            
            Action action = () => DraftPayer.LegalEntityPayer(inn, null!);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void LegalEntityPayer_should_create_payer_with_legal_entity_inn_and_kpp()
        {
            var inn = codesGenerator.LegalEntityInn();
            var kpp = codesGenerator.Kpp();
            var expectedRequest = new AccountInfoRequest
            {
                Inn = inn.ToString(),
                Organization = new OrganizationInfoRequest
                {
                    Kpp = kpp
                }
            };

            var request = DraftPayer.LegalEntityPayer(inn, kpp).ToRequest();
            
            request.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public void WithFssRegNumber_should_fail_when_given_null_fss_reg_number()
        {
            var inn = codesGenerator.LegalEntityInn();
            var kpp = codesGenerator.Kpp();
            var payer = DraftPayer.LegalEntityPayer(inn, kpp);

            Action action = () => payer.WithFssRegNumber(null!);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void WithFssRegNumber_should_set_fss_reg_number_to_payer()
        {
            var inn = codesGenerator.LegalEntityInn();
            var kpp = codesGenerator.Kpp();
            var fssRegNumber = codesGenerator.FssRegNumber();
            var expectedRequest = new AccountInfoRequest
            {
                Inn = inn.ToString(),
                Organization = new OrganizationInfoRequest
                {
                    Kpp = kpp
                },
                RegistrationNumberFss = fssRegNumber
            };
            var payer = DraftPayer
                .LegalEntityPayer(inn, kpp)
                .WithFssRegNumber(fssRegNumber);

            var request = payer.ToRequest();
            
            request.Should().BeEquivalentTo(expectedRequest);
        }

        [Test]
        public void WithPfrRegNumber_should_fail_when_given_null_pfr_reg_number()
        {
            var inn = codesGenerator.LegalEntityInn();
            var kpp = codesGenerator.Kpp();
            var payer = DraftPayer.LegalEntityPayer(inn, kpp);

            Action action = () => payer.WithPfrRegNumber(null!);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void WithPfrRegNumber_should_set_pfr_reg_number_to_payer()
        {
            var inn = codesGenerator.LegalEntityInn();
            var kpp = codesGenerator.Kpp();
            var pfrRegNumber = codesGenerator.PfrRegNumber();
            var expectedRequest = new AccountInfoRequest
            {
                Inn = inn.ToString(),
                Organization = new OrganizationInfoRequest
                {
                    Kpp = kpp
                },
                RegistrationNumberPfr = pfrRegNumber
            };
            var payer = DraftPayer
                .LegalEntityPayer(inn, kpp)
                .WithPfrRegNumber(pfrRegNumber);

            var request = payer.ToRequest();
            
            request.Should().BeEquivalentTo(expectedRequest);
        }
    }
}
#nullable enable
using System;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.Model.Numbers;

namespace Kontur.Extern.Client.Model.Drafts
{
    public class NewDraftPayer
    {
        public static NewDraftPayer LegalEntityPayer(LegalEntityInn inn, Kpp kpp)
        {
            if (inn is null)
                throw new ArgumentNullException(nameof(inn));
            if (kpp is null)
                throw new ArgumentNullException(nameof(kpp));
            
            return new(inn.ToString(), kpp.ToString());
        }

        public static NewDraftPayer IndividualEntrepreneur(Inn inn)
        {
            if (inn is null)
                throw new ArgumentNullException(nameof(inn));

            var innString = inn.ToString() ?? throw new ArgumentNullException(nameof(inn));
            return new(innString, null);
        }

        private FssRegNumber? fssRegNumber;
        private PfrRegNumber? pfrRegNumber;
        private readonly string inn;
        private readonly string? kpp;

        private NewDraftPayer(string inn, string? kpp)
        {
            this.inn = inn;
            this.kpp = kpp;
        }

        public NewDraftPayer WithFssRegNumber(FssRegNumber value)
        {
            fssRegNumber = value ?? throw new ArgumentNullException(nameof(value));
            return this;
        }

        public NewDraftPayer WithPfrRegNumber(PfrRegNumber value)
        {
            pfrRegNumber = value ?? throw new ArgumentNullException(nameof(value));
            return this;
        }

        public AccountInfoRequest ToRequest()
        {
            var request = new AccountInfoRequest
            {
                Inn = inn,
                RegistrationNumberFss = fssRegNumber?.ToString(),
                RegistrationNumberPfr = pfrRegNumber?.ToString()
            };
            if (kpp != null)
            {
                request.Organization = new OrganizationInfoRequest
                {
                    Kpp = kpp
                };
            }

            return request;
        }
    }
}
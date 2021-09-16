using System;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Model.Drafts
{
    public class DraftPayer
    {
        public static DraftPayer LegalEntityPayer(LegalEntityInn inn, Kpp kpp)
        {
            if (inn is null)
                throw new ArgumentNullException(nameof(inn));
            if (kpp is null)
                throw new ArgumentNullException(nameof(kpp));
            
            return new(inn.ToString(), kpp);
        }

        public static DraftPayer IndividualEntrepreneur(Inn inn)
        {
            if (inn is null)
                throw new ArgumentNullException(nameof(inn));

            var innString = inn.ToString() ?? throw new ArgumentNullException(nameof(inn));
            return new(innString, null);
        }

        private FssRegNumber? fssRegNumber;
        private PfrRegNumber? pfrRegNumber;
        private readonly string inn;
        private readonly Kpp? kpp;

        private DraftPayer(string inn, Kpp? kpp)
        {
            this.inn = inn;
            this.kpp = kpp;
        }

        public DraftPayer WithFssRegNumber(FssRegNumber value)
        {
            fssRegNumber = value ?? throw new ArgumentNullException(nameof(value));
            return this;
        }

        public DraftPayer WithPfrRegNumber(PfrRegNumber value)
        {
            pfrRegNumber = value ?? throw new ArgumentNullException(nameof(value));
            return this;
        }

        public AccountInfoRequest ToRequest()
        {
            var request = new AccountInfoRequest
            {
                Inn = inn,
                RegistrationNumberFss = fssRegNumber,
                RegistrationNumberPfr = pfrRegNumber
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
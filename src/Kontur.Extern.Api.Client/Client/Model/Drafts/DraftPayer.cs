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
        private SfrRegNumber? sfrRegNumber;
        private readonly string inn;
        private readonly Kpp? kpp;
        private Okpo? okpo;
        private Ogrn? ogrn;
        private Snils? snils;

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

        public DraftPayer WithSfrRegNumber(SfrRegNumber value)
        {
            sfrRegNumber = value ?? throw new ArgumentNullException(nameof(value));
            return this;
        }

        public DraftPayer WithOkpo(Okpo value)
        {
            okpo = value ?? throw new ArgumentNullException(nameof(value));
            return this;
        }

        public DraftPayer WithOgrn(Ogrn value)
        {
            ogrn = value ?? throw new ArgumentNullException(nameof(value));
            return this;
        }

        public DraftPayer WithSnils(Snils value)
        {
            snils = value ?? throw new ArgumentNullException(nameof(value));
            return this;
        }

        public AccountInfoRequest ToRequest()
        {
            var request = new AccountInfoRequest
            {
                Inn = inn,
                RegistrationNumberFss = fssRegNumber,
                RegistrationNumberPfr = pfrRegNumber,
                RegistrationNumberSfr = sfrRegNumber,
                Okpo = okpo,
                Ogrn = ogrn,
                Snils = snils
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
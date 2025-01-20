using System;
using System.Net;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Model.Drafts
{
    public class DraftSender
    {
        public static DraftSender LegalEntity(LegalEntityInn inn, Kpp kpp, CertificateContent? certificate)
        {
            if (inn is null)
                throw new ArgumentNullException(nameof(inn));
            if (kpp is null)
                throw new ArgumentNullException(nameof(kpp));
            
            return new(inn.ToString(), kpp, certificate);
        }

        public static DraftSender IndividualEntrepreneur(Inn inn, CertificateContent? certificate)
        {
            if (inn is null)
                throw new ArgumentNullException(nameof(inn));
            
            return new(inn.ToString(), null, certificate);
        }

        private readonly string inn;
        private readonly Kpp? kpp;
        private IPAddress? ip;
        private bool isRepresentative;
        private readonly CertificateContent? certificate;

        private DraftSender(string inn, Kpp? kpp, CertificateContent? certificate)
        {
            this.inn = inn;
            this.kpp = kpp;
            this.certificate = certificate;
        }

        public DraftSender WithIpAddress(IPAddress ipAddress)
        {
            if (ipAddress == null!)
                throw new ArgumentNullException(nameof(ipAddress));
            ip = ipAddress;
            return this;
        }

        public DraftSender Representative(bool value = true)
        {
            isRepresentative = value;
            return this;
        }

        public SenderRequest ToRequest() => new()
        {
            Inn = inn,
            Kpp = kpp,
            IpAddress = ip,
            IsRepresentative = isRepresentative,
            Certificate = certificate is null ? null : new PublicKeyCertificateRequest
            {
                Content = certificate.ToBytes()
            }
        };
    }
}
#nullable enable
using System;
using System.Net;
using Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Client.Model.Numbers;

namespace Kontur.Extern.Client.Model.Drafts
{
    public class DraftSender
    {
        public static DraftSender LegalEntity(LegalEntityInn inn, Kpp kpp, CertificateContent certificatePublicKey)
        {
            if (inn is null)
                throw new ArgumentNullException(nameof(inn));
            if (kpp is null)
                throw new ArgumentNullException(nameof(kpp));
            if (certificatePublicKey is null)
                throw new ArgumentNullException(nameof(certificatePublicKey));
            
            return new(inn.ToString(), kpp.ToString(), certificatePublicKey);
        }

        public static DraftSender IndividualEntrepreneur(Inn inn, CertificateContent certificatePublicKey)
        {
            if (inn is null)
                throw new ArgumentNullException(nameof(inn));
            if (certificatePublicKey is null)
                throw new ArgumentNullException(nameof(certificatePublicKey));
            
            return new(inn.ToString(), null, certificatePublicKey);
        }

        private readonly string inn;
        private readonly string? kpp;
        private IPAddress? ip;
        private bool isRepresentative;
        private readonly CertificateContent certificatePublicKey;

        private DraftSender(string inn, string? kpp, CertificateContent certificatePublicKey)
        {
            this.inn = inn;
            this.kpp = kpp;
            this.certificatePublicKey = certificatePublicKey;
        }

        public DraftSender WithIpAddress(IPAddress ipAddress)
        {
            if (ipAddress is null!)
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
            IpAddress = ip?.ToString(),
            IsRepresentative = isRepresentative,
            Certificate = new CertificateRequest
            {
                PublicKey = certificatePublicKey.ToBytes()
            }
        };
    }
}
#nullable enable
using System;
using System.Net;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.Model.Numbers;

namespace Kontur.Extern.Client.Model.Drafts
{
    public class DraftSender
    {
        public static DraftSender LegalEntity(LegalEntityInn inn, Kpp kpp, CertificateContent certificate)
        {
            if (inn is null)
                throw new ArgumentNullException(nameof(inn));
            if (kpp is null)
                throw new ArgumentNullException(nameof(kpp));
            if (certificate is null)
                throw new ArgumentNullException(nameof(certificate));
            
            return new(inn.ToString(), kpp.ToString(), certificate);
        }

        public static DraftSender IndividualEntrepreneur(Inn inn, CertificateContent certificate)
        {
            if (inn is null)
                throw new ArgumentNullException(nameof(inn));
            if (certificate is null)
                throw new ArgumentNullException(nameof(certificate));
            
            return new(inn.ToString(), null, certificate);
        }

        private readonly string inn;
        private readonly string? kpp;
        private IPAddress? ip;
        private bool isRepresentative;
        private readonly CertificateContent certificate;

        private DraftSender(string inn, string? kpp, CertificateContent certificate)
        {
            this.inn = inn;
            this.kpp = kpp;
            this.certificate = certificate;
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
                Content = certificate.ToBytes()
            }
        };
    }
}
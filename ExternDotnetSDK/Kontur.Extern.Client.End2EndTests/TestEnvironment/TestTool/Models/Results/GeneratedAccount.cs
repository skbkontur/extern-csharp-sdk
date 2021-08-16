using System;
using Kontur.Extern.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Client.Model.Numbers;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.Models
{
    internal class GeneratedAccount
    {
        public GeneratedAccount(
            Guid accountId,
            Guid organizationId,
            Guid portalUserId,
            string inn,
            string kpp,
            string organizationName,
            Credentials credentials,
            string certificateThumbprint,
            byte[] certificatePublicPart)
        {
            AccountId = accountId;
            OrganizationId = organizationId;
            PortalUserId = portalUserId;
            Inn = LegalEntityInn.Parse(inn);
            Kpp = Kpp.Parse(kpp);
            OrganizationName = organizationName;
            Credentials = credentials;
            CertificateThumbprint = certificateThumbprint;
            CertificatePublicPart = certificatePublicPart;
        }

        public Guid AccountId { get; }
        public Guid OrganizationId { get; }
        public Guid PortalUserId { get; }
        public LegalEntityInn Inn { get; }
        public Kpp Kpp { get; }
        public string OrganizationName { get; }
        public Credentials Credentials { get; }
        public string CertificateThumbprint { get; }
        public byte[] CertificatePublicPart { get; }
    }
}
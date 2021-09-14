using System;
using Kontur.Extern.Api.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.Models.Results
{
    public class GeneratedAccount
    {
        // ReSharper disable StringLiteralTypo
        public static readonly PersonFullName DefaultChiefName = new (
            "Афанасьев",
            "Сергей",
            "Робертович"
        );
        // ReSharper restore StringLiteralTypo
        
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
            ChiefName = DefaultChiefName;
        }

        public PersonFullName ChiefName { get; set; }

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
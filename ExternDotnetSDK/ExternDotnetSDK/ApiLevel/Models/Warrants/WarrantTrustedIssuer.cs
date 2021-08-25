#nullable enable
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class WarrantTrustedIssuer
    {
        public TrustedIssuerType Type { get; set; }

        public WarrantIndividual? TrustedIssuerIndividual { get; set; }

        public TrustedIssuerOrganization? TrustedIssuerOrganization { get; set; }
    }
}
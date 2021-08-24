using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class WarrantTrustedIssuer
    {
        public TrustedIssuerType Type { get; set; }

        [CanBeNull]
        public WarrantIndividual TrustedIssuerIndividual { get; set; }

        [CanBeNull]
        public TrustedIssuerOrganization TrustedIssuerOrganization { get; set; }
    }
}
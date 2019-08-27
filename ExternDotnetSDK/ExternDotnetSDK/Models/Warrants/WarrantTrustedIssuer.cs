using JetBrains.Annotations;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class WarrantTrustedIssuer
    {
        public TrustedIssuerType Type { get; set; }

        [CanBeNull]
        public WarrantIndividual TrustedIssuerIndividual { get; set; }

        [CanBeNull]
        public TrustedIssuerOrganization TrustedIssuerOrganization { get; set; }
    }
}
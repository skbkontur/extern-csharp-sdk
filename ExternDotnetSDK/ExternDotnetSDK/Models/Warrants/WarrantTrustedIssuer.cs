using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Warrants
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
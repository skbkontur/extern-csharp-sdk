using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
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
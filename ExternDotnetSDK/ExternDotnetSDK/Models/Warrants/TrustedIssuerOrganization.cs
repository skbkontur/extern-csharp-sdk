using JetBrains.Annotations;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class TrustedIssuerOrganization : WarrantOrganization
    {
        [CanBeNull]
        public string Address { get; set; }

        [CanBeNull]
        public Fio ChiefFio { get; set; }

        [CanBeNull]
        public string ChiefInn { get; set; }
    }
}
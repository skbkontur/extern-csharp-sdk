using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Warrants
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
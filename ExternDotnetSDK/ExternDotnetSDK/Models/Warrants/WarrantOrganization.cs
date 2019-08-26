using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class WarrantOrganization
    {
        [CanBeNull]
        public string Name { get; set; }

        [CanBeNull]
        public string Inn { get; set; }

        [CanBeNull]
        public string Kpp { get; set; }

        [CanBeNull]
        public string Ogrn { get; set; }
    }
}
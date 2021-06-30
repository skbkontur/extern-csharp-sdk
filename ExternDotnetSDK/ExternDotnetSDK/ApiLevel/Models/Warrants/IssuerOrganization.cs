using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class IssuerOrganization
    {
        [CanBeNull]
        public string Name { get; set; }

        [CanBeNull]
        public string Inn { get; set; }

        [CanBeNull]
        public string Kpp { get; set; }
    }
}
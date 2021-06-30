using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class WarrantIssuer
    {
        [CanBeNull]
        public IssuerOrganization IssuerOrganization { get; set; }
    }
}
using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Warrants
{
    /// <summary>
    ///     Представляемое лицо
    /// </summary>
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class WarrantIssuer
    {
        /// <summary>
        ///     Информация об организации - представляемом лице
        /// </summary>
        [CanBeNull]
        public IssuerOrganization IssuerOrganization { get; set; }
    }
}
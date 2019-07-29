using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Warrants
{
    /// <summary>
    ///     Информация об организации - представляемом лице
    /// </summary>
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class IssuerOrganization
    {
        /// <summary>
        ///     Название
        /// </summary>
        [CanBeNull]
        public string Name { get; set; }

        /// <summary>
        ///     ИНН
        /// </summary>
        [CanBeNull]
        public string Inn { get; set; }

        /// <summary>
        ///     КПП
        /// </summary>
        [CanBeNull]
        public string Kpp { get; set; }
    }
}
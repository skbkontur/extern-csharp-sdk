using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Warrants
{
    /// <summary>
    ///     Информация об организации из доверенности
    /// </summary>
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class WarrantOrganization
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

        /// <summary>
        ///     ОГРН
        /// </summary>
        [CanBeNull]
        public string Ogrn { get; set; }
    }
}
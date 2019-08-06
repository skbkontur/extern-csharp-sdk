using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Warrants
{
    /// <summary>
    ///     Информация об организации-передоверителе
    /// </summary>
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class TrustedIssuerOrganization : WarrantOrganization
    {
        /// <summary>
        ///     Юридический  адрес
        /// </summary>
        [CanBeNull]
        public string Address { get; set; }

        /// <summary>
        ///     ФИО руководителя организации
        /// </summary>
        [CanBeNull]
        public Fio ChiefFio { get; set; }

        /// <summary>
        ///     ИНН руководителя организации
        /// </summary>
        [CanBeNull]
        public string ChiefInn { get; set; }
    }
}
using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Warrants
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
using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Warrants
{
    /// <summary>
    ///     Нотариус
    /// </summary>
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Notary
    {
        /// <summary>
        ///     ФИО нотариуса
        /// </summary>
        [CanBeNull]
        public Fio Fio { get; set; }

        /// <summary>
        ///     ИНН
        /// </summary>
        [CanBeNull]
        public string Inn { get; set; }

        /// <summary>
        ///     Адрес
        /// </summary>
        [CanBeNull]
        public string Address { get; set; }

        /// <summary>
        ///     Информация об организации, в случае если это нотариальная контора
        /// </summary>
        [CanBeNull]
        public WarrantOrganization NotaryOrganization { get; set; }
    }
}
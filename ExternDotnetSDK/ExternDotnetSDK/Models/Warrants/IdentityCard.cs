using System;
using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Warrants
{
    /// <summary>
    ///     Удостоверение личности
    /// </summary>
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class IdentityCard
    {
        /// <summary>
        ///     Тип удостоверения личности
        /// </summary>
        [CanBeNull]
        public string DocumentType { get; set; }

        /// <summary>
        ///     Серия
        /// </summary>
        [CanBeNull]
        public string Series { get; set; }

        /// <summary>
        ///     Номер
        /// </summary>
        [CanBeNull]
        public string Number { get; set; }

        /// <summary>
        ///     Кем выдан
        /// </summary>
        [CanBeNull]
        public string Issuer { get; set; }

        /// <summary>
        ///     Дата выдачи
        /// </summary>
        [CanBeNull]
        public DateTime? IssuanceDate { get; set; }

        /// <summary>
        ///     Код подразделения
        /// </summary>
        [CanBeNull]
        public string IssuerCode { get; set; }
    }
}
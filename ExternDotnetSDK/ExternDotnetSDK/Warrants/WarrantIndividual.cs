using System;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Warrants
{
    /// <summary>
    ///     Информация о частном лице или индивидуальном предпринимателе из доверенности
    /// </summary>
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class WarrantIndividual
    {
        /// <summary>
        ///     ФИО
        /// </summary>
        [CanBeNull]
        public Fio Fio { get; set; }

        /// <summary>
        ///     ИНН
        /// </summary>
        [CanBeNull]
        public string Inn { get; set; }

        /// <summary>
        ///     Удостоверение личности
        /// </summary>
        [CanBeNull]
        public IdentityCard Document { get; set; }

        /// <summary>
        ///     Дата рождения
        /// </summary>
        [CanBeNull]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        ///     ОГРНИП
        /// </summary>
        [CanBeNull]
        public string Ogrnip { get; set; }

        /// <summary>
        ///     Гражданство
        /// </summary>
        [CanBeNull]
        public string Citizenship { get; set; }

        /// <summary>
        ///     Место жительства
        /// </summary>
        [CanBeNull]
        public string Address { get; set; }
    }
}
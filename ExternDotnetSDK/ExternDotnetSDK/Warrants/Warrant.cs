using System;
using System.Collections.Generic;
using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Warrants
{
    /// <summary>
    ///     Доверенность
    /// </summary>
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Warrant
    {
        /// <summary>
        ///     Дата начала действия доверенности
        /// </summary>
        [CanBeNull]
        public DateTime? DateBegin { get; set; }

        /// <summary>
        ///     Дата окончания действия доверенности
        /// </summary>
        [CanBeNull]
        public DateTime? DateEnd { get; set; }

        /// <summary>
        ///     Номер доверенности
        /// </summary>
        [CanBeNull]
        public string Number { get; set; }

        /// <summary>
        ///     Список полномочий представителя
        /// </summary>
        [CanBeNull]
        public List<int> Permissions { get; set; } = new List<int>();

        /// <summary>
        ///     Нотариус
        /// </summary>
        [CanBeNull]
        public Notary Notary { get; set; }

        /// <summary>
        ///     Уполномоченный представитель, на которого выдана доверенность (отправитель)
        /// </summary>
        [CanBeNull]
        public WarrantSender Sender { get; set; }

        /// <summary>
        ///     Представляемое лицо
        /// </summary>
        [CanBeNull]
        public WarrantIssuer Issuer { get; set; }

        /// <summary>
        ///     Представитель, который выдал доверенность.
        ///     Присутствует в случае передоверия, когда доверенность выдана не самим представляемым лицом, а его представителем.
        ///     Отсутствует в случае, когда лицо, выдавшее доверенность, (доверитель) совпадает с представляемым лицом.
        /// </summary>
        [CanBeNull]
        public WarrantTrustedIssuer TrustedIssuer { get; set; }
    }
}
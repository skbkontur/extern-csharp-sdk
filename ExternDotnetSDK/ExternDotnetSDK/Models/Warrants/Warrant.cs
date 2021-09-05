#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Warrants
{
    /// <summary>
    /// Доверенность
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Warrant
    {
        /// <summary>
        /// Дата начала действия доверенности
        /// </summary>
        [CanBeNull]
        public DateTime? DateBegin { get; set; }

        /// <summary>
        /// Дата окончания действия доверенности
        /// </summary>
        [CanBeNull]
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Номер доверенности
        /// </summary>
        public string? Number { get; set; }

        /// <summary>
        /// Список полномочий представителя
        /// </summary>
        public List<int>? Permissions { get; set; } = new List<int>();

        /// <summary>
        /// Нотариус
        /// </summary>
        public Notary? Notary { get; set; }

        /// <summary>
        /// Уполномоченный представитель, на которого выдана доверенность (отправитель)
        /// </summary>
        public WarrantSender? Sender { get; set; }

        /// <summary>
        /// Представляемое лицо
        /// </summary>
        public WarrantIssuer? Issuer { get; set; }

        /// <summary>
        /// Представитель, который выдал доверенность. 
        /// Присутствует в случае передоверия, когда доверенность выдана не самим представляемым лицом, а его представителем.
        /// Отсутствует в случае, когда лицо, выдавшее доверенность, (доверитель) совпадает с представляемым лицом.
        /// </summary>
        public WarrantTrustedIssuer? TrustedIssuer { get; set; }
    }
}
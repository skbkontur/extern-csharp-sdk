#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    /// <summary>
    /// Удостоверение личности
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class IdentityCard
    {
        /// <summary>
        /// Тип удостоверения личности
        /// </summary>
        public string? DocumentType { get; set; }

        /// <summary>
        /// Серия
        /// </summary>
        public string? Series { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string? Number { get; set; }

        /// <summary>
        /// Кем выдан
        /// </summary>
        public string? Issuer { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? IssuanceDate { get; set; }

        /// <summary>
        /// Код подразделения
        /// </summary>
        public string? IssuerCode { get; set; }
    }
}
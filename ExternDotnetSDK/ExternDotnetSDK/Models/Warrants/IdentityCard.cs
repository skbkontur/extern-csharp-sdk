#nullable enable
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Common.Time;

namespace Kontur.Extern.Client.Models.Warrants
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
        public DateOnly? IssuanceDate { get; set; }

        /// <summary>
        /// Код подразделения
        /// </summary>
        public string? IssuerCode { get; set; }
    }
}
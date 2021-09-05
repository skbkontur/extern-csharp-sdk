#nullable enable
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.Models.Warrants
{
    /// <summary>
    /// Нотариус
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Notary
    {
        /// <summary>
        /// ФИО нотариуса
        /// </summary>
        [JsonPropertyName("fio")]
        public PersonFullName? Fio { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string? Inn { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Информация об организации, в случае если это нотариальная контора
        /// </summary>
        public WarrantOrganization? NotaryOrganization { get; set; }
    }
}
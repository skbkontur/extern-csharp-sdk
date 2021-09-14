#nullable enable
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Warrants
{
    /// <summary>
    /// Информация об организации-передоверителе
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class TrustedIssuerOrganization : WarrantOrganization
    {
        /// <summary>
        /// Юридический  адрес
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// ФИО руководителя организации
        /// </summary>
        [JsonPropertyName("chief-fio")]
        public PersonFullName? ChiefFullName { get; set; }

        /// <summary>
        /// ИНН руководителя организации
        /// </summary>
        public string? ChiefInn { get; set; }
    }
}
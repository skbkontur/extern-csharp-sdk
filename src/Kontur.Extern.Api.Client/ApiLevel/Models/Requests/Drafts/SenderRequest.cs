using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class SenderRequest
    {
        /// <summary>
        /// ИНН
        /// </summary>
        //[Required]
        public string Inn { get; set; } = null!;

        /// <summary>
        /// КПП
        /// </summary>
        public Kpp? Kpp { get; set; }

        /// <summary>
        /// Сертификат для отправки
        /// </summary>
        // [Required]
        public PublicKeyCertificateRequest? Certificate { get; set; }

        /// <summary>
        /// Отправитель является представителем
        /// </summary>
        //[Required]
        public bool IsRepresentative { get; set; }

        /// <summary>
        /// IP адрес отправителя отчета
        /// </summary>
        [JsonPropertyName("ipaddress")]
        public IPAddress? IpAddress { get; set; }
    }
}
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Sender
    {
        /// <summary>
        ///  ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        ///  КПП
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        ///  Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  Сертификат для отправки
        /// </summary>
        // [Required]
        public Certificate Certificate { get; set; }

        /// <summary>
        ///  Отправитель является представителем
        /// </summary>
        public bool IsRepresentative { get; set; }

        /// <summary>
        ///  IP-адрес отправителя отчета
        /// </summary>
        [JsonPropertyName("ipaddress")]
        public string IpAddress { get; set; }
    }
}
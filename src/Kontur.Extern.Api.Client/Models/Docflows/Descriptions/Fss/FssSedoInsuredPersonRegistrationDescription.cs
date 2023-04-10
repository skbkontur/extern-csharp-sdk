using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    public class FssSedoInsuredPersonRegistrationDescription : FssSedoDescription
    {
        /// <summary>
        /// Отпечаток сертификата отправителя
        /// </summary>
        public string? SenderCertificateThumbprint { get; set; }

        /// <summary>
        /// ИНН организации, за которую сдается отчет
        /// </summary>
        public string? PayerInn { get; set; }

        /// <summary>
        /// СНИЛС
        /// </summary>
        public string? Snils { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        [JsonPropertyName("fio")]
        public PersonFullName PersonFullName { get; set; } = null!;

        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion? FormVersion { get; set; }

        /// <summary>
        /// Идентификатор доверенности
        /// </summary>
        public Guid? WarrantId { get; set; }
    }
}
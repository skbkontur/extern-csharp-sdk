using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Certificates
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Certificate
    {
        /// <summary>
        /// ФИО владельца сертификата
        /// </summary>
        public string Fio { get; set; } = null!;

        /// <summary>
        /// ИНН организации
        /// </summary>
        public string Inn { get; set; } = null!;

        /// <summary>
        /// КПП организации если есть
        /// </summary>
        public string? Kpp { get; set; }

        /// <summary>
        /// Валидность сертификата
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Является ли сертификат КЭП
        /// </summary>
        public bool IsQualified { get; set; }

        /// <summary>
        /// Контент
        /// </summary>
        public byte[] Content { get; set; } = null!;

        /// <summary>
        /// Отпечаток сертификата
        /// </summary>
        public string Thumbprint { get; set; } = null!;

        /// <summary>
        /// Действителен по 
        /// </summary>
        public DateTime ExpiredAt { get; set; }

        /// <summary>
        /// Серийный номер
        /// </summary>
        public string? SerialNumber { get; set; }
    }
}
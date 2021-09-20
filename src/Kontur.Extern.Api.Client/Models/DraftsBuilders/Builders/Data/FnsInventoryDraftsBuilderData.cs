using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Model.Drafts;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FnsInventoryDraftsBuilderData : DraftsBuilderData
    {
        /// <summary>
        /// Конструктор данных ответа на требование
        /// </summary>
        /// <param name="relatedDocument">Связанный документ (требование, письмо или отчет), на который формируется ответ</param>
        /// <param name="idFileOsn">Идентификатор файла основания (отчета), в ответ на который формируется данный файл (опись).</param>
        /// <param name="additionalCertificates">Сертификаты дополнительных подписантов (публичная часть в base64)</param>
        public FnsInventoryDraftsBuilderData(RelatedDocument relatedDocument, string? idFileOsn = null, string[]? additionalCertificates = null)
        {
            RelatedDocument = relatedDocument ?? throw new ArgumentNullException(nameof(relatedDocument));
            IdFileOsn = string.IsNullOrWhiteSpace(idFileOsn) ? null : idFileOsn;
            AdditionalCertificates = additionalCertificates;
        }
        
        /// <summary>
        /// Связанный документ (требование, письмо или отчет), на который формируется ответ
        /// </summary>
        public RelatedDocument RelatedDocument { get; }

        /// <summary>
        /// Идентификатор файла основания (отчета), в ответ на который формируется данный файл (опись).
        /// Параметр предназначен для заполнения поля 'ИдФайлОсн' в описи, направляемой на отчет, который отправлен через другого оператора ЭДО.
        /// Если отчет отправлен через Контур.Экстерн, то заполнению подлежит только параметр related-document.
        /// Подробнее читайте в [документации](https://docs-ke.readthedocs.io/ru/latest/builder/%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B%20%D0%B1%D0%B8%D0%BB%D0%B4%D0%B5%D1%80%D0%B0.html#rst-markup-createdb)
        /// </summary>
        public string? IdFileOsn { get; }

        /// <summary>
        /// Сертификаты дополнительных подписантов (публичная часть в base64)
        /// </summary>
        public string[]? AdditionalCertificates { get; }
    }
}
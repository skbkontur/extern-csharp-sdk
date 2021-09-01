using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FnsInventoryDraftsBuilderData : DraftsBuilderData
    {
        /// <summary>
        /// Связанный документ (требование, письмо или отчет), на который формируется ответ
        /// </summary>
        //[Required]
        public RelatedDocumentRequest RelatedDocument { get; set; }

        /// <summary>
        /// Идентификатор файла основания (отчета), в ответ на который формируется данный файл (опись).
        /// Параметр предназначен для заполнения поля 'ИдФайлОсн' в описи, направляемой на отчет, который отправлен через другого оператора ЭДО.
        /// Если отчет отправлен через Контур.Экстерн, то заполнению подлежит только параметр related-document.
        /// Подробнее читайте в [документации](https://docs-ke.readthedocs.io/ru/latest/builder/%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B%20%D0%B1%D0%B8%D0%BB%D0%B4%D0%B5%D1%80%D0%B0.html#rst-markup-createdb)
        /// </summary>
        public string IdFileOsn { get; set; }

        /// <summary>
        /// Сертификаты дополнительных подписантов (публичная часть в base64)
        /// </summary>
        public string[] AdditionalCertificates { get; set; }
    }
}
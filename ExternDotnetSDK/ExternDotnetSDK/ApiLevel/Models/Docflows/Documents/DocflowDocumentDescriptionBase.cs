using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.Attributes;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public abstract class DocflowDocumentDescriptionBase<TRequisites>
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        public Urn Type { get; set; }
        
        /// <summary>
        /// Наименование файла
        /// </summary>
        public string Filename { get; set; }
        
        /// <summary>
        /// Тип контента
        /// </summary> 
        public string ContentType { get; set; }
        
        /// <summary>
        /// Размер расшифрованного контента, если он есть
        /// </summary>
        public long? DecryptedContentSize { get; set; }
        
        /// <summary>
        /// Размер зашифрованного контента, если он есть
        /// </summary>
        public long? EncryptedContentSize { get; set; }
        
        /// <summary>
        /// Признак сжатого контента
        /// </summary>
        public bool Compressed { get; set; }
        
        /// <summary>
        /// Реквизиты документа
        /// </summary>
        public TRequisites Requisites { get; set; }
        
        /// <summary>
        /// Количество связанных документооборотов
        /// </summary>
        public long? RelatedDocflowsCount { get; set; }
        
        /// <summary>
        /// Поддержка распознавания документа
        /// </summary>
        public bool SupportRecognition { get; set; }
        
        /// <summary>
        /// Сертификаты, на которые был зашифрован документооборот
        /// </summary>
        public EncryptedCertificate[] EncryptedCertificates { get; set; }

        /// <summary>
        /// Поддержка печати документа
        /// </summary>
        [YesNoUnknownNullableBoolJsonProperty]
        public bool? SupportPrint { get; set; }
    }
}
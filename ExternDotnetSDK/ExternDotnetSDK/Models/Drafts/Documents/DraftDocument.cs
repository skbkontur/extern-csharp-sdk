using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Drafts.Documents
{
    /// <summary>
    /// Документ черновика
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftDocument
    {
        /// <summary>
        /// Идентификатор документа черновика
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ссылка на контент подписи документа
        /// </summary>
        public Link SignatureContentLink { get; set; }
        
        /// <summary>
        /// Список подписей документа
        /// </summary>
        public List<Signature> Signatures { get; set; }
        
        /// <summary>
        /// Данные о документе
        /// </summary>
        public DocumentDescription Description { get; set; }
        
        /// <summary>
        /// Контент документа
        /// </summary>
        public List<DraftDocumentContent> Contents { get; set; }
        
        /// <summary>
        /// Идентификатор контента подписи для формирования raw-подписи отчетов в ПФР. [Подробнее в документации](https://docs-ke.readthedocs.io/ru/latest/manuals/xmldsig.html)
        /// </summary>
        public Guid? DataToSignContentId { get; set; }
    }
}
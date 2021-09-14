using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    [DebuggerDisplay("Document {Description.Type}")]
    public class Document
    {
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Описание документа
        /// </summary>
        public DocflowDocumentDescription Description { get; set; }
        
        /// <summary>
        /// Контент документа
        /// </summary>
        public Content Content { get; set; }
        
        /// <summary>
        /// Дата отправки документа
        /// </summary>
        public DateOnly? SendDate { get; set; }
        
        /// <summary>
        /// Подписи документа
        /// </summary>
        public Signature[] Signatures { get; set; }
        
        /// <summary>
        /// Ссылки для работы с документом
        /// </summary>
        public Link[] Links { get; set; }
    }
}
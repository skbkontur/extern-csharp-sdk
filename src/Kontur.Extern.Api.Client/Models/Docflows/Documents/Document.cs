using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

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
        public DocflowDocumentDescription Description { get; set; } = null!;
        
        /// <summary>
        /// Контент документа
        /// </summary>
        public Content? Content { get; set; }
        
        /// <summary>
        /// Дата и время отправки документа
        /// </summary>
        [JsonPropertyName("send-date")]
        public DateTime? SendDateTime { get; set; }
        
        /// <summary>
        /// Подписи документа
        /// </summary>
        public Signature[] Signatures { get; set; } = null!;
        
        /// <summary>
        /// Ссылки для работы с документом
        /// </summary>
        public Link[] Links { get; set; } = null!;
    }
}
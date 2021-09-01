using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Documents;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DocumentRequest
    {
        /// <summary>
        /// Идентификатор документа в сервисе контентов
        /// </summary>
        public Guid? ContentId { get; set; }

        /// <summary>
        /// Подпись документа
        /// </summary>
        public byte[] Signature { get; set; }

        /// <summary>
        /// Описание документа
        /// </summary>
        public DocumentDescriptionRequest Description { get; set; }
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Requests.Docflows.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PrintDocumentRequest
    {
        /// <summary>
        /// Идентификатор расшифрованного документа в сервисе контентов 
        /// </summary>
        public Guid ContentId { get; set; }
    }
}
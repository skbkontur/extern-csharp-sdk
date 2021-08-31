using System;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Documents.Data
{
    public class PrintDocumentRequest
    {
        /// <summary>
        /// Идентификатор расшифрованного документа в сервисе контентов 
        /// </summary>
        public Guid ContentId { get; set; }
    }
}
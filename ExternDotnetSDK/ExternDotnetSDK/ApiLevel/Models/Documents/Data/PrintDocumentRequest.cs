using System;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents.Data
{
    public class PrintDocumentRequest
    {
        /// <summary>
        /// Идентификатор расшифрованного документа в сервисе контентов 
        /// </summary>
        public Guid ContentId { get; set; }
    }
}
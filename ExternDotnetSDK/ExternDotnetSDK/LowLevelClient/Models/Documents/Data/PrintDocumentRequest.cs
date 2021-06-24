using System;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Documents.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class PrintDocumentRequest
    {
        /// <summary>
        /// Идентификатор расшифрованного документа в сервисе контентов 
        /// </summary>
        public Guid ContentId { get; set; }
    }
}
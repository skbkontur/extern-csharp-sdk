using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents.Data
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
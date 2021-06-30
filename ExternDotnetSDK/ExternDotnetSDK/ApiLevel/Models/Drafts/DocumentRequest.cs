using System;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DocumentRequest
    {
        public string Base64Content { get; set; }

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
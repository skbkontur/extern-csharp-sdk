using System;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.DocumentFiles
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderFileRequest
    {
        /// <summary>
        /// Идентификатор контента в сервисе контентов
        /// </summary>
        public Guid ContentId { get; set; }

        /// <summary>
        /// Контент подписи файла
        /// </summary>
        public byte[] Signature { get; set; }

        /// <summary>
        /// Метаинформация файла
        /// </summary>
        public DraftsBuilderFileMetaRequest Meta { get; set; }
    }
}
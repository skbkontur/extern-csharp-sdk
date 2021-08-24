using System;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles
{
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
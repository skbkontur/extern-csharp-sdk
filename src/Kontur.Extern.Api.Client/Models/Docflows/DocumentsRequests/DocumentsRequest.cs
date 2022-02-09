using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Docflows.DocumentsRequests
{
    [PublicAPI]
    public class DocumentsRequest
    {
        /// <summary>
        /// Идентификатор запроса
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Контент запроса
        /// </summary>
        public byte[] Content { get; set; }
        /// <summary>
        /// Данные для подписи
        /// </summary>
        public byte[] DataToSign { get; set; }
        /// <summary>
        /// Подпись пользователя
        /// </summary>
        public byte[] Signature { get; set; }
        /// <summary>
        /// Ссылки для работы с запросом
        /// </summary>
        public List<Link> Links { get; set; }
        /// <summary>
        /// Идентификатор документооборота, в котором сформирован запрос
        /// </summary>
        public Guid DocflowId { get; set; }
    }
}
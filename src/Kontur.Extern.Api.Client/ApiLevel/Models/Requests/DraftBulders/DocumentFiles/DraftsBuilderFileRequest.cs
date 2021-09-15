using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBulders.DocumentFiles
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderFileRequest
    {
        /// <summary>
        /// Идентификатор контента в сервисе контентов
        /// </summary>
        public Guid? ContentId { get; set; }
        
        /// <summary>
        /// Контент подписи файла в формате base64
        /// </summary>
        public string? Base64SignatureContent { get; set; }
        
        /// <summary>
        /// Метаинформация файла
        /// </summary>
        //[Required]
        public DraftsBuilderFileMetaRequest Meta { get; set; } = null!;
    }
}
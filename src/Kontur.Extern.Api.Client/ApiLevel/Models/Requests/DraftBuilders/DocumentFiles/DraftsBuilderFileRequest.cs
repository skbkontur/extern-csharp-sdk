using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.DocumentFiles
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderFileRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentId">Идентификатор контента в сервисе контентов</param>
        /// <param name="base64SignatureContent">Контент подписи файла в формате base64</param>
        /// <param name="meta">Метаинформация файла</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DraftsBuilderFileRequest(Guid? contentId, string? base64SignatureContent, DraftsBuilderFileMetaRequest meta)
        {
            ContentId = contentId;
            Base64SignatureContent = base64SignatureContent;
            Meta = meta ?? throw new ArgumentNullException(nameof(meta));
        }
        
        /// <summary>
        /// Идентификатор контента в сервисе контентов
        /// </summary>
        public Guid? ContentId { get; }
        
        /// <summary>
        /// Контент подписи файла в формате base64
        /// </summary>
        public string? Base64SignatureContent { get; set; }
        
        /// <summary>
        /// Метаинформация файла
        /// </summary>
        public DraftsBuilderFileMetaRequest Meta { get; }
    }
}
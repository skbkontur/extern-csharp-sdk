using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocumentFile
    {
        /// <summary>
        /// Идентификатор файла документа
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Идентификатор DraftsBuilder
        /// </summary>
        public Guid DraftsBuilderId { get; set; }
        
        /// <summary>
        /// Идентификатор документа DraftsBuilder
        /// </summary>
        public Guid DraftsBuilderDocumentId { get; set; }
        
        /// <summary>
        ///  Внимание. Параметр устарел. Используйте вместо него content-id
        /// </summary>
        public Link ContentLink { get; set; }
        
        /// <summary>
        /// Идентификатор контента файла в сервисе контентов
        /// </summary>
        public Guid? ContentId { get; set; }
        
        /// <summary>
        /// Ссылка на контент подписи файла
        /// </summary>
        public Link SignatureContentLink { get; set; }
        
        /// <summary>
        /// Метаинформация файла документа DraftsBuilder
        /// </summary>
        public DraftsBuilderDocumentFileMeta Meta { get; set; }
    }
}
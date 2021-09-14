using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Drafts.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OriginalDraftsBuilder
    {
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public Guid FileId { get; set; }
        
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public Guid DocumentId { get; set; }
    }
}
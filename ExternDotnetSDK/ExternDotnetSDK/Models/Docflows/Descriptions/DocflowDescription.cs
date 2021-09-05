using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Docflows.Descriptions
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public abstract class DocflowDescription
    {
        /// <summary>
        /// Идентификатор черновика документооборота, если он был создан через API
        /// </summary>
        public Guid? OriginalDraftId { get; set; }
    }
}
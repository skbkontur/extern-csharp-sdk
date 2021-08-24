using System;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    public abstract class DocflowDescription
    {
        /// <summary>
        /// Идентификатор черновика документооборота, если он был создан через API
        /// </summary>
        public Guid? OriginalDraftId { get; set; }
    }
}
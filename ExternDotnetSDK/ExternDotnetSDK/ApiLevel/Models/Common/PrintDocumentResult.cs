using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PrintDocumentResult
    {
        /// <summary>
        /// Идентификатор контента печатной формы документа
        /// </summary>
        public Guid ContentId { get; set; }
    }
}
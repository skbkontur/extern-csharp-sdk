using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class RecognizeRequest
    {
        /// <summary>
        /// Идентификатор контента
        /// </summary>
        public Guid ContentId { get; set; }
    }
}
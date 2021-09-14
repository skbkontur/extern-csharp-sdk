using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows.Documents
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
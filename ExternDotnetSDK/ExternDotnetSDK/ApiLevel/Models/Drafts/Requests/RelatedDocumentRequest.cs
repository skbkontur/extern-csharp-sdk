using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class RelatedDocumentRequest
    {
        /// <summary>
        /// Идентификатор связанного документооборота
        /// </summary>
        public Guid RelatedDocflowId { get; set; }

        /// <summary>
        ///  Идентификатор документа в связанном документообороте (требование или отчет)
        /// </summary>
        public Guid RelatedDocumentId { get; set; }
    }
}
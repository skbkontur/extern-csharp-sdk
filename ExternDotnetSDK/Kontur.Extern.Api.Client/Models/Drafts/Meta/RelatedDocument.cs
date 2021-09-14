using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Drafts.Meta
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class RelatedDocument
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
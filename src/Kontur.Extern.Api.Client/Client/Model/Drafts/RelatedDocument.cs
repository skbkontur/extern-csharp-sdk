using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Model.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class RelatedDocument
    {
        /// <summary>
        /// Cвязанный документ
        /// </summary>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа в связанном документообороте (требование или отчет)</param>
        public RelatedDocument(Guid relatedDocflowId, Guid relatedDocumentId)
        {
            RelatedDocflowId = relatedDocflowId;
            RelatedDocumentId = relatedDocumentId;
        }
        
        /// <summary>
        /// Идентификатор связанного документооборота
        /// </summary>
        public Guid RelatedDocflowId { get; }

        /// <summary>
        ///  Идентификатор документа в связанном документообороте (требование или отчет)
        /// </summary>
        public Guid RelatedDocumentId { get; }
    }
}
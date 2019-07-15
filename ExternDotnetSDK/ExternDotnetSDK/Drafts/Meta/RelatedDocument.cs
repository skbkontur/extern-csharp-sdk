using System;

namespace ExternDotnetSDK.Drafts.Meta
{
    public class RelatedDocument
    {
        /// <summary>
        ///     Идентификатор связанного ДО
        /// </summary>
        public Guid RelatedDocflowId { get; set; }

        /// <summary>
        ///     Идентификатор документа в ДО
        /// </summary>
        public Guid RelatedDocumentId { get; set; }
    }
}
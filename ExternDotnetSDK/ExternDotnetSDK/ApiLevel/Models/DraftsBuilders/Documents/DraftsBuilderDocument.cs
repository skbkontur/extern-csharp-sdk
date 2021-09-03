using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocument
    {
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Идентификатор DraftsBuilder
        /// </summary>
        public Guid DraftsBuilderId { get; set; }
        
        /// <summary>
        /// Метаинформация документа DraftsBuilder
        /// </summary>
        public DraftsBuilderDocumentMeta Meta { get; set; }
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Drafts.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftDocumentContent
    {
        /// <summary>
        /// Идентификатор контента
        /// </summary>
        public Guid ContentId { get; set; }
        
        /// <summary>
        /// Признак зашифрованности контента
        /// </summary>
        public bool Encrypted { get; set; }
    }
}
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocumentMeta
    {
        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        // [Required]
        public Urn BuilderType { get; set; }
        
        /// <summary>
        /// Сведения о документе
        /// </summary>
        public DraftsBuilderDocumentData BuilderData { get; set; }
    }
}
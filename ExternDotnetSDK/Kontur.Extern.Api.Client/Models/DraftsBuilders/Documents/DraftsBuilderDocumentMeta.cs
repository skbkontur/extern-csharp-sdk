using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocumentMeta : IDraftsBuilderMeta<DraftsBuilderDocumentData>
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
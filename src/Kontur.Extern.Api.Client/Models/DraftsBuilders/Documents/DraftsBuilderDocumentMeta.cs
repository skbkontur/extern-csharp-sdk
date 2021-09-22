using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocumentMeta : IDraftsBuilderMeta<DraftsBuilderDocumentData>
    {
        public DraftsBuilderDocumentMeta(DraftBuilderType builderType, DraftsBuilderDocumentData? builderData)
        {
            if (builderType.IsEmpty)
                throw Errors.ValueShouldNotBeEmpty(nameof(builderType));
            
            BuilderType = builderType;
            BuilderData = builderData;
        }
        
        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        public DraftBuilderType BuilderType { get; }

        /// <summary>
        /// Сведения о документе
        /// </summary>
        public DraftsBuilderDocumentData? BuilderData { get; }
    }
}
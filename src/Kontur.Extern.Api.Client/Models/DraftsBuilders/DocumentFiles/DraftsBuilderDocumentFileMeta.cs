using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocumentFileMeta : IDraftsBuilderMeta<DraftsBuilderDocumentFileData>
    {
        public DraftsBuilderDocumentFileMeta(string fileName, DraftBuilderType builderType, DraftsBuilderDocumentFileData? builderData)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw Errors.JsonDoesNotContainProperty(nameof(fileName));
            
            if (builderType.IsEmpty)
                throw Errors.ValueShouldNotBeEmpty(nameof(builderType));

            FileName = fileName;
            BuilderType = builderType;
            BuilderData = builderData;
        }
        
        /// <summary>
        /// Название файла
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        public DraftBuilderType BuilderType { get; }

        /// <summary>
        /// Дополнительная информация о файле
        /// </summary>
        public DraftsBuilderDocumentFileData? BuilderData { get; }
    }
}
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.DocumentFiles
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderFileMetaRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">Название файла</param>
        /// <param name="builderData">Сведения о файле</param>
        /// <exception cref="ContractException"></exception>
        public DraftsBuilderFileMetaRequest(string fileName, DraftsBuilderDocumentFileData? builderData)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw Errors.RequiredJsonPropertyIsMissed(nameof(fileName));
            
            FileName = fileName;
            BuilderData = builderData;
        }
        
        /// <summary>
        /// Название файла
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Сведения о файле
        /// </summary>
        public DraftsBuilderDocumentFileData? BuilderData { get; }
    }
}
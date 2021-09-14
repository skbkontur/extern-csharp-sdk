﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBulders.DocumentFiles
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderFileMetaRequest
    {
        /// <summary>
        /// Название файла
        /// </summary>
        //[Required]
        public string FileName { get; set; }
        
        /// <summary>
        /// Сведения о файле
        /// </summary>
        public DraftsBuilderDocumentFileData BuilderData { get; set; }
    }
}
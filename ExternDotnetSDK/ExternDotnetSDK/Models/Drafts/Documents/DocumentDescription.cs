using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Docflows.Documents.Enums;

namespace Kontur.Extern.Client.Models.Drafts.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DocumentDescription
    {
        /// <summary>
        /// Тип документа
        /// </summary>
        public DocumentType Type { get; set; }
        
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Filename { get; set; }
        
        /// <summary>
        /// Тип контента документа
        /// </summary>
        public string ContentType { get; set; }
        
        /// <summary>
        /// Свойства документа
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }
        
        /// <summary>
        /// Идентификатор документа и файла из конструктора черновиков, если черновик создан с его помощью
        /// </summary>
        public OriginalDraftsBuilder OriginalDraftsBuilder { get; set; }
    }
}
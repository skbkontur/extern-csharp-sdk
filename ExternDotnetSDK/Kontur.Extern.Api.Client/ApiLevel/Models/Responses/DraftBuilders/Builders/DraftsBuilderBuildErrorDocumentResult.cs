using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.DraftBuilders.Builders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderBuildErrorDocumentResult
    {
        /// <summary>
        /// Идентификатор документа, в котором были выявлены ошибки при сборке
        /// </summary>
        public Guid DocumentId { get; set; }
        
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
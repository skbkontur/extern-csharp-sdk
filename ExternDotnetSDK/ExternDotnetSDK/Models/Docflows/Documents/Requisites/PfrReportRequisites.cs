using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents.Requisites
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PfrReportRequisites : DocflowDocumentRequisites
    {
        /// <summary>
        /// Тип корректировки
        /// </summary>
        public PfrReportCorrectionType CorrectionType { get; set; }
    }
}
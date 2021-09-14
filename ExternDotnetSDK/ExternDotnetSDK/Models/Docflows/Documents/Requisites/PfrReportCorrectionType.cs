using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents.Requisites
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum PfrReportCorrectionType
    {
        Initial,
        Corrective,
        Abrogative,
        Additional,
        PensionAssignment,
        Special
    }
}
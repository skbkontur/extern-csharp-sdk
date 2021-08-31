using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents.Requisites
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
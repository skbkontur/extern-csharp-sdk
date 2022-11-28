using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables;

[PublicAPI]
public enum DocflowReportStatus
{
    Error = 0,
    Declined = 1,
    Sent = 2,
    Accepted = 3,
    Editing = 4,
    AcceptedWithRebuke = 5,
}
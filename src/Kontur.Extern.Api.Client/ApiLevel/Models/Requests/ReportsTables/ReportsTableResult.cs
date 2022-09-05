using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.ReportsTables;

[PublicAPI]
public class ReportsTableResult
{
    /// <summary>
    /// Список таблиц отчётностей
    /// </summary>
    public ReportsTable[] ReportsTables { get; set; }
}
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.ReportsTables;

[PublicAPI]
public class ReportsTableList
{
    /// <summary>
    /// Список таблиц отчётностей
    /// </summary>
    public ReportsTable[] ReportsTables { get; set; }
}
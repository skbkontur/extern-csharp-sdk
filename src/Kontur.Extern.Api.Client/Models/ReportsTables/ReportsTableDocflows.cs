using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables;

[PublicAPI]
public class ReportsTableDocflows
{
    /// <summary>
    /// Список документооборотов
    /// </summary>
    public ReportsTableDocflow[] Docflows { get; set; } = null!;
}
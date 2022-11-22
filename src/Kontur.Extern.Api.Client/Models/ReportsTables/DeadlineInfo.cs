using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class DeadlineInfo
{
    /// <summary>
    /// Дедлайн
    /// </summary>
    public DateTime Deadline { get; set; }
    /// <summary>
    /// Год периода
    /// </summary>
    public int PeriodYear { get; set; }
    /// <summary>
    /// Номер периода
    /// </summary>
    public int PeriodNumber { get; set; }
    /// <summary>
    /// Статус отчёта
    /// </summary>
    public ReportDeadlineStatus Status { get; set; }
}
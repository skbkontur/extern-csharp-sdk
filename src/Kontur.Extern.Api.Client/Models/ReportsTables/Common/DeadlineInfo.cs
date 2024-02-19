using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Common;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class DeadlineInfo
{
    /// <summary>
    /// Крайний срок подачи отчетности, дедлайн
    /// </summary>
    public DateTime Deadline { get; set; }
    /// <summary>
    /// Год периода подачи формы отчетности
    /// </summary>
    public int PeriodYear { get; set; }
    /// <summary>
    /// Номер периода подачи формы отчетности
    /// </summary>
    public int PeriodNumber { get; set; }
    /// <summary>
    /// Начальная дата периода, за который сдается отчет НДФЛ: Уведомление
    /// </summary>
    public DateTime? PeriodStart { get; set; }
    /// <summary>
    /// Конечная дата периода, за который сдается отчет НДФЛ: Уведомление
    /// </summary>
    public DateTime? PeriodEnd { get; set; }
    /// <summary>
    /// Статус формы отчетности
    /// </summary>
    public ReportDeadlineStatus Status { get; set; }
}
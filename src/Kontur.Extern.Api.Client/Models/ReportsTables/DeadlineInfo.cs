using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables;

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
    /// Статус формы отчетности
    /// </summary>
    public ReportDeadlineStatus Status { get; set; }
}
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
    /// Статус ячейки
    /// </summary>
    public ReportDeadlineStatus Status { get; set; }
}
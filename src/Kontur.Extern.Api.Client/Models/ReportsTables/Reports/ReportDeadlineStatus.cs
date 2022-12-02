using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public enum ReportDeadlineStatus
{
    /// <summary>
    /// Отчёт отклонён
    /// </summary>
    Declined,
    /// <summary>
    /// Дедлайн прошёл, отчёт не отправлен
    /// </summary>
    NotSentAfterDeadline,
    /// <summary>
    /// Отправлен после дедлайна
    /// </summary>
    SentAfterDeadline,
    /// <summary>
    /// На корректировку пришёл отказ
    /// </summary>
    AcceptedWithWarning,
    /// <summary>
    /// Не отправлен
    /// </summary>
    NotSent,
    /// <summary>
    /// Отправлен
    /// </summary>
    Sent,
    /// <summary>
    /// Принято после дедлайна
    /// </summary>
    AcceptedAfterDeadline,
    /// <summary>
    /// Отчёт принят, был отправлен новый
    /// </summary>
    AcceptedWithSent,
    /// <summary>
    /// Отмечен вручную как сданный
    /// </summary>
    AcceptedCustom,
    /// <summary>
    /// Отчёт принят в срок
    /// </summary>
    Accepted,
    /// <summary>
    /// Уведомление об уточнении пришло на отчёт
    /// </summary>
    AcceptedWithRebuke,
    /// <summary>
    /// Не нужно сдавать отчёт (отмечен вручную)
    /// </summary>
    NotRequiredReport,
}
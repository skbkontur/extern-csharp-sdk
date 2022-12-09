using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

[PublicAPI]
public enum ReportDocflowStatus
{
    /// <summary>
    /// Cтатус не был заполнен, произошел сбой
    /// </summary>
    Error = 0,
    /// <summary>
    /// Документооборот отклонён
    /// </summary>
    Declined = 1,
    /// <summary>
    /// Документооборот отправлен
    /// </summary>
    Sent = 2,
    /// <summary>
    /// Документооборот принят в срок
    /// </summary>
    Accepted = 3,
    /// <summary>
    /// Статус использовался для отчетов РПН до 2018 года 
    /// </summary>
    Editing = 4,
    /// <summary>
    /// Требуется корректировка
    /// </summary>
    AcceptedWithRebuke = 5,
}
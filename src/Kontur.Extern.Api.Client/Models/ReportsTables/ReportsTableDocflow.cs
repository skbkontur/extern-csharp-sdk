using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables;
[PublicAPI]
public class ReportsTableDocflow
{
    /// <summary>
    /// Идентификатор документооборота
    /// </summary>
    public Guid DocflowId { get; set; }
    /// <summary>
    /// Краткое имя организации
    /// </summary>
    public string OrganizationShortName { get; set; }
    /// <summary>
    /// Информация о форме отчетности
    /// </summary>
    public FormInfo Form { get; set; }
    /// <summary>
    /// Код инспекции
    /// </summary>
    [CanBeNull]
    public string Recipient { get; set; }
    /// <summary>
    /// Дата отправки документооборота
    /// </summary>
    public DateTime SendDate { get; set; }
    /// <summary>
    /// Номер корректировки
    /// </summary>
    public int? CorrectionNumber { get; set; }
    /// <summary>
    /// Статус документооборота
    /// </summary>
    public DocflowReportStatus ReportStatus { get; set; }
}
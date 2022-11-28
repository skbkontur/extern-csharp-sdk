using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables;
[PublicAPI]
public class ReportsTableDocflow
{
    /// <summary>
    /// Идентификатор ДО
    /// </summary>
    public Guid DocflowId { get; set; }
    /// <summary>
    /// Краткое имя организации
    /// </summary>
    public string OrganizationShortName { get; set; }
    /// <summary>
    /// Информация о форме
    /// </summary>
    public FormInfo Form { get; set; }
    /// <summary>
    /// Код инспекции
    /// </summary>
    public string Recipient { get; set; }
    /// <summary>
    /// Дата отправки ДО
    /// </summary>
    public DateTime SendDate { get; set; }
    /// <summary>
    /// Номер корректировки
    /// </summary>
    public int? CorrectionNumber { get; set; }
    /// <summary>
    /// Статус ДО
    /// </summary>
    public DocflowReportStatus ReportStatus { get; set; }
}
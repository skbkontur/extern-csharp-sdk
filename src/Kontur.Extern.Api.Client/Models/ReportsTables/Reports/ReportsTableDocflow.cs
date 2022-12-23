using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables.Forms;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Reports;
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
    public string OrganizationShortName { get; set; } = null!;
    /// <summary>
    /// Информация о форме отчетности
    /// </summary>
    public FormInfo Form { get; set; } = null!;
    /// <summary>
    /// Код инспекции
    /// </summary>
    public string? Recipient { get; set; }
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
    public ReportDocflowStatus ReportStatus { get; set; }
}
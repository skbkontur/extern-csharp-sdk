using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Forms;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class FormInfo
{
    /// <summary>
    /// Полное наименование формы
    /// </summary>
    public string FormFullName { get; set; }
    /// <summary>
    /// Краткое наименование формы
    /// </summary>
    public string FormShortName { get; set; }
    /// <summary>
    /// Идентификатор формы
    /// </summary>
    public int FormId { get; set; }
    /// <summary>
    /// КНД
    /// </summary>
    public string Knd { get; set; }
    /// <summary>
    /// ОКУД
    /// </summary>
    public string Okud { get; set; }
    /// <summary>
    /// Периодичность формы
    /// </summary>
    public PeriodEnum Periodicity { get; set; }
}
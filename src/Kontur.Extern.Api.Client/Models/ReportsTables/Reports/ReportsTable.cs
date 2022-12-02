using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class ReportsTable
{
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public Guid OrganizationId { get; set; }
    /// <summary>
    /// Список форм
    /// </summary>
    public ReportInfo[] Forms { get; set; }
}
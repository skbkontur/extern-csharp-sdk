using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.ReportsTables;

[PublicAPI]
public class SearchReportsRequest
{
    /// <summary>
    /// Список организаций для поиска
    /// </summary>
    public Guid[]? OrganizationIds { get; set; }
    /// <summary>
    /// Начало периода, за который нужно получить список отчетов. По умолчанию отдаём отчёты за последние 3 месяца.
    /// Возможный формат данных: "yyyy-mm-ddThh:mm:ss±hh:mm", "yyyy-mm-ddThh:mm:ssZ", "yyyy-mm-ddThh:mm:ss.fffffff±hh:mm", "yyyy-mm-ddThh:mm:ss.fffffffZ"
    /// </summary>
    public DateTime? DateFrom { get; set; }
    /// <summary>
    /// Конец периода, за который нужно получить список отчетов. По умолчанию отдаём отчёты за будущие 3 месяца.
    /// Возможный формат данных: "yyyy-mm-ddThh:mm:ss±hh:mm", "yyyy-mm-ddThh:mm:ssZ", "yyyy-mm-ddThh:mm:ss.fffffff±hh:mm", "yyyy-mm-ddThh:mm:ss.fffffffZ"
    /// </summary>
    public DateTime? DateTo { get; set; }
    /// <summary>
    /// Флаг отображения удаленных форм отчетности. (false/true)
    /// </summary>
    public bool? IncludeDeleted { get; set; }
    /// <summary>
    /// Количество организаций, которые были пропущены, значение по умолчанию - 0.
    /// </summary>
    public int? Skip { get; set; }
    /// <summary>
    /// Количество организаций, которые вернулись в запросе. По умолчанию - 1000, минимум - 0, максимум - 1000
    /// </summary>
    public int? Take { get; set; }
}
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.ReportsTables;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class SearchPaymentsRequest
{
    /// <summary>
    /// Список организаций для поиска
    /// </summary>
    public Guid[]? OrganizationIds { get; set; }
    /// <summary>
    /// Количество пропускаемых организаций . Значение по умолчанию: 0
    /// </summary>
    public int? Skip { get; set; }
    /// <summary>
    /// Количество возвращаемых организаций. Значение по умолчанию: 1000. Минимум: 0. Максимум: 1000
    /// </summary>
    public int? Take { get; set; }
    /// <summary>
    /// Начальная дата крайнего срока платежей. По умолчанию метод вернет данные за последние 3 месяца от текущей даты.
    /// </summary>
    public DateTime? DeadlineFrom { get; set; }
    /// <summary>
    /// Конечная дата крайнего срока платежей. По умолчанию метод вернет данные за следующие 3 месяца от текущей даты.
    /// </summary>
    public DateTime? DeadlineTo { get; set; }
}
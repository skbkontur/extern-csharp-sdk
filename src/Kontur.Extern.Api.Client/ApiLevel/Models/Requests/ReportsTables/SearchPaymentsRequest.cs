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
    /// Количество пропускаемых организаций, значение по умолчанию - 0.
    /// </summary>
    public int? Skip { get; set; }
    /// <summary>
    /// Количество возвращаемых организаций. По умолчанию - 1000, минимум - 0, максимум - 1000
    /// </summary>
    public int? Take { get; set; }
    /// <summary>
    /// Начальная дата крайнего срока платежей. По умолчанию на 3 месяца ранее текущий даты
    /// </summary>
    public DateTime? DeadlineFrom { get; set; }
    /// <summary>
    /// Конечная дата крайнего срока платежа. По умолчанию на 3 месяца позже текущий даты
    /// </summary>
    public DateTime? DeadlineTo { get; set; }
}
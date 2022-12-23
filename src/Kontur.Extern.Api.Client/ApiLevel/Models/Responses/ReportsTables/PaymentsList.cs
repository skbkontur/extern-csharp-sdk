using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables.Payments;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.ReportsTables;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class PaymentsList
{
    /// <summary>
    /// Количество организаций, которые будут пропущены. Значение по умолчанию: 0
    /// </summary>
    public int Skip { get; set; }
    /// <summary>
    /// Количество организаций, которые вернутся в запросе. Значение по умолчанию: 1000. Минимум: 0. Максимум: 1000
    /// </summary>
    public int Take { get; set; }
    /// <summary>
    /// Общее количество организаций
    /// </summary>
    public int TotalCount { get; set; }
    /// <summary>
    /// Список платежей по организациям
    /// </summary>
    public OrganizationPayments[] OrganizationPayments { get; set; }
}
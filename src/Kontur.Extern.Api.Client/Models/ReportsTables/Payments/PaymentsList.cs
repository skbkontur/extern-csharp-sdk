using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Payments;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class PaymentsList
{
    /// <summary>
    /// Количество организаций, которые будут пропущены. Значение по умолчанию: 0
    /// </summary>
    public int Skip;
    /// <summary>
    /// Количество организаций, которые вернутся в запросе. Значение по умолчанию: 1000. Минимум: 0. Максимум: 1000
    /// </summary>
    public int Take;
    /// <summary>
    /// Общее количество организаций
    /// </summary>
    public int TotalCount;
    /// <summary>
    /// Список платежей по организациям
    /// </summary>
    public OrganizationPayments[] OrganizationPayments;
}
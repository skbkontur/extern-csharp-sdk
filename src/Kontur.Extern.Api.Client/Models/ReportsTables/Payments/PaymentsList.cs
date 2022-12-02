using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Payments;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class PaymentsList
{
    /// <summary>
    /// Примененный пропуск по организациям
    /// </summary>
    public int Skip;
    /// <summary>
    /// Примененное взятие по организациям
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
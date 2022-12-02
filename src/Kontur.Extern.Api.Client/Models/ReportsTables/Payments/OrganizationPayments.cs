using System;
using System.Diagnostics.CodeAnalysis;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Payments;

[SuppressMessage("ReSharper", "CommentTypo")]
public class OrganizationPayments
{
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public Guid OrganizationId { get; set; }
    /// <summary>
    /// Платежи организации
    /// </summary>
    public PaymentInfo[] Payments { get; set; }
}
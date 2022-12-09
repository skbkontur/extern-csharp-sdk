using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Payments;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class OrganizationPayments
{
    /// <summary>
    /// Идентификатор организации
    /// </summary>
    public Guid OrganizationId { get; set; }
    /// <summary>
    /// Сведения о платежах организации
    /// </summary>
    public PaymentInfo[] Payments { get; set; }
}
using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Payments;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class PaymentPeriod
{
    /// <summary>
    /// Конечный срок оплаты платежа в конкретном периоде
    /// </summary>
    public DateTime Deadline { get; set; }
    /// <summary>
    /// Год периода платежа
    /// </summary>
    public int PeriodYear { get; set; }
    /// <summary>
    /// Тип периода, относительно которого определяется <see cref="PeriodNumber"/>: ежегодный, ежеквартальный, ежемесячный
    /// </summary>
    public PeriodEnum PeriodType { get; set; }
    /// <summary>
    /// Порядковый номер периода, в котором происходит платеж
    /// </summary>
    public int PeriodNumber { get; set; }
    /// <summary>
    /// Порядковый номер платежа внутри текущего периода. Для случаев, когда за один период происходит несколько платежей
    /// </summary>
    public int? PaymentNumberInPeriod { get; set; }
    
    // Для добавления в будущем
    // public dynamic Status { get; set; }
}
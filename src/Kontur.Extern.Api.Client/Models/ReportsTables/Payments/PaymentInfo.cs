using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Payments;

[PublicAPI]
public class PaymentInfo
{
    /// <summary>
    /// Наименование платежа
    /// </summary>
    public string FullName;
    /// <summary>
    /// Краткое наименование платежа
    /// </summary>
    public string ShortName;
    /// <summary>
    /// Id платежа (если платеж по форме, то id как у формы)
    /// </summary>
    public int PaymentFormId;
    /// <summary>
    /// Периодичность оплаты платежа
    /// </summary>
    public PeriodEnum Periodicity;
    /// <summary>
    /// Периоды, в которые происходят платежи
    /// </summary>
    public PaymentPeriod[] PaymentPeriods;
}
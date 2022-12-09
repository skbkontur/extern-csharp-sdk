using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

namespace Kontur.Extern.Api.Client.Models.ReportsTables.Payments;

[PublicAPI]
public class PaymentInfo
{
    /// <summary>
    /// Наименование платежа
    /// </summary>
    public string FullName { get; set; }
    /// <summary>
    /// Краткое наименование платежа
    /// </summary>
    public string ShortName { get; set; }
    /// <summary>
    /// Идентификатор платежа. Если платеж по форме отчетности, то идентификатор будет такой же, как у формы
    /// </summary>
    public int PaymentFormId { get; set; }
    /// <summary>
    /// Периодичность оплаты платежа: ежегодный, ежеквартальный, ежемесячный
    /// </summary>
    public PeriodEnum Periodicity { get; set; }
    /// <summary>
    /// Сведения о платежах: периоды поступлений, сроки уплаты
    /// </summary>
    public PaymentPeriod[] PaymentPeriods { get; set; }
}
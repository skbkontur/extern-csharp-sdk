using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public class FnsForm
{
    /// <summary>
    /// Код по КНД
    /// </summary>
    public string Knd { get; set; }

    /// <summary>
    /// Наименование КНД
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Версия формы
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Дата начала действия версии формы
    /// </summary>
    public string PeriodBegin { get; set; }

    /// <summary>
    /// Дата окончания действия версии формы
    /// </summary>
    public string PeriodEnd { get; set; }

    /// <summary>
    /// Описание к периоду действия версии формы
    /// </summary>
    public string DescriptionPeriod { get; set; }
}
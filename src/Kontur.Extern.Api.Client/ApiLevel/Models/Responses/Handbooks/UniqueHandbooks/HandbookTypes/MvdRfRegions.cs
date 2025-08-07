namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

/// <summary>
/// Трудовые уведомления. Регионы
/// </summary>
public class MvdRfRegions : HandbookItem
{
    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование региона
    /// </summary>
    public string Name { get; set; }
}
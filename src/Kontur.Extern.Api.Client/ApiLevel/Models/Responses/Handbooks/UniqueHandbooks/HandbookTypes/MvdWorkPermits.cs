namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

/// <summary>
/// Трудовые уведомления. Основание для работы
/// </summary>
public class MvdWorkPermits : HandbookItem
{
    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование основания для работы
    /// </summary>
    public string Name { get; set; }
}
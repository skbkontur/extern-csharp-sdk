namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

/// <summary>
/// Трудовые уведомления. Гражданство
/// </summary>
public class MvdCitizenship : HandbookItem
{
    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Наименование страны
    /// </summary>
    public string Name { get; set; }
}
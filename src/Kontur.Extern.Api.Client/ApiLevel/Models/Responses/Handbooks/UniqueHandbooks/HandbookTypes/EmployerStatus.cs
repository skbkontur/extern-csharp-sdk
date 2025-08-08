namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

/// <summary>
/// Трудовые уведомления. Статусы работодателя по договору 
/// </summary>
public class EmployerStatus : HandbookItem
{
    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование статуса работодателя
    /// </summary>
    public string Name { get; set; }
}
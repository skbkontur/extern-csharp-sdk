namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

/// <summary>
/// Трудовые уведомления. Документ трудовой миграции
/// </summary>
public class MvdLaborMigrationDocuments : HandbookItem
{
    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование документа трудовой миграции
    /// </summary>
    public string Name { get; set; }
}
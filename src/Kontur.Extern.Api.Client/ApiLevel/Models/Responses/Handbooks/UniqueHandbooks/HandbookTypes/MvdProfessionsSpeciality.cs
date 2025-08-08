namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

/// <summary>
/// Трудовые уведомления. Профессии. Виды деятельности. Специальности
/// </summary>
public class MvdProfessionsSpeciality : HandbookItem
{
    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование профессии/специальности
    /// </summary>
    public string Name { get; set; }
}
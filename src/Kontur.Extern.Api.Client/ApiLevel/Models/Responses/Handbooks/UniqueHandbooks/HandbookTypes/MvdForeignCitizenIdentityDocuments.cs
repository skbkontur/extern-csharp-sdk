namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

/// <summary>
/// Трудовые уведомления. Документ удостоверяющий личность иностранного гражданина
/// </summary>
public class MvdForeignCitizenIdentityDocuments : HandbookItem
{
    /// <summary>
    /// Код
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование документа удостоверяющего личность иностранного гражданина или лица без гражданства
    /// </summary>
    public string Name { get; set; }
}
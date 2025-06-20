namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

public class Okved : HandbookMvdItem
{
    /// <summary>
    /// Код
    /// </summary>
    public override string Code { get; set; }

    /// <summary>
    /// Наименование вида экономической деятельности
    /// </summary>
    public override string Name { get; set; }
}
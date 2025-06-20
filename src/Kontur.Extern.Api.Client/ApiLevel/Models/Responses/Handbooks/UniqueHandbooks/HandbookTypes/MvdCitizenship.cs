namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

public class MvdCitizenship : HandbookMvdItem
{
    /// <summary>
    /// Код
    /// </summary>
    public override string Code { get; set; }
    
    /// <summary>
    /// Наименование страны
    /// </summary>
    public override string Name { get; set; }
}
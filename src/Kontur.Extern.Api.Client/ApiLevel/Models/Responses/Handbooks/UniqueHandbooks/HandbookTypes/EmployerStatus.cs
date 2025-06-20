namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

public class EmployerStatus : HandbookMvdItem
{
    /// <summary>
    /// Код
    /// </summary>
    public override string Code { get; set; }

    /// <summary>
    /// Наименование статуса работодателя
    /// </summary>
    public override string Name { get; set; }
}
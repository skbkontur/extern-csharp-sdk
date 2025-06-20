namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

public class MvdWorkPermits : HandbookMvdItem
{
    /// <summary>
    /// Код
    /// </summary>
    public override string Code { get; set; }

    /// <summary>
    /// Наименование основания для работы
    /// </summary>
    public override string Name { get; set; }
}
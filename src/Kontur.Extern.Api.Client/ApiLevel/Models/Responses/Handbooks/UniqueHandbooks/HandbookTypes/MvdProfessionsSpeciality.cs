namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks.HandbookTypes;

public class MvdProfessionsSpeciality : HandbookMvdItem
{
    /// <summary>
    /// Код
    /// </summary>
    public override string Code { get; set; }

    /// <summary>
    /// Наименование профессии/специальности
    /// </summary>
    public override string Name { get; set; }
}
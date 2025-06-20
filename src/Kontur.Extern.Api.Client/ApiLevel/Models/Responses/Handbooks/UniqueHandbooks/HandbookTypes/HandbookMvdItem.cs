namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks;

public abstract class HandbookMvdItem : HandbookItem
{
    public abstract string Code { get; set; }
    public abstract string Name { get; set; }
}
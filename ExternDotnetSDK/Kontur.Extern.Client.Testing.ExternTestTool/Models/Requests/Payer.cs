// ReSharper disable CommentTypo
namespace Kontur.Extern.Client.Testing.ExternTestTool.Models.Requests
{
    /// <summary>
    /// Учетная запись налогоплательщика - организация, за которую отправляется отчетность. Payer и Sender могут совпадать.
    /// </summary>
    /// <param name="Inn">ИНН налогоплательщика (required).</param>
    /// <param name="Name">Название организации.</param>
    /// <param name="Kpp">КПП, специфично для ЮЛ.</param>
    /// <param name="ChiefFullName">ФИО руководителя.</param>
    public record Payer(string Inn, string? Name = null, string? Kpp = null, PersonFullName? ChiefFullName = null);
}
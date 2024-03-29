using Kontur.Extern.Api.Client.Http.Models;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.Models.Requests
{
    /// <summary>
    /// Организация, которая общается с налоговым органом (отправляет и получает документы). Payer и Sender могут совпадать.
    /// </summary>
    /// <param name="Inn">ИНН (required).</param>
    /// <param name="Kpp">КПП.</param>
    /// <param name="OrgName">Название организации.</param>
    /// <param name="Certificate">Сертификат, публичный ключ (required).</param>
    public record Sender(string Inn, Base64String? Certificate = null, string? Kpp = null, string? OrgName = null);
}
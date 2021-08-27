// ReSharper disable CommentTypo
namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool.Models.Requests
{
    /// <summary>
    /// Данные для генерации сертификата, если не заполнить, будут сгенерированы
    /// </summary>
    /// <param name="Inn">ИНН организации.</param>
    /// <param name="Kpp">КПП организации.</param>
    /// <param name="OrganizationName">Наименование организации.</param>
    /// <param name="FirstName">Имя владельца сертификата.</param>
    /// <param name="Surname">Фамилия владельца сертификата.</param>
    /// <param name="Patronymic">Отчество владельца сертификата.</param>
    /// <param name="Email">Адрес электронной почты владельца сертификата.</param>
    /// <param name="Encoding">Кодировка reg-файла</param>
    public record CertificateGenerationData
    (
        string? Inn = null,
        string? Kpp = null,
        string? OrganizationName = null,
        string? FirstName = null,
        string? Surname = null,
        string? Patronymic = null,
        string? Email = null,
        string? Encoding = "Unicode"
    );
}
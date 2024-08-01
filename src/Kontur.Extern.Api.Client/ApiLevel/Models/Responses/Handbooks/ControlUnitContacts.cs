using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public class ControlUnitContacts
{
    /// <summary>
    /// Короткое наименование
    /// </summary>
    public string ShortName { get; }
    /// <summary>
    /// Полное наименование
    /// </summary>
    public string FullName { get; }
    /// <summary>
    /// ИНН контролирующего органа
    /// </summary>
    public string Inn { get; }
    /// <summary>
    /// КПП контролирующего органа
    /// </summary>
    public string Kpp { get; }
    /// <summary>
    /// Юридический адрес
    /// </summary>
    public string Address { get; }
    /// <summary>
    /// Контактный телефон
    /// </summary>
    public string Phone { get; }
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; }

    public ControlUnitContacts(string shortName, string fullName, string inn, string kpp, string address, string phone, string email)
    {
        ShortName = shortName;
        FullName = fullName;
        Inn = inn;
        Kpp = kpp;
        Address = address;
        Phone = phone;
        Email = email;
    }
}
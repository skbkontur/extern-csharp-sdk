using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public class ControlUnitContacts
{
    /// <summary>
    /// Короткое наименование
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// Полное наименование
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// ИНН контролирующего органа
    /// </summary>
    public string Inn { get; set; }

    /// <summary>
    /// КПП контролирующего органа
    /// </summary>
    public string Kpp { get; set; }

    /// <summary>
    /// Юридический адрес
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Id отделения МВД
    /// </summary>
    public string MvdId { get; set; }
}
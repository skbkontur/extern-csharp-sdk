using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public class ControlUnit
{
    /// <summary>
    /// Тип контролирующего органа
    /// </summary>
    public ControlUnitType Type { get; set; }

    /// <summary>
    /// Код контролирующего органа
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование контролирующего органа
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Номер региона
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// Флаги особенностей работы контролирующего органа.
    /// Подробнее о значениях флагов в [документации](https://developer.kontur.ru/docs/extern-api/accounts/%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B%20API%20%D0%B4%D0%BB%D1%8F%20%D1%81%D0%BF%D1%80%D0%B0%D0%B2%D0%BE%D1%87%D0%BD%D0%B8%D0%BA%D0%BE%D0%B2.html)
    /// </summary>
    public ControlUnitFlags Flags { get; set; }

    /// <summary>
    /// Контакты
    /// </summary>
    public ControlUnitContacts Contacts { get; set; }

    /// <summary>
    /// Список сертификатов
    /// </summary>
    public ControlUnitCertificateInfo[] Certificates;
}
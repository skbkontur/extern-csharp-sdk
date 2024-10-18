using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;

[PublicAPI]
public class ControlUnitsFilter
{
    /// <summary>
    /// Тип контролирующего органа
    /// </summary>
    public ControlUnitType Type { get; set; }

    /// <summary>
    /// Номер региона
    /// </summary>
    public string Region { get; set; }

    /// <summary>
    /// Неотрицательное количество записей, которые нужно получить. Default = 100 (Если значение > 1000, оно понижается до 1000)
    /// </summary>
    public int Take { get; set; } = 100;

    /// <summary>
    /// Неотрицательное количество записей, которые нужно пропустить при считывании. Default = 0
    /// </summary>
    public int Skip { get; set; }

    /// <summary>
    /// Включать в результат неактивные контролирующие органы
    /// </summary>
    public bool IncludeInactive { get; set; }
}
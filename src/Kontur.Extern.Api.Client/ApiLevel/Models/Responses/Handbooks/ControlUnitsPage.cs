using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public class ControlUnitsPage
{
    /// <summary>
    /// Количество записей, которые были пропущены при считывании
    /// </summary>
    public int Skip { get; set; }
    /// <summary>
    /// Количество записей, которые вернулись в запросе
    /// </summary>
    public int Take { get; set; }
    /// <summary>
    /// Общее количество найденных записей
    /// </summary>
    public int TotalCount { get; set; }
    /// <summary>
    /// Список контролирующих органов
    /// </summary>
    public ControlUnitsPageItem[] ControlUnits { get; set; }
}
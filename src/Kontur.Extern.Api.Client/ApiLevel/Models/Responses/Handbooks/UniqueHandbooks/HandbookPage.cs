
namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks.UniqueHandbooks;

public class HandbookPage
{
    /// <summary>
    /// Тип справочника
    /// </summary>
    public HandbookType HandbookType { get; set; }

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
    /// Справочник
    /// </summary>
    public HandbookItem[] Handbook { get; set; }
}
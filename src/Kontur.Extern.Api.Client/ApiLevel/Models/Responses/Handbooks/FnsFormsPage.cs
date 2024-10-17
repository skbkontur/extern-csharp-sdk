using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Handbooks;

[PublicAPI]
public class FnsFormsPage
{
    /// <summary>
    /// Количество записей, которые были пропущены при считывании
    /// </summary>
    public long Skip { get; set; }

    /// <summary>
    /// Количество записей, которые вернулись в запросе
    /// </summary>
    public long Take { get; set; }

    /// <summary>
    /// Общее количество найденных записей
    /// </summary>
    public long TotalCount { get; set; }

    /// <summary>
    /// Список КНД
    /// </summary>
    public FnsFormPageItem[] FnsForms { get; set; }
}
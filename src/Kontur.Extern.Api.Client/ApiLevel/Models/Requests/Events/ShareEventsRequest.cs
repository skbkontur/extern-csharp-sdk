using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Events;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class ShareEventsRequest
{
    /// <summary>
    /// Подписчик ленты событий
    /// </summary>
    public string Subscriber { get; set; } = null!;

    /// <summary>
    /// Информация о событиях, доступных подписчику
    /// </summary>
    public EventsFilter? EventsFilter { get; set; }
}
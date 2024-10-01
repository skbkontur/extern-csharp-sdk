using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Events;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class EventsDocumentForm
{
    /// <summary>
    /// КНД
    /// </summary>
    public string? Knd { get; set; }

    /// <summary>
    /// ОКУД
    /// </summary>
    public string? Okud { get; set; }
}
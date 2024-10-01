using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Events;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public class EventsFilter
{
    /// <summary>
    /// Типы доступных документооборотов
    /// </summary>
    public List<Urn>? DocflowTypes { get; set; }

    /// <summary>
    /// Типы доступных документов
    /// </summary>
    public List<EventsDocumentForm>? DocumentForms { get; set; }
}
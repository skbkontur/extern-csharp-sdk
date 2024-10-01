using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Events;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client;

[PublicAPI]
public static class EventsListPathExtensions
{
    public static Task<EventsPage> GetEventsAsync(this in EventsListPath path, int take, string fromId = "0_0", TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        return apiClient.Events.GetEventsAsync(take, fromId, timeout);
    }
}
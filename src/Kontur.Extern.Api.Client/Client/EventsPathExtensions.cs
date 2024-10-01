using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Events;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client;

[PublicAPI]
public static class EventsPathExtensions
{
    public static Task ShareEventsAsync(this in EventsPath path, ShareEventsRequest shareEventsRequest, TimeSpan? timeout = null)
    {
        var apiClient = path.Services.Api;
        return apiClient.Events.ShareEventsAsync(path.AccountId, shareEventsRequest, timeout);
    }
}
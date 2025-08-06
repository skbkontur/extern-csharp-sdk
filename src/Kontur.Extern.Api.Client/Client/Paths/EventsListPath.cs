using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Events;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ApiPathSection]
public readonly struct EventsListPath
{
    public EventsListPath(IExternClientServices services) => Services = services ?? throw new ArgumentNullException(nameof(services));

    public IExternClientServices Services { get; }

    public EventsPath WithId(Guid accountId) => new(accountId, Services);

    public Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0", TimeSpan? timeout = null)
    {
        var apiClient = Services.Api;
        return apiClient.Events.GetEventsAsync(take, fromId, timeout);
    }
}
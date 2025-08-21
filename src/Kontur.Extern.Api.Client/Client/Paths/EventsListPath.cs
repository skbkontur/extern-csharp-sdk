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
    public EventsListPath(IExternClientServices services) => this.services = services ?? throw new ArgumentNullException(nameof(services));

    private readonly IExternClientServices services;

    #region ObsoleteCode
    [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
    public IExternClientServices Services => services;
    #endregion

    public Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0", TimeSpan? timeout = null)
    {
        var apiClient = services.Api;
        return apiClient.Events.GetEventsAsync(take, fromId, timeout);
    }
}
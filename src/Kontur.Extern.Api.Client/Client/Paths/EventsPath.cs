using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Events;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ApiPathSection]
public readonly struct EventsPath
{
    public EventsPath(Guid accountId, IExternClientServices services)
    {
        AccountId = accountId;
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public Guid AccountId { get; }
    public IExternClientServices Services { get; }

    public Task ShareEventsAsync(ShareEventsRequest shareEventsRequest, TimeSpan? timeout = null)
    {
        var apiClient = Services.Api;
        return apiClient.Events.ShareEventsAsync(AccountId, shareEventsRequest, timeout);
    }
}
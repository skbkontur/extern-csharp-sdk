using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

public readonly struct EventsListPath
{
    public EventsListPath(IExternClientServices services) => Services = services ?? throw new ArgumentNullException(nameof(services));

    public IExternClientServices Services { get; }

    public AccountPath WithId(Guid accountId) => new(accountId, Services);
}
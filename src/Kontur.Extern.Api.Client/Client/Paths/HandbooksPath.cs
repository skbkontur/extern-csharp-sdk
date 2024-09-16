using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

public readonly struct HandbooksPath
{
    public HandbooksPath(Guid accountId, IExternClientServices services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
        AccountId = accountId;
    }

    public Guid AccountId { get; }
    public IExternClientServices Services { get; }
}
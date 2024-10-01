using System;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

public readonly struct HandbooksPath
{
    public HandbooksPath(IExternClientServices services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public IExternClientServices Services { get; }
}
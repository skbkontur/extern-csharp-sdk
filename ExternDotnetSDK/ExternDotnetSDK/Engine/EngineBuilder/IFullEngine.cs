using System;

namespace Kontur.Extern.Client.Engine.EngineBuilder
{
    public interface IFullEngine
    {
        IFullEngine WithDefaultTimeout(TimeSpan? timeout);
        ExternEngine Build();
    }
}
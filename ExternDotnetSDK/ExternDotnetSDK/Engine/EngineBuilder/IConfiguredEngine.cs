using Kontur.Extern.Client.Clients.Common.Logging;

namespace Kontur.Extern.Client.Engine.EngineBuilder
{
    public interface IConfiguredEngine
    {
        ILoggedEngine WithLog(ILogger log);
    }
}
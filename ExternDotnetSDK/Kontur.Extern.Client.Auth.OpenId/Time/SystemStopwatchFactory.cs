using System.Diagnostics;

namespace Kontur.Extern.Client.Auth.OpenId.Time
{
    internal class SystemStopwatchFactory : IStopwatchFactory
    {
        public IStopwatch Start() => new SystemStopwatch(Stopwatch.StartNew());
    }
}
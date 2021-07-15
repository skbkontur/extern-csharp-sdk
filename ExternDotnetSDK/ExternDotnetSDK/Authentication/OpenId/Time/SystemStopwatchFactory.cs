using System.Diagnostics;

namespace Kontur.Extern.Client.Authentication.OpenId.Time
{
    internal class SystemStopwatchFactory : IStopwatchFactory
    {
        public IStopwatch Start() => new SystemStopwatch(Stopwatch.StartNew());
    }
}
using System.Diagnostics;

namespace Kontur.Extern.Client.Common.Time
{
    public class SystemStopwatchFactory : IStopwatchFactory
    {
        public IStopwatch Start() => new SystemStopwatch(Stopwatch.StartNew());
    }
}
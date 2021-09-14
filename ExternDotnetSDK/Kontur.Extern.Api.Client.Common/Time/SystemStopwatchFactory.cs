using System.Diagnostics;

namespace Kontur.Extern.Api.Client.Common.Time
{
    public class SystemStopwatchFactory : IStopwatchFactory
    {
        public IStopwatch Start() => new SystemStopwatch(Stopwatch.StartNew());
    }
}
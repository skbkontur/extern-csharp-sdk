using System.Diagnostics;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Time
{
    internal class SystemStopwatch : IStopwatch
    {
        private readonly Stopwatch stopwatch;

        public SystemStopwatch(Stopwatch stopwatch)
        {
            if (!stopwatch.IsRunning)
                throw Errors.StopwatchHaveToBeRunning(nameof(stopwatch));
            
            this.stopwatch = stopwatch;
        }

        public TimeInterval Elapsed => stopwatch.Elapsed;
    }
}
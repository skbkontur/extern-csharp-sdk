using System.Diagnostics;
using Kontur.Extern.Client.Common.Exceptions;

namespace Kontur.Extern.Client.Common.Time
{
    public class SystemStopwatch : IStopwatch
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
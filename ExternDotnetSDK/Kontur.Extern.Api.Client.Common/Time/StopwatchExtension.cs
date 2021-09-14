using System;

namespace Kontur.Extern.Api.Client.Common.Time
{
    public static class StopwatchExtension
    {
        public static bool HasPassed(this IStopwatch stopwatch, TimeInterval amount) =>
            amount - stopwatch.Elapsed /*- stopwatch.Precision*/ <= TimeSpan.Zero;
    }
}
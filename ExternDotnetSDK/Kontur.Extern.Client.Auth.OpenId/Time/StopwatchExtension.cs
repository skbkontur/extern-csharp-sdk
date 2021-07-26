using System;

namespace Kontur.Extern.Client.Auth.OpenId.Time
{
    internal static class StopwatchExtension
    {
        public static bool HasPassed(this IStopwatch stopwatch, TimeInterval amount) =>
            amount - stopwatch.Elapsed /*- stopwatch.Precision*/ <= TimeSpan.Zero;
    }
}
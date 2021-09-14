using Kontur.Extern.Api.Client.Common.Exceptions;

namespace Kontur.Extern.Api.Client.Common.Time
{
    public class TimeToLive : ITimeToLive
    {
        public static bool TryCreateActive(TimeInterval timeInterval, IStopwatch stopwatch, out TimeToLive timeToLive)
        {
            if (timeInterval <= stopwatch.Elapsed)
            {
                timeToLive = default!;
                return false;
            }

            timeToLive = new TimeToLive(timeInterval, stopwatch);
            return true;
        }

        private readonly TimeInterval amount;
        private readonly IStopwatch stopwatch;

        public TimeToLive(TimeInterval amount, IStopwatch stopwatch)
        {
            if (amount == default)
                throw Errors.TimeIntervalShouldBeNonEmpty(nameof(amount));
            
            this.amount = amount;
            this.stopwatch = stopwatch;
        }
        
        public bool HasExpired => stopwatch.HasPassed(amount);
        public TimeInterval Remaining => amount - stopwatch.Elapsed;

        public bool WillExpireAfter(TimeInterval interval) => stopwatch.HasPassed(amount - interval);
    }
}
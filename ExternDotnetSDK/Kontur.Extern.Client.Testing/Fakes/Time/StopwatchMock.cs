using System.Collections.Generic;
using System.Linq;
using Kontur.Extern.Client.Auth.OpenId.Time;
using NSubstitute;

namespace Kontur.Extern.Client.Testing.Fakes.Time
{
    internal class StopwatchMock
    {
        private readonly TimeInterval proactiveAuthTokenRefreshInterval;
        private readonly List<IStopwatch> stopwatches;
            
        public StopwatchMock(TimeInterval proactiveAuthTokenRefreshInterval)
        {
            this.proactiveAuthTokenRefreshInterval = proactiveAuthTokenRefreshInterval;
            stopwatches = new List<IStopwatch> {Substitute.For<IStopwatch>()};
                
            StopwatchFactory = Substitute.For<IStopwatchFactory>();
            StopwatchFactory.Start().Returns(_ =>
            {
                var lastStopwatch = stopwatches.Last();
                stopwatches.Add(Substitute.For<IStopwatch>());
                return lastStopwatch;
            });
        }
        public IStopwatchFactory StopwatchFactory { get; }

        public void UpcomingStopwatchAdvancedTo(TimeInterval interval) => 
            UpcomingStopwatch.Elapsed.Returns(interval);

        public void ActiveStopwatchAdvancedToRefreshTokenTimeWhenActiveTokenTTLIs(TimeInterval activeTokenExpiresIn) => 
            ActiveStopwatchAdvancedTo(activeTokenExpiresIn - proactiveAuthTokenRefreshInterval);

        public void ActiveStopwatchAdvancedTo(TimeInterval timeInterval) => 
            ActiveStopwatch.Elapsed.Returns(timeInterval);

        private IStopwatch UpcomingStopwatch => stopwatches.Last();
        private IStopwatch ActiveStopwatch => stopwatches.Count > 1 ? stopwatches[^2] : stopwatches[^1];
    }
}
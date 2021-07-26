using FluentAssertions;
using Kontur.Extern.Client.Auth.OpenId.Time;
using Xunit;

namespace Kontur.Extern.Client.Auth.OpenId.UnitTests.Time
{
    public class SystemStopwatchFactory_Tests
    {
        [Fact]
        public void Should_create_a_running_stopwatch()
        {
            var stopwatchFactory = new SystemStopwatchFactory();

            var stopwatch = stopwatchFactory.Start();

            stopwatch.Should().BeOfType<SystemStopwatch>();
        }
    }
}
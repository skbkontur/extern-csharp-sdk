using FluentAssertions;
using Kontur.Extern.Client.Common.Time;
using Xunit;

namespace Kontur.Extern.Client.Common.UnitTests.Time
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
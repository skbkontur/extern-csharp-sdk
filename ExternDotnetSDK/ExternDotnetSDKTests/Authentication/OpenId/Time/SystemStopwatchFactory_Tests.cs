using FluentAssertions;
using Kontur.Extern.Client.Auth.OpenId.Time;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.Authentication.OpenId.Time
{
    [TestFixture]
    internal class SystemStopwatchFactory_Tests
    {
        [Test]
        public void Should_create_a_running_stopwatch()
        {
            var stopwatchFactory = new SystemStopwatchFactory();

            var stopwatch = stopwatchFactory.Start();

            stopwatch.Should().BeOfType<SystemStopwatch>();
        }
    }
}
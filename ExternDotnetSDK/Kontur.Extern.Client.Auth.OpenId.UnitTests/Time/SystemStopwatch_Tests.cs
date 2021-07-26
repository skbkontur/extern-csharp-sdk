using System;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using Kontur.Extern.Client.Auth.OpenId.Time;
using Vostok.Commons.Time;
using Xunit;

namespace Kontur.Extern.Client.Auth.OpenId.UnitTests.Time
{
    public static class SystemStopwatch_Tests
    {
        public class Ctor
        {
            [Fact]
            public void Should_fail_when_create_with_inactive_stopwatch()
            {
                Action action = () => _ = new SystemStopwatch(new Stopwatch());

                action.Should().Throw<ArgumentException>();
            }
        }

        public class HasPassed
        {
            [Fact]
            public void Should_indicate_that_elapsed_time_is_less_than_in_the_given_interval()
            {
                var systemStopwatch = new SystemStopwatch(Stopwatch.StartNew());

                var hasPassed = systemStopwatch.HasPassed(1.Hours());

                hasPassed.Should().BeFalse();
            }
            
            [Fact]
            public void Should_indicate_that_elapsed_time_is_more_or_equal_than_in_the_given_interval()
            {
                var systemStopwatch = new SystemStopwatch(Stopwatch.StartNew());
                Thread.Sleep(1.Seconds());

                var hasPassed = systemStopwatch.HasPassed(1.Seconds());

                hasPassed.Should().BeTrue();
            }

            [Fact]
            public void Should_always_return_true_if_the_given_interval_is_empty()
            {
                var systemStopwatch = new SystemStopwatch(Stopwatch.StartNew());
                systemStopwatch.HasPassed(TimeSpan.Zero).Should().BeTrue();
                
                Thread.Sleep(1.Seconds());
                
                systemStopwatch.HasPassed(TimeSpan.Zero).Should().BeTrue();
            }

            [Fact]
            public void Should_fail_when_the_given_interval_is_negative()
            {
                var systemStopwatch = new SystemStopwatch(Stopwatch.StartNew());
                systemStopwatch.HasPassed(TimeSpan.Zero).Should().BeTrue();
                
                Thread.Sleep(1.Seconds());
                
                systemStopwatch.HasPassed(TimeSpan.Zero).Should().BeTrue();
            }
        }
    }
}
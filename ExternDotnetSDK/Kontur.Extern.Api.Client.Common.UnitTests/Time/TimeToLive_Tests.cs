using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using Kontur.Extern.Api.Client.Common.Time;
using NSubstitute;
using Xunit;

namespace Kontur.Extern.Api.Client.Common.UnitTests.Time
{
    public static class TimeToLive_Tests
    {
        public class Ctor
        {
            [Fact]
            public void Should_fail_when_given_the_empty_interval()
            {
                Action action = () => _ = new TimeToLive(default, Substitute.For<IStopwatch>());

                action.Should().Throw<ArgumentException>();
            }
        }

        public class HasPassed
        {
            [Fact]
            public void Should_indicate_that_TTL_has_not_been_passed_yet()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                var timeToLive = new TimeToLive(1.Seconds(), stopwatch);

                timeToLive.HasExpired.Should().BeFalse();
            }
            
            [Fact]
            public void Should_indicate_that_TTL_has_been_passed()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(11.Seconds());
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.HasExpired.Should().BeTrue();
            }
        }

        public class WillExpireAfter
        {
            [Fact]
            public void Should_indicate_that_TTL_will_not_pass_after_specified_interval()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(1.Seconds());
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.WillExpireAfter(8.Seconds()).Should().BeFalse();
            }
            
            [Fact]
            public void Should_indicate_that_TTL_will_pass_after_specified_interval()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(1.Seconds());
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.WillExpireAfter(9.Seconds()).Should().BeTrue();
            }
            
            [Fact]
            public void Should_indicate_that_TTL_will_pass_after_the_given_interval_when_the_time_is_not_elapsed_yet()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.WillExpireAfter(10.Seconds()).Should().BeTrue();
            }
            
            [Fact]
            public void Should_indicate_that_TTL_will_pass_after_the_given_interval_when_elapsed_only_part_of_the_TTL()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(1.Seconds());
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.WillExpireAfter(10.Seconds()).Should().BeTrue();
            }
        }

        public class TryCreateActive
        {
            [Fact]
            public void Should_successfully_create_TTL_when_it_is_not_expired()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(0.5.Seconds());
                
                var success = TimeToLive.TryCreateActive(1.Seconds(), stopwatch, out var timeToLive);

                success.Should().BeTrue();
                timeToLive.HasExpired.Should().BeFalse();
            }

            [Fact]
            public void Should_return_error_when_the_given_interval_has_been_passed_already()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(1.Seconds());
                
                var success = TimeToLive.TryCreateActive(1.Seconds(), stopwatch, out var timeToLive);

                success.Should().BeFalse();
                timeToLive.Should().BeNull();
            }
        }

        public class Remaining
        {
            [Fact]
            public void Should_return_whole_TTL_time_if_the_time_has_not_advanced_yet()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.Remaining.Should().Be(new TimeInterval(10.Seconds()));
            }
            
            [Fact]
            public void Should_return_remaining_time_of_the_TTL()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(3.Seconds());
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.Remaining.Should().Be(new TimeInterval(7.Seconds()));
            }

            [Fact]
            public void Should_return_zero_if_the_time_advanced_more_then_the_TTL()
            {
                var stopwatch = Substitute.For<IStopwatch>();
                stopwatch.Elapsed.Returns(11.Seconds());
                var timeToLive = new TimeToLive(10.Seconds(), stopwatch);

                timeToLive.Remaining.Should().Be(new TimeInterval());
            }
        }
    }
}
using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using Kontur.Extern.Client.Common.Time;
using Kontur.Extern.Client.Common.UnitTests.TestAssertions;
using Xunit;

namespace Kontur.Extern.Client.Common.UnitTests.Time
{
    public static class TimeInterval_Tests
    {
        public class Ctor
        {
            [Fact]
            public void Should_fail_if_created_with_a_negative_time_span()
            {
                Action action = () => _ = new TimeInterval((-1).Seconds());

                action.Should().Throw<ArgumentException>();
            }
        }

        public class Unwrap
        {
            [Fact]
            public void Should_expose_the_given_time_span()
            {
                var givenTimeSpan = 101.Seconds();
                var timeInterval = new TimeInterval(givenTimeSpan);

                var timeSpan = timeInterval.Unwrap();

                timeSpan.Should().Be(givenTimeSpan);
            }

            [Fact]
            public void Should_expose_zero_time_span_for_default_instance()
            {
                TimeInterval interval = default;

                var timeSpan = interval.Unwrap();

                timeSpan.Should().Be(TimeSpan.Zero);
            }
        }

        public class Formatting
        {
            [Fact]
            public void Should_format_the_wrapped_time_span()
            {
                var timeSpan = 101.Seconds();
                var timeInterval = new TimeInterval(timeSpan);

                var formattedValue = timeInterval.ToString();

                formattedValue.Should().Be(timeSpan.ToString());
            }

            [Fact]
            public void Should_format_default_interval_as_zero_interval()
            {
                TimeInterval defaultInstance = default;

                defaultInstance.ToString().Should().Be("00:00:00");
            }
        }

        public class EqualityComparison
        {
            [Fact]
            public void Should_be_equality_comparable_with_another_time_interval()
            {
                TimeInterval interval = 101.Seconds();
                TimeInterval equal = 101.Seconds();
                TimeInterval notEqual = 100.Seconds();

                interval.Should().BeEqualAndHaveSameHashCode(equal);
                interval.Should().NotBeEqualAndHaveDifferentHashCodes(notEqual);
            }
            
            [Fact]
            public void Should_be_equality_comparable_with_default_interval()
            {
                TimeInterval defaultInstance = default;
                TimeInterval equal = TimeSpan.Zero;
                TimeInterval notEqual = 100.Seconds();
                
                defaultInstance.Should().BeEqualAndHaveSameHashCode(equal);
                defaultInstance.Should().NotBeEqualAndHaveDifferentHashCodes(notEqual);
            }
        }

        public class InteroperabilityWithTimeSpans
        {
            [Fact]
            public void Should_convert_from_time_span_implicitly()
            {
                var givenTimeSpan = 101.Seconds();
                TimeInterval timeInterval = givenTimeSpan;

                var timeSpan = timeInterval.Unwrap();

                timeSpan.Should().Be(givenTimeSpan);
            }

            [Fact]
            public void Should_subtract_a_timespan_from_the_interval()
            {
                TimeInterval expectedInterval = 100.Seconds();
                TimeInterval timeInterval = 101.Seconds();
                
                var resultInterval = timeInterval - 1.Seconds();

                resultInterval.Should().Be(expectedInterval);
            }

            [Fact]
            public void Should_return_empty_interval_when_subtract_a_timespan_greater_than_the_interval()
            {
                TimeInterval expectedInterval = default;
                TimeInterval timeInterval = 101.Seconds();
                
                var resultInterval = timeInterval - 110.Seconds();

                resultInterval.Should().Be(expectedInterval);
            }
        }
    }
}
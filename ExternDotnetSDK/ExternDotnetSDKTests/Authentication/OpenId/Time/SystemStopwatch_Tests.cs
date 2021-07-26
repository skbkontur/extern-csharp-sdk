using System;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using Kontur.Extern.Client.Auth.OpenId.Time;
using NUnit.Framework;
using Vostok.Commons.Time;

namespace Kontur.Extern.Client.Tests.Authentication.OpenId.Time
{
    [TestFixture]
    internal class SystemStopwatch_Tests
    {
        [TestFixture]
        internal class Ctor
        {
            [Test]
            public void Should_fail_when_create_with_inactive_stopwatch()
            {
                Action action = () => _ = new SystemStopwatch(new Stopwatch());

                action.Should().Throw<ArgumentException>();
            }
        }

        [TestFixture]
        internal class HasPassed
        {
            [Test]
            public void Should_indicate_that_elapsed_time_is_less_than_in_the_given_interval()
            {
                var systemStopwatch = new SystemStopwatch(Stopwatch.StartNew());

                var hasPassed = systemStopwatch.HasPassed(1.Hours());

                hasPassed.Should().BeFalse();
            }
            
            [Test]
            public void Should_indicate_that_elapsed_time_is_more_or_equal_than_in_the_given_interval()
            {
                var systemStopwatch = new SystemStopwatch(Stopwatch.StartNew());
                Thread.Sleep(1.Seconds());

                var hasPassed = systemStopwatch.HasPassed(1.Seconds());

                hasPassed.Should().BeTrue();
            }

            [Test]
            public void Should_always_return_true_if_the_given_interval_is_empty()
            {
                var systemStopwatch = new SystemStopwatch(Stopwatch.StartNew());
                systemStopwatch.HasPassed(TimeSpan.Zero).Should().BeTrue();
                
                Thread.Sleep(1.Seconds());
                
                systemStopwatch.HasPassed(TimeSpan.Zero).Should().BeTrue();
            }

            [Test]
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
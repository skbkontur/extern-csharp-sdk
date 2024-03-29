using System;
using FluentAssertions;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.Common.Time;
using NSubstitute;
using Vostok.Commons.Time;
using Xunit;

namespace Kontur.Extern.Api.Client.Auth.OpenId.UnitTests.Provider.Models
{
    public static class AccessToken_Tests
    {
        public class Ctor
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void Should_fail_when_given_the_invalid_access_token(string invalidToken)
            {
                ShouldFail(invalidToken, "123", AliveTTL);
            }
            
            [Theory]
            [InlineData("")]
            [InlineData(" ")]
            public void Should_fail_when_given_the_invalid_refresh_token(string invalidToken)
            {
                ShouldFail("123", invalidToken, AliveTTL);
            }
            
            [Fact]
            public void Should_fail_when_given_the_null_TTL()
            {
                ShouldFail("123", "123", null!);
            }

            [Fact]
            public void Should_fail_when_the_given_TTL_has_been_already_expired()
            {
                ShouldFail("123", "123", ExpiredTTL);
            }

            [Fact]
            public void Should_initialize_with_given_tokens()
            {
                var accessToken = new AccessToken("123", "1234", AliveTTL);

                accessToken.TryGetRefreshToken(out var refreshToken).Should().BeTrue();
                refreshToken.Should().Be("1234");
                accessToken.ToString().Should().Be("123");
            }

            [Fact]
            public void Should_initialize_without_refresh_token_tokens()
            {
                var accessToken = new AccessToken("123", null, AliveTTL);

                accessToken.TryGetRefreshToken(out var refreshToken).Should().BeFalse();
                refreshToken.Should().BeNull();
                accessToken.ToString().Should().Be("123");
            }

            private ITimeToLive AliveTTL => new TimeToLive(10.Seconds(), Substitute.For<IStopwatch>());
            
            private ITimeToLive ExpiredTTL
            {
                get
                {
                    var stopwatch = Substitute.For<IStopwatch>();
                    stopwatch.Elapsed.Returns(10.Seconds());
                    return new TimeToLive(10.Seconds(), stopwatch);
                }
            }

            private static void ShouldFail(string accessToken, string refreshToken, ITimeToLive timeToLive)
            {
                Action action = () => _ = new AccessToken(accessToken, refreshToken, timeToLive);

                action.Should().Throw<ArgumentException>();
            }
        }
    }
}
#nullable enable
using System;
using FluentAssertions;
using Kontur.Extern.Client.Auth.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Client.Auth.OpenId.Time;
using NSubstitute;
using Vostok.Commons.Time;
using Xunit;

namespace Kontur.Extern.Client.Auth.OpenId.UnitTests.Provider.Models
{
    public static class AccessTokenFactory_Tests
    {
        public class Ctor
        {
            [Fact]
            public void Should_start_a_new_stopwatch()
            {
                var stopwatchFactory = Substitute.For<IStopwatchFactory>();
                
                _ = new AccessTokenFactory(stopwatchFactory);

                stopwatchFactory.Received(1).Start();
            }
        }
        
        public class CreateAccessToken
        {
            private readonly IStopwatchFactory stopwatchFactory;
            private readonly IStopwatch stopwatch;

            public CreateAccessToken()
            {
                stopwatchFactory = Substitute.For<IStopwatchFactory>();
                stopwatch = Substitute.For<IStopwatch>();
                stopwatchFactory.Start().Returns(stopwatch);
            }
            
            [Fact]
            public void Should_return_a_new_access_token_if_the_response_has_not_expired_yet()
            {
                var factory = new AccessTokenFactory(stopwatchFactory);
                stopwatch.Elapsed.Returns(1.Seconds());
                
                var accessToken = factory.CreateAccessToken(CreateTokenResponse(2));

                accessToken.Should().NotBeNull();
                accessToken!.HasNotExpired.Should().BeTrue();
            }
            
            [Fact]
            public void Should_initialize_token_with_values_from_the_response_has_not_expired_yet()
            {
                var factory = new AccessTokenFactory(stopwatchFactory);
                stopwatch.Elapsed.Returns(1.Seconds());
                
                var accessToken = factory.CreateAccessToken(CreateTokenResponse(2, "access-token", "refresh_token"));

                accessToken.Should().NotBeNull();
                accessToken!.ToString().Should().Be("access-token");
                accessToken.TryGetRefreshToken(out var refreshToken).Should().BeTrue();
                refreshToken.Should().Be("refresh_token");
            }
            
            [Fact]
            public void Should_initialize_token_with_values_from_the_response_has_not_expired_yet_but_without_refresh_token()
            {
                var factory = new AccessTokenFactory(stopwatchFactory);
                stopwatch.Elapsed.Returns(1.Seconds());
                
                var accessToken = factory.CreateAccessToken(CreateTokenResponse(2, "access-token", null));

                accessToken.Should().NotBeNull();
                accessToken!.ToString().Should().Be("access-token");
                accessToken.TryGetRefreshToken(out _).Should().BeFalse();
            }
            
            [Fact]
            public void Should_return_null_access_token_if_the_response_has_already_expired()
            {
                var factory = new AccessTokenFactory(stopwatchFactory);
                stopwatch.Elapsed.Returns(1.Seconds());

                Action action = () => factory.CreateAccessToken(CreateTokenResponse(1));

                action.Should().Throw<OpenIdException>();
            }

            private static TokenResponse CreateTokenResponse(int expiresInSeconds, string accessToken = "123", string? refreshToken = "123") => new()
            {
                ExpiresInSeconds = expiresInSeconds, 
                AccessToken = accessToken, 
                RefreshToken = refreshToken
            };
        }
    }
}
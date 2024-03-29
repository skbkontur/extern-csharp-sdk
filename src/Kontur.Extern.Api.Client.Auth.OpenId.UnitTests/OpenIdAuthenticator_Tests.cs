using System;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.AuthStrategies;
using Kontur.Extern.Api.Client.Auth.OpenId.Client;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Api.Client.Testing.Fakes.Time;
using NSubstitute;
using Vostok.Commons.Time;
using Xunit;

namespace Kontur.Extern.Api.Client.Auth.OpenId.UnitTests
{
    public class OpenIdAuthenticator_Tests
    {
        private readonly OpenIdAuthenticator authenticator;
        private readonly AuthenticationStrategyMock authStrategyMock;
        private readonly OpenIdClientMock openIdMock;
        private readonly StopwatchMock stopwatchMock;

        public OpenIdAuthenticator_Tests()
        {
            var proactiveAuthTokenRefreshInterval = 5.Seconds();
            var options = new OpenIdAuthenticationOptions("123", "client_id", proactiveAuthTokenRefreshInterval);
            openIdMock = new OpenIdClientMock();

            authStrategyMock = new AuthenticationStrategyMock();
            stopwatchMock = new StopwatchMock(proactiveAuthTokenRefreshInterval);

            authenticator = new OpenIdAuthenticator(options, openIdMock.Instance, authStrategyMock.Instance, stopwatchMock.StopwatchFactory);
        }

        [Fact]
        public async Task Should_authenticate_for_the_first_time()
        {
            await authenticator.AuthenticateAsync();

            authStrategyMock.ReceivedAuthenticateOnce();
        }

        [Fact]
        public async Task Should_return_auth_result_with_apply_open_id_to_request()
        {
            var authenticationResult = await authenticator.AuthenticateAsync();

            authenticationResult.Should().BeOfType<OpenIdAuthenticationResult>();
        }

        [Fact]
        public async Task Should_fail_when_the_authentication_token_has_expired()
        {
            authStrategyMock.AuthTokenExpiresInSeconds(10);
            stopwatchMock.ActiveStopwatchAdvancedTo(11.Seconds());
            
            Func<Task> action = () => authenticator.AuthenticateAsync();

            await action.Should().ThrowAsync<OpenIdException>();
        }

        [Fact]
        public async Task Should_just_return_result_when_authorization_has_been_made_and_token_is_still_active()
        {
            authStrategyMock.AuthTokenExpiresInSeconds(40);
            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOnce();
            
            stopwatchMock.ActiveStopwatchAdvancedTo(11.Seconds());
            var authenticationResult = await authenticator.AuthenticateAsync();

            authenticationResult.Should().BeOfType<OpenIdAuthenticationResult>();
            authStrategyMock.ReceivedAuthenticateOnce();
            openIdMock.DidNotReceiveRefreshToken();
        }
        
        [Fact]
        public async Task Should_remember_access_token_of_first_time_authentication()
        {
            const string firstTimeAccessToken = "token1";
            authStrategyMock.AuthenticateReturnsToken(firstTimeAccessToken, "refresh1", 40);
            await authenticator.AuthenticateAsync();
            
            var authenticationResult = await authenticator.AuthenticateAsync();

            var openIdAuthResult = authenticationResult.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
            openIdAuthResult.AccessToken.Should().Be(firstTimeAccessToken);
        }

        [Fact]
        public async Task Should_refresh_token_when_authorization_has_been_made_and_token_is_expired()
        {
            const string firstTimeRefreshToken = "refresh1";
            authStrategyMock.AuthenticateReturnsToken("token1", firstTimeRefreshToken, 40);
            
            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOnce();

            stopwatchMock.ActiveStopwatchAdvancedToRefreshTokenTimeWhenActiveTokenTTLIs(40.Seconds());
            var authenticationResult = await authenticator.AuthenticateAsync();

            authenticationResult.Should().BeOfType<OpenIdAuthenticationResult>();
            authStrategyMock.ReceivedAuthenticateOnce();
            openIdMock.ReceiveRefreshTokenOnce(firstTimeRefreshToken);
        }

        [Fact]
        public async Task Should_remember_refreshed_access_token()
        {
            authStrategyMock.AuthenticateReturnsToken("token1", "refresh1", 40);
            openIdMock.RefreshReturnsToken("token2", "refresh2", 40);
            
            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOnce();

            stopwatchMock.ActiveStopwatchAdvancedToRefreshTokenTimeWhenActiveTokenTTLIs(40.Seconds());
            var authenticationResult = await authenticator.AuthenticateAsync();

            var openIdAuthResult = authenticationResult.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
            openIdAuthResult.AccessToken.Should().Be("token2");
        }

        [Fact]
        public async Task Should_remember_refresh_tokens_and_set_new_TTL_for_the_refreshed_access_token()
        {
            authStrategyMock.AuthenticateReturnsToken("token1", "refresh1", 40);
            // second access token will expire after 60 sec.
            openIdMock.RefreshReturnsToken("token2", "refresh2", 60);
            
            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOnce();

            stopwatchMock.ActiveStopwatchAdvancedToRefreshTokenTimeWhenActiveTokenTTLIs(40.Seconds());
            await authenticator.AuthenticateAsync();
            
            // initiate second refresh of the access token with new TTL (60 seconds)
            stopwatchMock.ActiveStopwatchAdvancedToRefreshTokenTimeWhenActiveTokenTTLIs(60.Seconds());
            openIdMock.RefreshReturnsToken("token3", "refresh2", 90);
            await authenticator.AuthenticateAsync();

            authStrategyMock.ReceivedAuthenticateOnce();
            openIdMock.ReceiveRefreshTokens("refresh1", "refresh2");
        }
        
        [Fact]
        public async Task Should_fail_when_the_refreshed_token_has_expired()
        {
            authStrategyMock.AuthenticateReturnsToken("token1", "refresh1", 40);
            openIdMock.RefreshReturnsToken("token2", "refresh2", 60);
            
            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOnce();
            
            stopwatchMock.ActiveStopwatchAdvancedToRefreshTokenTimeWhenActiveTokenTTLIs(40.Seconds());
            stopwatchMock.UpcomingStopwatchAdvancedTo(60.Seconds());

            Func<Task> action = () => authenticator.AuthenticateAsync();

            await action.Should().ThrowAsync<OpenIdException>();
        }

        [Fact]
        public async Task Should_reauthenticate_if_TTL_of_the_current_token_expired()
        {
            authStrategyMock.AuthenticateReturnsToken("token1", "refresh1", 40);

            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOnce();
            authStrategyMock.AuthenticateReturnsToken("token2", "refresh2", 50);

            stopwatchMock.ActiveStopwatchAdvancedTo(40.Seconds());
            var authenticationResult = await authenticator.AuthenticateAsync();
            
            authStrategyMock.ReceivedAuthenticateTwice();
            var openIdAuthResult = authenticationResult.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
            openIdAuthResult.AccessToken.Should().Be("token2");
        }
        
        [Fact]
        public async Task Should_reauthenticate_when_there_is_no_a_refresh_token_and_the_access_token_will_expire_in_proactive_period()
        {
            authStrategyMock.AuthenticateReturnsToken("token1", null, 40);

            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOnce();
            
            authStrategyMock.AuthenticateReturnsToken("token2", null, 50);
            stopwatchMock.ActiveStopwatchAdvancedTo(35.Seconds());
            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateTwice();
            
            authStrategyMock.AuthenticateReturnsToken("token3", null, 60);
            stopwatchMock.ActiveStopwatchAdvancedTo(45.Seconds());
            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOfTimes(3);
        }

        [Fact]
        public async Task Should_updates_TTL_on_each_successful_authentication_attempt()
        {
            authStrategyMock.AuthenticateReturnsToken("token1", "refresh1", 40);

            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOnce();
            authStrategyMock.AuthenticateReturnsToken("token2", "refresh2", 50);

            stopwatchMock.ActiveStopwatchAdvancedTo(50.Seconds());
            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateTwice();
            authStrategyMock.AuthenticateReturnsToken("token3", "refresh3", 60);

            stopwatchMock.ActiveStopwatchAdvancedTo(50.Seconds());
            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOfTimes(3);
        }
        
        [Fact]
        public async Task Should_reauthenticate_when_the_token_is_not_expired_but_the_force_flag_is_enabled()
        {
            authStrategyMock.AuthTokenExpiresInSeconds(40);
            
            await authenticator.AuthenticateAsync();
            authStrategyMock.ReceivedAuthenticateOnce();
            
            authStrategyMock.AuthTokenExpiresInSeconds(40);
            stopwatchMock.ActiveStopwatchAdvancedTo(10.Seconds());

            await authenticator.AuthenticateAsync(true);
            
            authStrategyMock.ReceivedAuthenticateTwice();
        }

        private class OpenIdClientMock
        {
            public OpenIdClientMock()
            {
                Instance = Substitute.For<IOpenIdClient>();
                RefreshReturnsToken("new-access-token", "new-refresh-token", 30);
            }

            public IOpenIdClient Instance { get; }

            public void RefreshReturnsToken(string accessToken, string refreshToken, int expireInSeconds)
            {
                Instance.RequestTokenAsync(Arg.Any<RefreshTokenRequest>(), Arg.Any<TimeSpan?>())
                    .Returns(new TokenResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        ExpiresInSeconds = expireInSeconds
                    });
            }

            public void DidNotReceiveRefreshToken()
            {
                Instance.DidNotReceive().RequestTokenAsync(Arg.Any<RefreshTokenRequest>(), Arg.Any<TimeSpan?>());
            }

            public void ReceiveRefreshTokenOnce(string refreshToken)
            {
                Instance.Received(1).RequestTokenAsync(Arg.Is<RefreshTokenRequest>(x => x.RefreshToken == refreshToken), Arg.Any<TimeSpan?>());
            }

            public void ReceiveRefreshTokens(params string[] refreshTokens)
            {
                Instance.Received(refreshTokens.Length)
                    .RequestTokenAsync(Arg.Any<RefreshTokenRequest>(), Arg.Any<TimeSpan?>());
                Received.InOrder(() =>
                {
                    foreach (var refreshToken in refreshTokens)
                    {
                        _ = Instance.RequestTokenAsync(Arg.Is<RefreshTokenRequest>(x => x.RefreshToken == refreshToken), Arg.Any<TimeSpan?>());
                    }
                });
            }
        }

        private class AuthenticationStrategyMock
        {
            private const string AuthRefreshToken = "refresh-token";
            private const string AuthAccessToken = "access-token";

            public AuthenticationStrategyMock()
            {
                Instance = Substitute.For<IOpenIdAuthenticationStrategy>();
                AuthTokenExpiresInSeconds(30);
            }

            public IOpenIdAuthenticationStrategy Instance { get; }

            public void AuthTokenExpiresInSeconds(int expiresInSeconds) => AuthenticateReturnsToken(AuthAccessToken, AuthRefreshToken, expiresInSeconds);

            public void AuthenticateReturnsToken(string accessToken, string? refreshToken, int expiresInSeconds)
            {
                Instance
                    .AuthenticateAsync(Arg.Any<IOpenIdClient>(), Arg.Any<OpenIdAuthenticationOptions>(), Arg.Any<TimeSpan?>())
                    .Returns(Task.FromResult(new TokenResponse
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        ExpiresInSeconds = expiresInSeconds 
                    }));
            }

            public void ReceivedAuthenticateTwice()
            {
                ReceivedAuthenticateOfTimes(2);
            }

            public void ReceivedAuthenticateOnce()
            {
                ReceivedAuthenticateOfTimes(1);
            }

            public void ReceivedAuthenticateOfTimes(int times)
            {
                _ = Instance
                    .Received(times)
                    .AuthenticateAsync(Arg.Any<IOpenIdClient>(), Arg.Any<OpenIdAuthenticationOptions>(), Arg.Any<TimeSpan?>());
            }
        }
    }
}
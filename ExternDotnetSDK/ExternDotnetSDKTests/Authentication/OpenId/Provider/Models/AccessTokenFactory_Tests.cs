using FluentAssertions;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Authentication.OpenId.Provider.Models;
using Kontur.Extern.Client.Authentication.OpenId.Time;
using NSubstitute;
using NUnit.Framework;
using Vostok.Commons.Time;

namespace Kontur.Extern.Client.Tests.Authentication.OpenId.Provider.Models
{
    [TestFixture]
    internal class AccessTokenFactory_Tests
    {
        [TestFixture]
        internal class Ctor
        {
            [Test]
            public void Should_start_a_new_stopwatch()
            {
                var stopwatchFactory = Substitute.For<IStopwatchFactory>();
                
                _ = new AccessTokenFactory(stopwatchFactory);

                stopwatchFactory.Received(1).Start();
            }
        }
        
        [TestFixture]
        internal class CreateIfNotExpired
        {
            private IStopwatchFactory stopwatchFactory;
            private IStopwatch stopwatch;

            [SetUp]
            public void SetUp()
            {
                stopwatchFactory = Substitute.For<IStopwatchFactory>();
                stopwatch = Substitute.For<IStopwatch>();
                stopwatchFactory.Start().Returns(stopwatch);
            }
            
            [Test]
            public void Should_return_a_new_access_token_if_the_response_has_not_expired_yet()
            {
                var factory = new AccessTokenFactory(stopwatchFactory);
                stopwatch.Elapsed.Returns(1.Seconds());
                
                var accessToken = factory.CreateIfNotExpired(CreateTokenResponse(2));

                accessToken.Should().NotBeNull();
                accessToken!.HasNotExpired.Should().BeTrue();
            }
            
            [Test]
            public void Should_initialize_token_with_values_from_the_response_has_not_expired_yet()
            {
                var factory = new AccessTokenFactory(stopwatchFactory);
                stopwatch.Elapsed.Returns(1.Seconds());
                
                var accessToken = factory.CreateIfNotExpired(CreateTokenResponse(2, "access-token", "refresh_token"));

                accessToken.Should().NotBeNull();
                accessToken!.ToString().Should().Be("access-token");
                accessToken!.RefreshToken.Should().Be("refresh_token");
            }
            
            [Test]
            public void Should_return_null_access_token_if_the_response_has_already_expired()
            {
                var factory = new AccessTokenFactory(stopwatchFactory);
                stopwatch.Elapsed.Returns(1.Seconds());
                
                var accessToken = factory.CreateIfNotExpired(CreateTokenResponse(1));

                accessToken.Should().BeNull();
            }

            private static TokenResponse CreateTokenResponse(int expiresInSeconds, string accessToken = "123", string refreshToken = "123") => new()
            {
                ExpiresInSeconds = expiresInSeconds, 
                AccessToken = accessToken, 
                RefreshToken = refreshToken
            };
        }
    }
}
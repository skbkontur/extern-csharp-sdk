using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator;
using Kontur.Extern.Api.Client.Auth.OpenId.Builder;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Testing.End2End.ClusterClient;
using Kontur.Extern.Api.Client.Testing.End2End.Environment;
using Kontur.Extern.Api.Client.Testing.Fakes.Logging;
using Kontur.Extern.Api.Client.Testing.Fakes.Time;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.Auth.OpenId.End2EndTests.Authenticator
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class OpenIdAuthenticator_Tests
    {
        public class Authenticate_By_Password : BaseTests
        {
            public Authenticate_By_Password(ITestOutputHelper output)
                : base(output)
            {
            }
            
            [Fact]
            public async Task Should_authenticate_by_password_for_the_first_time()
            {
                var authenticator = CreateAuthenticator(CreateStrategy);
            
                var result = await authenticator.AuthenticateAsync();

                var authResult = result.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
                authResult.AccessToken.Should().NotBeNullOrWhiteSpace();
            }
            
            [Fact]
            public async Task Should_reauthenticate_if_the_access_token_expired()
            {
                var authenticator = CreateAuthenticator(CreateStrategy);
            
                var firstTimeResult = (await authenticator.AuthenticateAsync()).As<OpenIdAuthenticationResult>();
                var firstTimeAccessToken = firstTimeResult.AccessToken;
                stopwatchMock.ActiveStopwatchAdvancedTo(firstTimeResult.RemainingTime);
                
                var result = await authenticator.AuthenticateAsync();

                var authResult = result.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
                authResult.AccessToken.Should().NotBeNullOrWhiteSpace().And.NotBe(firstTimeAccessToken);
            }
            
            [Fact]
            public async Task Should_reauthenticate_if_the_access_token_is_valid_but_the_force_flag_is_enabled()
            {
                var authenticator = CreateAuthenticator(CreateStrategy);
            
                var firstTimeResult = (await authenticator.AuthenticateAsync()).As<OpenIdAuthenticationResult>();
                var firstTimeAccessToken = firstTimeResult.AccessToken;
                stopwatchMock.ActiveStopwatchAdvancedTo(0.Seconds());
                
                var result = await authenticator.AuthenticateAsync(true);

                var authResult = result.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
                authResult.AccessToken.Should().NotBeNullOrWhiteSpace().And.NotBe(firstTimeAccessToken);
            }

            private static OpenIdAuthenticatorBuilder.Configured CreateStrategy(OpenIdAuthenticatorBuilder.SpecifyAuthStrategy builder, AuthTestData authTestData) => 
                builder.WithAuthenticationByPassword(authTestData.UserName, authTestData.Password);
        }

        public class Authenticate_By_Certificate : BaseTests
        {
            public Authenticate_By_Certificate(ITestOutputHelper output)
                : base(output)
            {
            }

            [Fact]
            public async Task Should_authenticate_by_certificate()
            {
                var authenticator = CreateAuthenticator(CreateStrategy);

                var result = await authenticator.AuthenticateAsync();

                var authResult = result.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
                authResult.AccessToken.Should().NotBeNullOrWhiteSpace();
            }

            [Fact]
            public async Task Should_reauthenticate_if_the_access_token_expired()
            {
                var authenticator = CreateAuthenticator(CreateStrategy);

                var firstTimeResult = (await authenticator.AuthenticateAsync()).As<OpenIdAuthenticationResult>();
                var firstTimeAccessToken = firstTimeResult.AccessToken;
                stopwatchMock.ActiveStopwatchAdvancedTo(firstTimeResult.RemainingTime);

                var result = await authenticator.AuthenticateAsync();

                var authResult = result.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
                authResult.AccessToken.Should().NotBeNullOrWhiteSpace().And.NotBe(firstTimeAccessToken);
            }

            private static OpenIdAuthenticatorBuilder.Configured CreateStrategy(OpenIdAuthenticatorBuilder.SpecifyAuthStrategy builder, AuthTestData authTestData) =>
                builder.WithAuthenticationByCertificate(authTestData.CertificateThumbprint);
        }

        public abstract class BaseTests
        {
            internal readonly StopwatchMock stopwatchMock;
            private readonly ILog log;

            protected BaseTests(ITestOutputHelper output)
            {
                stopwatchMock = new StopwatchMock(5.Seconds());
                log = new TestLog(output);
            }

            protected private IAuthenticator CreateAuthenticator(Func<OpenIdAuthenticatorBuilder.SpecifyAuthStrategy, AuthTestData, OpenIdAuthenticatorBuilder.Configured> strategyFactory)
            {
                var testData = AuthTestData.LoadFromJsonFile();
                return new OpenIdAuthenticatorBuilder(new WinApiCrypt(), log)
                    .WithHttpConfiguration(new TestingHttpClientConfiguration(testData.OpenIdServer))
                    .WithClientIdentification(testData.ClientId, testData.ApiKey)
                    .WithAuthenticationStrategy(x => strategyFactory(x, testData))
                    .SubstituteStopwatch(stopwatchMock.StopwatchFactory)
                    .Build();
            }
        }
    }
}
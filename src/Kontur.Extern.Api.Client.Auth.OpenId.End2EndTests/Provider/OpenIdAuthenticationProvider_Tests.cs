using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Auth.OpenId.Builder;
using Kontur.Extern.Api.Client.Auth.OpenId.Provider;
using Kontur.Extern.Api.Client.Testing.End2End.ClusterClient;
using Kontur.Extern.Api.Client.Testing.End2End.Environment;
using Kontur.Extern.Api.Client.Testing.Fakes.Logging;
using Kontur.Extern.Api.Client.Testing.Fakes.Time;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.Auth.OpenId.End2EndTests.Provider
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class OpenIdAuthenticationProvider_Tests
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
                var provider = CreateAuthProvider(CreateStrategy);
            
                var result = await provider.AuthenticateAsync();

                var authResult = result.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
                authResult.AccessToken.Should().NotBeNullOrWhiteSpace();
            }
            
            [Fact]
            public async Task Should_reauthenticate_if_the_access_token_expired()
            {
                var provider = CreateAuthProvider(CreateStrategy);
            
                var firstTimeResult = (await provider.AuthenticateAsync()).As<OpenIdAuthenticationResult>();
                var firstTimeAccessToken = firstTimeResult.AccessToken;
                stopwatchMock.ActiveStopwatchAdvancedTo(firstTimeResult.RemainingTime);
                
                var result = await provider.AuthenticateAsync();

                var authResult = result.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
                authResult.AccessToken.Should().NotBeNullOrWhiteSpace().And.NotBe(firstTimeAccessToken);
            }
            
            [Fact]
            public async Task Should_reauthenticate_if_the_access_token_is_valid_but_the_force_flag_is_enabled()
            {
                var provider = CreateAuthProvider(CreateStrategy);
            
                var firstTimeResult = (await provider.AuthenticateAsync()).As<OpenIdAuthenticationResult>();
                var firstTimeAccessToken = firstTimeResult.AccessToken;
                stopwatchMock.ActiveStopwatchAdvancedTo(0.Seconds());
                
                var result = await provider.AuthenticateAsync(true);

                var authResult = result.Should().BeOfType<OpenIdAuthenticationResult>().Subject;
                authResult.AccessToken.Should().NotBeNullOrWhiteSpace().And.NotBe(firstTimeAccessToken);
            }

            private static OpenIdAuthenticationProviderBuilder.Configured CreateStrategy(OpenIdAuthenticationProviderBuilder.SpecifyAuthStrategy builder, AuthTestData authTestData) => 
                builder.WithAuthenticationByPassword(authTestData.UserName, authTestData.Password);
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

            protected private IAuthenticationProvider CreateAuthProvider(Func<OpenIdAuthenticationProviderBuilder.SpecifyAuthStrategy, AuthTestData, OpenIdAuthenticationProviderBuilder.Configured> strategyFactory)
            {
                var testData = AuthTestData.LoadFromJsonFile();
                return new OpenIdAuthenticationProviderBuilder(log)
                    .WithHttpConfiguration(new TestingHttpClientConfiguration(testData.OpenIdServer))
                    .WithClientIdentification(testData.ClientId, testData.ApiKey)
                    .WithAuthenticationStrategy(x => strategyFactory(x, testData))
                    .SubstituteStopwatch(stopwatchMock.StopwatchFactory)
                    .Build();
            }
        }
    }
}
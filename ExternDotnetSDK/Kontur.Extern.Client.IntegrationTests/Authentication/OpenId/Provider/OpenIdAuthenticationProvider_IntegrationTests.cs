using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.Authentication.OpenId.Provider;
using Kontur.Extern.Client.Authentication.OpenId.Provider.AuthStrategies;
using Kontur.Extern.Client.IntegrationTests.TestEnvironment;
using Kontur.Extern.Client.IntegrationTests.TestLogging;
using Kontur.Extern.Client.Testing.Fakes.Time;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.IntegrationTests.Authentication.OpenId.Provider
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class OpenIdAuthenticationProvider_IntegrationTests
    {
        public class Authenticate_By_Password : BaseTests
        {
            public Authenticate_By_Password([JetBrains.Annotations.NotNull] ITestOutputHelper output)
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

            private static IOpenIdAuthenticationStrategy CreateStrategy(AuthTestData testData) => 
                new PasswordOpenIdAuthenticationStrategy(testData.UserCredentials);
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

            protected private OpenIdAuthenticationProvider CreateAuthProvider(Func<AuthTestData, IOpenIdAuthenticationStrategy> strategyFactory)
            {
                var testData = AuthTestData.LoadFromJsonFile();
                var openId = OpenIdClientFactory.CreateTestClient(testData.OpenIdServer, log);
                var options = new OpenIdAuthenticationOptions(testData.ApiKey, testData.ClientId);
                return new OpenIdAuthenticationProvider(
                    options,
                    openId,
                    strategyFactory(testData),
                    stopwatchMock.StopwatchFactory
                );
            }
        }
    }
}
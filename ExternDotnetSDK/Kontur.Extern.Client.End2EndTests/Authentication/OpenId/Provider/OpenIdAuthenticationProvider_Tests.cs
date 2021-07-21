using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.Authentication;
using Kontur.Extern.Client.Authentication.OpenId.Builder;
using Kontur.Extern.Client.Authentication.OpenId.Provider;
using Kontur.Extern.Client.End2EndTests.TestClusterClient;
using Kontur.Extern.Client.End2EndTests.TestEnvironment;
using Kontur.Extern.Client.End2EndTests.TestLogging;
using Kontur.Extern.Client.HttpLevel.Options;
using Kontur.Extern.Client.HttpLevel.Serialization;
using Kontur.Extern.Client.Testing.Fakes.Time;
using Vostok.Commons.Time;
using Vostok.Logging.Abstractions;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.End2EndTests.Authentication.OpenId.Provider
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class OpenIdAuthenticationProvider_Tests
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
                
                var clusterClient = ClusterClientFactory.CreateTestClient(testData.OpenIdServer, log);
                var authenticationProvider = new OpenIdAuthenticationProviderBuilder(new JsonSerializer(), log)
                    .WithClusterClient(clusterClient)
                    .WithClientIdentification(testData.ClientId, testData.ApiKey)
                    .WithAuthenticationStrategy(x => strategyFactory(x, testData))
                    .SubstituteStopwatch(stopwatchMock.StopwatchFactory)
                    .Build();

                return authenticationProvider;
            }
        }
    }
}
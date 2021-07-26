using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;
using Kontur.Extern.Client.Auth.OpenId.Client;
using Kontur.Extern.Client.Auth.OpenId.Client.Models.Requests;
using Kontur.Extern.Client.Auth.OpenId.End2EndTests.TestFactories;
using Kontur.Extern.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Client.Testing.End2End.Environment;
using Kontur.Extern.Client.Testing.Fakes.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Client.Auth.OpenId.End2EndTests.Client
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class OpenIdClient_Tests
    {
        public class RequestToken_By_PasswordTokenRequest : BaseTests
        {
            public RequestToken_By_PasswordTokenRequest(ITestOutputHelper output)
                : base(output)
            {
            }
            
            [Fact]
            public async Task Should_successfully_authenticate_query()
            {
                var response = await OpenIdClient.RequestTokenAsync(RequestFactory.CorrectPasswordAuthenticationRequest());

                response.AccessToken.Should().NotBeNullOrWhiteSpace();
                response.ExpiresInSeconds.Should().BeGreaterThan(0);
            }

            [Fact]
            public void Should_fail_when_given_incorrect_credentials() => 
                ShouldFail(RequestFactory.PasswordAuthenticationRequestWithIncorrectCredentials());

            [Fact]
            public void Should_fail_when_given_an_incorrect_apy_key() => 
                ShouldFail(RequestFactory.PasswordAuthenticationRequestWithIncorrectApiKey());
            
            [Fact]
            public void Should_fail_when_given_an_incorrect_scope() => 
                ShouldFail(RequestFactory.PasswordAuthenticationRequestWithIncorrectScope());
            
            [Fact]
            public void Should_fail_when_given_an_incorrect_client_id() => 
                ShouldFail(RequestFactory.PasswordAuthenticationRequestWithIncorrectClientId());

            private void ShouldFail(PasswordTokenRequest request)
            {
                Func<Task> action = () => OpenIdClient.RequestTokenAsync(request);

                var exception = action.Should().Throw<OpenIdException>().Which;
                Console.WriteLine(exception);
            }
        }

        public abstract class BaseTests
        {
            internal readonly OpenIdClient OpenIdClient;
            internal readonly OpenIdRequestFactory RequestFactory;

            protected BaseTests(ITestOutputHelper output)
            {
                var testData = AuthTestData.LoadFromJsonFile();
                var log = new TestLog(output);
                RequestFactory = new OpenIdRequestFactory(testData);
                OpenIdClient = OpenIdClientFactory.CreateTestClient(testData.OpenIdServer, log);
            }
        }
    }
}
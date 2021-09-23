using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.Testing.End2End.ClusterClient;
using Kontur.Extern.Api.Client.Testing.End2End.Environment;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestAuthenticator
{
    internal static class SpecifyAuthenticatorExternBuilderExtension
    {
        public static ExternBuilder.Configured WithTestOpenIdAuthenticator(this ExternBuilder.SpecifyAuthenticator specifyAuthenticator, AuthTestData authTestData, Credentials credentials) =>
            specifyAuthenticator.WithOpenIdAuthenticator(builder => builder
                .WithHttpConfiguration(new TestingHttpClientConfiguration(authTestData.OpenIdServer))
                .WithClientIdentification(authTestData.ClientId, authTestData.ApiKey)
                .WithAuthenticationByPassword(credentials.UserName, credentials.Password)
            );
    }
}
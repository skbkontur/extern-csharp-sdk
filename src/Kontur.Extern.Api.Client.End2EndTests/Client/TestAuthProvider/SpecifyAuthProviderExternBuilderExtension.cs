using Kontur.Extern.Api.Client.Auth.OpenId.Provider.Models;
using Kontur.Extern.Api.Client.Testing.End2End.ClusterClient;
using Kontur.Extern.Api.Client.Testing.End2End.Environment;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestAuthProvider
{
    internal static class SpecifyAuthProviderExternBuilderExtension
    {
        public static ExternBuilder.Configured WithTestOpenIdAuthProvider(this ExternBuilder.SpecifyAuthProvider specifyAuthProvider, AuthTestData authTestData, Credentials credentials) =>
            specifyAuthProvider.WithOpenIdAuthProvider(builder => builder
                .WithHttpConfiguration(new TestingHttpClientConfiguration(authTestData.OpenIdServer))
                .WithClientIdentification(authTestData.ClientId, authTestData.ApiKey)
                .WithAuthenticationByPassword(credentials.UserName, credentials.Password)
            );
    }
}
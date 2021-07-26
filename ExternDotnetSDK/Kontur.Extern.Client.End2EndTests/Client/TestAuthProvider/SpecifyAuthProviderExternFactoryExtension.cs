using Kontur.Extern.Client.Testing.End2End.ClusterClient;
using Kontur.Extern.Client.Testing.End2End.Environment;

namespace Kontur.Extern.Client.End2EndTests.Client.TestAuthProvider
{
    internal static class SpecifyAuthProviderExternFactoryExtension
    {
        public static IExternFactory WithTestOpenIdAuthProvider(this ISpecifyAuthProviderExternFactory externFactory) =>
            externFactory.WithOpenIdAuthProvider(builder =>
            {
                var authTestData = AuthTestData.LoadFromJsonFile();
                return builder
                    .WithClusterClient(ClusterClientFactory.CreateTestClient(authTestData.OpenIdServer, builder.Log))
                    .WithClientIdentification(authTestData.ClientId, authTestData.ApiKey)
                    .WithAuthenticationByPassword(authTestData.UserName, authTestData.Password);
            });
    }
}
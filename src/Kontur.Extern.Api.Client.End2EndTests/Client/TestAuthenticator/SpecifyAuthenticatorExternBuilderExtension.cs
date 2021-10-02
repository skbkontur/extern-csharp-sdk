using Kontur.Extern.Api.Client.Auth.OpenId;
using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.Auth.OpenId.Builder;
using Kontur.Extern.Api.Client.Testing.End2End.Environment;
using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestAuthenticator
{
    internal static class SpecifyAuthenticatorExternBuilderExtension
    {
        public static ExternBuilder.Configured WithTestOpenIdAuthenticator(
            this ExternBuilder.SpecifyAuthenticator specifyAuthenticator,
            AuthTestData authTestData,
            Credentials credentials,
            ILog log)
        {
            return specifyAuthenticator.WithAuthenticator(
                () => new OpenIdAuthSetup(
                    new OpenIdAuthenticatorBuilder(log),
                    credentials,
                    authTestData.ApiKey,
                    authTestData.ClientId,
                    authTestData.OpenIdServer).Configure()
            );
        }
    }
}
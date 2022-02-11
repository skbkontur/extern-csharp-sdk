using Kontur.Extern.Api.Client.Auth.Abstractions;
using Kontur.Extern.Api.Client.Auth.OpenId.Builder;

namespace Kontur.Extern.Api.Client
{
    public delegate IAuthenticator OpenIdSetup(OpenIdAuthenticatorBuilder builder);
}
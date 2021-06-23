using Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests;
using Kontur.Extern.Client.Clients.Authentication.Providers;

namespace Kontur.Extern.Client.Engine.EngineBuilder
{
    public interface ILoggedEngine
    {
        IAuthenticatedEngine WithAuth(IAuthenticationProvider provider);

        IAuthenticatedEngine WithPasswordAuth(string authenticationBaseAddress, PasswordTokenRequest passwordTokenRequest);

        IAuthenticatedEngine WithCertificateAuth(string authenticationBaseAddress, CertificateAuthenticationRequest certificateAuthenticationRequest);
    }
}
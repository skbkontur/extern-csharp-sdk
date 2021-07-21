using Vostok.Logging.Abstractions;

namespace Kontur.Extern.Client.Authentication.OpenId.Builder
{
    public interface IBasicClientOpenIdAuthenticationProviderBuilder
    {
        ILog Log { get; }
    }
}
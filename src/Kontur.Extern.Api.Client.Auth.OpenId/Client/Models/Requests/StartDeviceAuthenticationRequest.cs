

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Запрос для начала аутентификации по Device Flow
    /// </summary>
    /// <seealso cref="ScopedAuthenticatedRequest" />
    public class StartDeviceAuthenticationRequest : ScopedAuthenticatedRequest
    {
        public StartDeviceAuthenticationRequest(string scope, string clientId, string clientSecret)
            : base(scope, clientId, clientSecret)
        { }
    }
}

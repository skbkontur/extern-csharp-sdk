using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Запрос для получения токенов авторизации по Device Flow
    /// </summary>
    /// <seealso cref="ScopedAuthenticatedRequest" />
    public class DeviceTokenRequest : ScopedAuthenticatedRequest
    {
        public DeviceTokenRequest([NotNull] string deviceCode, string scope, string clientId, string clientSecret)
            : base(scope, clientId, clientSecret)
        {
            if (string.IsNullOrWhiteSpace(deviceCode))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(deviceCode));

            DeviceCode = deviceCode;
        }

        public string DeviceCode { get; }
    }
}
#nullable enable
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models
{
    [PublicAPI]
    public interface IAccessToken
    {
        string ToString();

        bool TryGetRefreshToken(out string token);

        bool HasNotExpired { get; }
        bool HasExpired { get; }
        TimeInterval RemainingTime { get; }

        bool WillExpireAfter(TimeInterval interval);
    }
}
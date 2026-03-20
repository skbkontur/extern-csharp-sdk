#nullable enable
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models
{
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
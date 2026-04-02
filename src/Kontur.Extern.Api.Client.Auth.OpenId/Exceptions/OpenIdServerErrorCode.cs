namespace Kontur.Extern.Api.Client.Auth.OpenId.Exceptions
{
    public enum OpenIdServerErrorCode
    {
        AbsentOrUnknown = 0,

        InvalidRequest,
        InvalidClient,
        InvalidScope,
        InvalidGrant,
        UnsupportedGrantType,
        UnauthorizedClient,
        AuthorizationPending,
        SlowDown,
        AccessDenied,
        ExpiredToken,
    }
}

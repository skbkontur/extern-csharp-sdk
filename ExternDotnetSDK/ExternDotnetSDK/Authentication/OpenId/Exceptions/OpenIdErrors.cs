using System;

namespace Kontur.Extern.Client.Authentication.OpenId.Exceptions
{
    internal static class OpenIdErrors
    {
        public static Exception AuthTokenHasAlreadyExpired() => new OpenIdException("The auth token has already expired");
    }
}
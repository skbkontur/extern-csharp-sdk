using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Authentication.OpenId.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider.Models
{
    internal static class AccessTokenFactoryExtension
    {
        // todo: replace  CreateIfNotExpired with CreateAccessToken 
        public static AccessToken CreateAccessToken(this AccessTokenFactory accessTokenFactory, TokenResponse tokenResponse)
        {
            var token = accessTokenFactory.CreateIfNotExpired(tokenResponse);
            if (token == null)
                throw OpenIdErrors.AuthTokenHasAlreadyExpired();
            return token;
        }
    }
}
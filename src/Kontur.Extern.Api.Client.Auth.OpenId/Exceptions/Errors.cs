using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Exceptions
{
    internal static class Errors
    {
        public static Exception StringShouldNotBeNullOrWhiteSpace([InvokerParameterName] string paramName) => 
            new ArgumentException("The given value cannot be null, or empty, or a whitespace string.", paramName);
        
        public static Exception StringShouldNotBeEmptyOrWhiteSpace([InvokerParameterName] string paramName) => 
            new ArgumentException("The given value cannot an empty or a whitespace string.", paramName);

        public static Exception AccessTokenAlreadyExpired([InvokerParameterName] string paramName) => 
            new ArgumentException("The access token has been expired already", paramName);

        public static Exception ArrayCannotBeEmpty([InvokerParameterName] string paramName) =>
            new ArgumentException("Value cannot be an empty collection.", paramName);
        
        public static Exception AuthTokenHasAlreadyExpired() => 
            new OpenIdException("The auth token has already expired");

        public static Exception TokenResponseHasEmptyAccessToken() => 
            new OpenIdException("The token response has empty access token, probably because of serialization");
        
        public static Exception TokenResponseInvalidExpirationSeconds(int expiresInSeconds) => 
            new OpenIdException($"The token response has invalid token expiration seconds {expiresInSeconds}");
    }
}
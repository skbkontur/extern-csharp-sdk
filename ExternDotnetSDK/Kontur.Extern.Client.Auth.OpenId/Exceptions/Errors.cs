using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Auth.OpenId.Exceptions
{
    internal static class Errors
    {
        public static Exception TimeSpanShouldBePositive([InvokerParameterName] string paramName, TimeSpan actualValue) => 
            new ArgumentOutOfRangeException(paramName, actualValue, "The duration interval should be positive");

        public static Exception StopwatchHaveToBeRunning([InvokerParameterName] string paramName) => 
            new ArgumentException("The stopwatch have to be running", paramName);
        
        public static Exception TimeIntervalShouldBeNonEmpty([InvokerParameterName] string paramName) => 
            new ArgumentException("The duration interval should not be empty", paramName);

        public static Exception StringShouldNotBeNullOrWhiteSpace([InvokerParameterName] string paramName) => 
            new ArgumentException("The given value cannot be null, or empty, or a whitespace string.", paramName);
        
        public static Exception StringShouldNotBeEmptyOrWhiteSpace([InvokerParameterName] string paramName) => 
            new ArgumentException("The given value cannot an empty or a whitespace string.", paramName);

        public static Exception AccessTokenAlreadyExpired([InvokerParameterName] string paramName) => 
            new ArgumentException("The access token has been expired already", paramName);

        public static Exception ArrayCannotBeEmpty([InvokerParameterName] string paramName) =>
            new ArgumentException("Value cannot be an empty collection.", paramName);

        public static Exception UrlShouldBeAbsolute([InvokerParameterName] string paramName, Uri uri) => 
            new ArgumentException($"The value '{uri}' is not be absolute url", paramName);
        
        public static Exception AuthTokenHasAlreadyExpired() => 
            new OpenIdException("The auth token has already expired");

        public static Exception TokenResponseHasEmptyAccessToken() => 
            new OpenIdException("The token response has empty access token, probably because of serialization");
        
        public static Exception TokenResponseInvalidExpirationSeconds(int expiresInSeconds) => 
            new OpenIdException($"The token response has invalid token expiration seconds {expiresInSeconds}");
    }
}
﻿using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Request for token using trusted grant
    /// </summary>
    /// <seealso cref="ScopedAuthenticatedRequest" />
    public class JwtTrustedTokenRequest : ScopedAuthenticatedRequest
    {
        public JwtTrustedTokenRequest(string jwtToken, string scope, string clientId, string clientSecret)
            : base(scope, clientId, clientSecret)
        {
            if (string.IsNullOrWhiteSpace(jwtToken))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(jwtToken));
            
            Token = jwtToken;
        }
        
        /// <summary>
        /// Gets user jwt token
        /// </summary>
        public string Token { get; }
    }
}
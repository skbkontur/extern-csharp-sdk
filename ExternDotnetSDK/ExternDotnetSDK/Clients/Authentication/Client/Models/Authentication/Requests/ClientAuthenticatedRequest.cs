using System;
using System.Net.Http;
using Kontur.Extern.Client.Clients.Authentication.Client.Extensions;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Request for authenticated client
    /// </summary>
    public abstract class ClientAuthenticatedRequest
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        protected HttpRequestMessage BuildOpenIdClientAuthenticatedRequest(
            string formContent,
            ClientAuthenticatedRequest clientAuthenticatedRequest,
            TimeSpan? timeout,
            string uri = "/connect/token"
        )
        {
            return new HttpRequestMessage(HttpMethod.Post, new Uri(uri, UriKind.Relative))
                .WithAcceptHeader("application/json")
                .WithContentTypeHeader("application/x-www-form-urlencoded")
                .WithBasicAuthorizationHeader(clientAuthenticatedRequest.ClientId, clientAuthenticatedRequest.ClientSecret)
                .WithContent(formContent);
        }
    }
}
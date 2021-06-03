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
    }
}
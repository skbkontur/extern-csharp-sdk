﻿using Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.Models;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Базовый запрос для аутентификации
    /// </summary>
    public abstract class ClientAuthenticatedRequest
    {
        protected ClientAuthenticatedRequest(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(clientSecret));
            
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
        
        /// <summary>
        /// Получить или установить идентификатор клиента
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Получить или установить секрет клиента
        /// </summary>
        /// <value>
        /// ApiKey
        /// </value>
        public string ClientSecret { get; }

        public Credentials ToRequestAuthCredentials() => new(ClientId, ClientSecret);
    }
}
namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Базовый запрос для аутентификации
    /// </summary>
    public abstract class ClientAuthenticatedRequest
    {
        /// <summary>
        /// Получить или установить идентификатор клиента
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Получить или установить секрет клиента
        /// </summary>
        /// <value>
        /// ApiKey
        /// </value>
        public string ClientSecret { get; set; }
    }
}
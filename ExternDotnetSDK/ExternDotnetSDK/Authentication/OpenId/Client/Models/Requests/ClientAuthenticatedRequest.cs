namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests
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
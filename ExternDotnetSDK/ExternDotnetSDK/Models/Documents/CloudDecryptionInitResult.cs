using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CloudDecryptionInitResult
    {
        /// <summary>
        /// Ссылка для подтверждения дешифрования
        /// </summary>
        public Link ConfirmLink { get; set; }

        /// <summary>
        /// Идентификатор запроса, нужен для подтверждения дешифрования
        /// </summary>
        public string RequestId { get; set; }
    }
}
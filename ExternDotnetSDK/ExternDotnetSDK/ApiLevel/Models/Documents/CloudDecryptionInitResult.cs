using Kontur.Extern.Client.ApiLevel.Models.Common;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Documents
{
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
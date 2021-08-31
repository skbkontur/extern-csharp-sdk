using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class EncryptedCertificate
    {
        /// <summary>
        /// Серийный номер сертификата электронной подписи
        /// </summary>
        public string SerialNumber { get; set; }
    }
}
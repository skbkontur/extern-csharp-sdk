using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class EncryptedCertificate
    {
        /// <summary>
        /// Серийный номер сертификата электронной подписи
        /// </summary>
        public string SerialNumber { get; set; } = null!;
    }
}
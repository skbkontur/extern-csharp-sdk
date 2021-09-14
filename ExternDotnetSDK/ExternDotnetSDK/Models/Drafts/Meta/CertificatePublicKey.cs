using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Drafts.Meta
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CertificatePublicKey
    {
        /// <summary>
        /// Публичная часть сертификата
        /// </summary>
        //[Required]
        public string Content { get; set; }
    }
}
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Drafts.Meta
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PublicKeyCertificate
    {
        /// <summary>
        /// Публичная часть сертификата
        /// </summary>
        //[Required]
        public byte[] Content { get; set; } = null!;
    }
}
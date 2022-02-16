using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PublicKeyCertificateRequest
    {
        /// <summary>
        /// Публичная часть сертификата
        /// </summary>
        //[JsonProperty(Required = Required.Always)]
        public byte[] Content { get; set; } = null!;
    }
}
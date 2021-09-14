using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CertificateRequest
    {
        /// <summary>
        /// Публичная часть сертификата
        /// </summary>
        //[JsonProperty(Required = Required.Always)]
        [JsonPropertyName("content")]
        public byte[] PublicKey { get; set; }
    }
}
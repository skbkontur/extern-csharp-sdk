using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class GenerateReplyDocumentRequest
    {
        /// <summary>
        /// Сертификат в формате base64
        /// </summary>
        //[JsonProperty(Required = Required.Always)]
        public byte[] CertificateBase64 { get; set; } = null!;
    }
}
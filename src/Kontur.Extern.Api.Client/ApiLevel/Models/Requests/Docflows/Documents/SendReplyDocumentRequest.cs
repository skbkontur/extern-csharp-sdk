using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class SendReplyDocumentRequest
    {
        /// <summary>
        /// IP адрес отправителя
        /// </summary>
        public string SenderIp { get; set; } = null!;
    }
}
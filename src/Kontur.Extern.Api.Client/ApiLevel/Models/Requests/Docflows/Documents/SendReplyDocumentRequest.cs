using System.Diagnostics.CodeAnalysis;
using System.Net;
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
        public IPAddress SenderIp { get; set; } = null!;
    }
}
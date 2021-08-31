using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class SendReplyDocumentRequest
    {
        /// <summary>
        /// IP адрес отправителя
        /// </summary>
        public string SenderIp { get; set; }
    }
}
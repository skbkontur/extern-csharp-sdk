using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Requests.Drafts.Signatures
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class SignatureRequest
    {
        /// <summary>
        /// Подпись документа в формате base64
        /// </summary>
        //[Required(AllowEmptyStrings = false)]
        public string Base64Content { get; set; }

        /// <summary>
        /// Принадлежность подписи третьей стороне, не участвующей в документообороте. По умолчанию <code>false</code>
        /// </summary>
        public bool IsThirdPartySignature { get; set; }
    }
}
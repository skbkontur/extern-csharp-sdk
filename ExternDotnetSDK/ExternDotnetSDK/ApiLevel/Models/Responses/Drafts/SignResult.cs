using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Responses.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class SignResult
    {
        /// <summary>
        /// Ссылки на подписанные документы
        /// </summary>
        public Link[] SignedDocuments { get; set; }
        
        public Link[] Links { get; set; }
    }
}
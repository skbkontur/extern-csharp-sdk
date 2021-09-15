using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class SignResult
    {
        /// <summary>
        /// Ссылки на подписанные документы
        /// </summary>
        public Link[] SignedDocuments { get; set; } = null!;

        public Link[] Links { get; set; } = null!;
    }
}
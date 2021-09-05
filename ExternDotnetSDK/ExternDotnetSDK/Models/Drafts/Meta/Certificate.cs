using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Drafts.Meta
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Certificate
    {
        /// <summary>
        ///     Публичная часть сертификата
        /// </summary>
        //[Required]
        public string Content { get; set; }
    }
}
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Drafts.Meta
{
    /// <summary>
    /// Реквизиты, специфичные для ЮЛ
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OrganizationInfo
    {
        /// <summary>
        /// КПП
        /// </summary>
        public Kpp? Kpp { get; set; }
    }
}
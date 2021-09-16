using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts
{
    /// <summary>
    /// Реквизиты, специфичные для ЮЛ
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OrganizationInfoRequest
    {
        /// <summary>
        /// КПП
        /// </summary>
        public Kpp? Kpp { get; set; }
    }
}
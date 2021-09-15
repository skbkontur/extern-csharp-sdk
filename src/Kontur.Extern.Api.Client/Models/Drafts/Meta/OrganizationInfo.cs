using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Drafts.Meta
{
    /// <summary>
    /// Реквизиты, специфичные для ЮЛ
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class OrganizationInfo
    {
        private string? kpp;

        /// <summary>
        /// КПП
        /// </summary>
        public string? Kpp
        {
            get => kpp;
            set => kpp = string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
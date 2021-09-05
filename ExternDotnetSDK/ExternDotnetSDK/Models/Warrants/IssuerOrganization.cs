#nullable enable
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Warrants
{
    /// <summary>
    /// Информация об организации - представляемом лице
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class IssuerOrganization
    {
        /// <summary>
        /// Название
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string? Inn { get; set; }

        /// <summary>
        /// КПП
        /// </summary>
        public string? Kpp { get; set; }
    }
}
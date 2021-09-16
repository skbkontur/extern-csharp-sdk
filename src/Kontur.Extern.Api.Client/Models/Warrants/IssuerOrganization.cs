using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Warrants
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
        public Kpp? Kpp { get; set; }
    }
}
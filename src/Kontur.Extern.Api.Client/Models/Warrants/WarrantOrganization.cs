using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Warrants
{
    /// <summary>
    /// Информация об организации из доверенности
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class WarrantOrganization
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

        /// <summary>
        /// ОГРН
        /// </summary>
        public string? Ogrn { get; set; }
    }
}
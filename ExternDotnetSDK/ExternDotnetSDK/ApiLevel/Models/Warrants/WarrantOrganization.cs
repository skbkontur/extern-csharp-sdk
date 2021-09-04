#nullable enable
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
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
        public string? Kpp { get; set; }

        /// <summary>
        /// ОГРН
        /// </summary>
        public string? Ogrn { get; set; }
    }
}
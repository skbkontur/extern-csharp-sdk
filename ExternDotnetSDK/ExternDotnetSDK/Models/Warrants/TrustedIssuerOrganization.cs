#nullable enable
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.Models.Warrants
{
    /// <summary>
    /// Информация об организации-передоверителе
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class TrustedIssuerOrganization : WarrantOrganization
    {
        /// <summary>
        /// Юридический  адрес
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// ФИО руководителя организации
        /// </summary>
        public Fio? ChiefFio { get; set; }

        /// <summary>
        /// ИНН руководителя организации
        /// </summary>
        public string? ChiefInn { get; set; }
    }
}
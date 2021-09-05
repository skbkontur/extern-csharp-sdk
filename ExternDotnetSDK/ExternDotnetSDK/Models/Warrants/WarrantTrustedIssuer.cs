#nullable enable
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Warrants
{
    /// <summary>
    /// Представитель, который выдал доверенность (передоверитель)
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class WarrantTrustedIssuer
    {
        /// <summary>
        /// Тип представителя
        /// </summary>
        public TrustedIssuerType Type { get; set; }

        /// <summary>
        /// Информация о частном лице или индивидуальном предпринимателе - представителе, выдавшем доверенность
        /// </summary>
        public WarrantIndividual? TrustedIssuerIndividual { get; set; }

        /// <summary>
        /// Информация об организации - представителе, выдавшем доверенность
        /// </summary>
        public TrustedIssuerOrganization? TrustedIssuerOrganization { get; set; }
    }
}
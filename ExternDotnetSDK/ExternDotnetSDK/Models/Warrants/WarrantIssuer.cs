#nullable enable
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Warrants
{
    /// <summary>
    /// Представляемое лицо
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class WarrantIssuer
    {        
        /// <summary>
        /// Информация об организации - представляемом лице
        /// </summary>
        public IssuerOrganization? IssuerOrganization { get; set; }
    }
}
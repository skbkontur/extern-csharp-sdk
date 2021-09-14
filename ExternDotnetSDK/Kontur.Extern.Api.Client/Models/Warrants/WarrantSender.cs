#nullable enable
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Warrants
{
    /// <summary>
    /// Уполномоченный представитель, на которого выдана доверенность (отправитель)
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class WarrantSender
    {
        /// <summary>
        /// Информация о частном лице или индивидуальном предпринимателе - уполномоченном представителе (отправителе)
        /// </summary>
        public WarrantIndividual? SenderIndividual { get; set; }

        /// <summary>
        /// Информация об организации - уполномоченном представителе (отправителе)
        /// </summary>
        public WarrantOrganization? SenderOrganization { get; set; }
    }
}
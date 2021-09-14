using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    /// <summary>
    /// Признак наличия запроса о предоставлении документов в письменном (бумажном) виде.
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum PaperDocumentsDeliveryType
    {
        /// <summary>
        /// Не требуется предоставления документов в письменном (бумажном) виде.
        /// </summary>
        No = 0,
        
        /// <summary>
        /// Выдать документы лично заявителю;
        /// </summary>
        ToApplicant,
        
        /// <summary>
        /// Выслать документы по почте
        /// </summary>
        ByPost
    }
}
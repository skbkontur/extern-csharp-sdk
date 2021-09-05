using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Drafts.Meta;
using Kontur.Extern.Client.Models.DraftsBuilders.Builders.Data;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Builders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderMeta : IDraftsBuilderMeta<DraftsBuilderData>
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        //[Required]
        public Sender Sender { get; set; }
        
        /// <summary>
        /// Налогоплательщик
        /// </summary>
        //[Required]
        public AccountInfo Payer { get; set; }
        
        /// <summary>
        /// Получатель
        /// </summary>
        //[Required]
        public RecipientInfo Recipient { get; set; }
        
        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        //[Required]
        public Urn BuilderType { get; set; }
        
        /// <summary>
        /// Данные, специфичные для указанного типа DraftsBuilder
        /// </summary>
        public DraftsBuilderData BuilderData { get; set; }
    }
}
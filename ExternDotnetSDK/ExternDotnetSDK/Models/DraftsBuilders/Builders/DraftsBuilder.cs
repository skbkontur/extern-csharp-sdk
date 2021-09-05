using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Builders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilder
    {
        /// <summary>
        /// Идентификатор DraftsBuilder
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Метаинформация DraftsBuilder
        /// </summary>
        public DraftsBuilderMeta Meta { get; set; }
        
        /// <summary>
        /// Статус DraftsBuilder
        /// </summary>
        public DraftsBuilderStatus Status { get; set; }
    }
}
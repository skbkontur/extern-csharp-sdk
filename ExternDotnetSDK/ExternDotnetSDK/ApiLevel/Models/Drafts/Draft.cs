using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Draft
    {
        /// <summary>
        /// Идентификатор черновика
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Ссылки на документообороты
        /// </summary>
        public Link[] Docflows { get; set; }
        
        /// <summary>
        /// Ссылки на документы черновика
        /// </summary>
        public Link[] Documents { get; set; }
        
        /// <summary>
        /// Метаинформация черновика
        /// </summary>
        public DraftMeta Meta { get; set; }
        
        /// <summary>
        /// Статус черновика
        /// </summary>
        public DraftStatus Status { get; set; }
        
        /// <summary>
        /// Ссылки для работы с черновиком
        /// </summary>
        public Link[] Links { get; set; }
    }
}
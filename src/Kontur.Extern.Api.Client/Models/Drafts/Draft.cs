using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Drafts.Meta;

namespace Kontur.Extern.Api.Client.Models.Drafts
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
        public Link[] Docflows { get; set; } = null!;
        
        /// <summary>
        /// Ссылки на документы черновика
        /// </summary>
        public Link[] Documents { get; set; } = null!;
        
        /// <summary>
        /// Метаинформация черновика
        /// </summary>
        public DraftMeta Meta { get; set; } = null!;
        
        /// <summary>
        /// Статус черновика
        /// </summary>
        public DraftStatus Status { get; set; }
        
        /// <summary>
        /// Ссылки для работы с черновиком
        /// </summary>
        public Link[] Links { get; set; } = null!;
    }
}
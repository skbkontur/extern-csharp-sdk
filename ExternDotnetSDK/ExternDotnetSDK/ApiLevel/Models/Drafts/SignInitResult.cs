using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class SignInitResult
    {
        public Link[] Links { get; set; }
        
        /// <summary>
        /// Ссылки на документы, для которых было инициировано подписание
        /// </summary>
        public Link[] DocumentsToSign { get; set; }
        
        /// <summary>
        /// Идентификатор запроса
        /// </summary>
        public string RequestId { get; set; }
        
        /// <summary>
        /// Идентификатор поставленной задачи ответного документа
        /// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid TaskId { get; set; }
        
        /// <summary>
        /// Способ подтверждения подписания
        /// </summary>
        public ConfirmTypeInternal ConfirmType { get; set; }
    }

    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum ConfirmTypeInternal
    {
        None,
        Sms,
        MyDSS,
        Applet,
    }
}
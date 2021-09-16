using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Events
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ApiEvent
    {
        /// <summary>
        /// ИНН организации
        /// </summary>
        public string? Inn { get; set; }
        
        /// <summary>
        /// КПП организации
        /// </summary>
        public Kpp? Kpp { get; set; }
        
        /// <summary>
        /// Тип документооборота
        /// </summary>
        public DocflowType DocflowType { get; set; }
        
        /// <summary>
        /// Ссылка на документооборот
        /// </summary>
        public Link? DocflowLink { get; set; }
        
        /// <summary>
        /// Тип события
        /// </summary>
        public Urn? NewState { get; set; }
        
        /// <summary>
        /// Дата и время появления события
        /// </summary>
        public DateTime EventDateTime { get; set; }
        
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public string Id { get; set; } = null!;

        /// <summary>
        /// Идентификатор документооборота
        /// </summary>
        public string DocflowId { get; set; } = null!;

        /// <summary>
        /// Идентификатор учетной записи пользователя
        /// </summary>
        public string AccountId { get; set; } = null!;

        /// <summary>
        /// Идентификатор связанного документооборота
        /// </summary>
        public string RelatedDocflowId { get; set; } = null!;

        /// <summary>
        /// Идентификатор документа в связанном документообороте (требование, письмо или отчет)
        /// </summary>
        public string RelatedDocumentId { get; set; } = null!;
    }
}
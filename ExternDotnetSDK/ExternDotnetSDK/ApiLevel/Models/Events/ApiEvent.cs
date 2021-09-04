using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Events
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ApiEvent
    {
        /// <summary>
        /// ИНН организации
        /// </summary>
        public string Inn { get; set; }
        
        /// <summary>
        /// КПП организации
        /// </summary>
        public string Kpp { get; set; }
        
        /// <summary>
        /// Тип документооборота
        /// </summary>
        public Urn DocflowType { get; set; }
        
        /// <summary>
        /// Ссылка на документооборот
        /// </summary>
        public Link DocflowLink { get; set; }
        
        /// <summary>
        /// Тип события
        /// </summary>
        public Urn NewState { get; set; }
        
        /// <summary>
        /// Дата и время появления события
        /// </summary>
        public DateTime EventDateTime { get; set; }
        
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Идентификатор документооборота
        /// </summary>
        public string DocflowId { get; set; }
        
        /// <summary>
        /// Идентификатор учетной записи пользователя
        /// </summary>
        public string AccountId { get; set; }
        
        /// <summary>
        /// Идентификатор связанного документооборота
        /// </summary>
        public string RelatedDocflowId { get; set; }
        
        /// <summary>
        /// Идентификатор документа в связанном документообороте (требование, письмо или отчет)
        /// </summary>
        public string RelatedDocumentId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions;
using Kontur.Extern.Client.ApiLevel.Models.Documents;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Docflow : IDocflow, IDocflowPageItem
    {
        public Docflow()
        {
        }

        public Docflow(
            Guid id,
            Guid organizationId,
            Urn type,
            Urn status,
            Urn successState,
            List<Document> documents,
            List<Link> links,
            DateTime sendDate,
            DateTime? lastChangeDate,
            DocflowDescription description)
        {
            Id = id;
            OrganizationId = organizationId;
            Type = type;
            Status = status;
            SuccessState = successState;
            Documents = documents;
            Links = links;
            SendDate = sendDate;
            LastChangeDate = lastChangeDate;
            Description = description;
        }
        
        /// <summary>
        /// Идентификатор документооборота
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Идентификатор организации
        /// </summary>
        public Guid OrganizationId { get; set; }
        
        /// <summary>
        /// Тип документооборота
        /// </summary>
        public Urn Type { get; set; }
        
        /// <summary>
        /// Статус документооборота.
        /// Подробнее о значении статуса читайте в [документации](https://docs-ke.readthedocs.io/ru/latest/specification/%D1%81%D1%82%D0%B0%D1%82%D1%83%D1%81%D1%8B%20%D0%94%D0%9E.html)
        /// </summary>
        public Urn Status { get; set; }
        
        /// <summary>
        /// Статус отчета (квитанция о приеме): принят или не принят
        /// </summary>
        public Urn SuccessState { get; set; }
        
        /// <summary>
        /// Список документов в документообороте
        /// </summary>
        public List<Document> Documents { get; set; }
        
        /// <summary>
        /// Ссылки для работы с документооборотом
        /// </summary>
        public List<Link> Links { get; set; }
        
        /// <summary>
        /// Дата и время отправки/получения для исходящего/входящего документооборота
        /// </summary>
        public DateTime SendDate { get; set; }
        
        /// <summary>
        /// Дата и время последнего изменения в документообороте
        /// </summary>
        public DateTime? LastChangeDate { get; set; }
        
        /// <summary>
        /// Дополнительные свойства документооборота
        /// </summary>
        public DocflowDescription Description { get; set; }
    }
}
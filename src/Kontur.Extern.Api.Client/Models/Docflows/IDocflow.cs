using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;

namespace Kontur.Extern.Api.Client.Models.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public interface IDocflow
    {
        /// <summary>
        /// Тип документооборота
        /// </summary>
        public DocflowType Type { get; }

        /// <summary>
        /// Дополнительные свойства документооборота
        /// </summary>
        public DocflowDescription Description { get; }
        
        /// <summary>
        /// Идентификатор документооборота
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Идентификатор абонента (техническое свойство)
        /// </summary>
        Guid AbonentId { get; }

        /// <summary>
        /// Идентификатор пользователя Экстерна (техническое свойство)
        /// </summary>
        Guid ExternUserId { get; }

        /// <summary>
        /// Идентификатор организации
        /// </summary>
        Guid OrganizationId { get; }
        
        /// <summary>
        /// Статус документооборота. Подробнее о значении статуса читайте в [документации](https://docs-ke.readthedocs.io/ru/latest/specification/%D1%81%D1%82%D0%B0%D1%82%D1%83%D1%81%D1%8B%20%D0%94%D0%9E.html)
        /// </summary>
        DocflowStatus Status { get; }
        
        /// <summary>
        /// Статус отчета (квитанция о приеме): принят или не принят
        /// </summary>
        DocflowState SuccessState { get; }
        
        /// <summary>
        /// Ссылки для работы с документооборотом
        /// </summary>
        List<Link> Links { get; }
        
        /// <summary>
        /// Дата и время отправки/получения для исходящего/входящего документооборота
        /// </summary>
        DateTime SendDateTime { get; }
        
        /// <summary>
        /// Дата и время последнего изменения в документообороте
        /// </summary>
        DateTime? LastChangeDateTime { get; }
    }
}
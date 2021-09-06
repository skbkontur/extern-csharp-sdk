using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.Models.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public interface IDocflowPageItem : IDocflowBase
    {
        /// <summary>
        /// Идентификатор документооборота
        /// </summary>
        Guid Id { get; }
        
        /// <summary>
        /// Идентификатор организации
        /// </summary>
        Guid OrganizationId { get; }
        
        /// <summary>
        /// Статус документооборота. Подробнее о значении статуса читайте в [документации](https://docs-ke.readthedocs.io/ru/latest/specification/%D1%81%D1%82%D0%B0%D1%82%D1%83%D1%81%D1%8B%20%D0%94%D0%9E.html)
        /// </summary>
        Urn Status { get; }
        
        /// <summary>
        /// Статус отчета (квитанция о приеме): принят или не принят
        /// </summary>
        Urn SuccessState { get; }
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
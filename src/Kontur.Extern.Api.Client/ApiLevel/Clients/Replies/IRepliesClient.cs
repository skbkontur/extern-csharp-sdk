using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Replies;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Replies
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    [ApiPathSection]
    public interface IRepliesClient
    {
        /// <summary>
        /// Получение ответного документа по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документ</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ReplyDocument> GetReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение ответного документа документооборота описи по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документ</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ReplyDocument> GetInventoryReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Создание ответного документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который формируется ответный документ</param>
        /// <param name="documentType">Тип генерируемого ответного документа</param>
        /// <param name="certificate">Сертификат</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ReplyDocument> GenerateReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            byte[] certificate,
            TimeSpan? timeout = null);

        /// <summary>
        /// Создание ответного документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который формируется ответный документ</param>
        /// <param name="documentType">Тип генерируемого ответного документа</param>
        /// <param name="declineNoticeErrorCodes">Коды причины отправки уведомления об отказе (используется при documentType = fns534-demand-acceptance-result-negative)</param>
        /// <param name="certificate">Сертификат</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ReplyDocument> GenerateReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            string[] declineNoticeErrorCodes,
            byte[] certificate,
            TimeSpan? timeout = null);

        /// <summary>
        /// Создание ответного документа документооборота описи
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа, на который формируется ответный документ</param>
        /// <param name="documentType">Тип ответного документа</param>
        /// <param name="certificate">Сертификат</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ReplyDocument> GenerateInventoryReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Urn documentType,
            byte[] certificate,
            TimeSpan? timeout = null);

        /// <summary>
        /// Отправка ответного документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документ</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="senderIp">IP адрес отправителя</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<IDocflowWithDocuments> SendReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            IPAddress senderIp,
            TimeSpan? timeout = null);

        /// <summary>
        /// Отправка ответного документа документооборота описи
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документ</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="senderIp">IP адрес отправителя</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<IDocflowWithDocuments> SendInventoryReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            IPAddress senderIp,
            TimeSpan? timeout = null);

        /// <summary>
        /// Добавление подписи к ответному документу
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документ</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="signature">Подпись</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ReplyDocument> UpdateReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null);

        /// <summary>
        /// Добавление подписи к ответному документу документооборота описи
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документ</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="signature">Подпись</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ReplyDocument> UpdateInventoryReplySignatureAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null);

        /// <summary>
        /// Обновление контента ответного документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документ</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="content">Контент ответного документа</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ReplyDocument> UpdateReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null);

        /// <summary>
        /// Обновление контента ответного документа документооборота описи
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документ</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="content">Контент ответного документа</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ReplyDocument> UpdateInventoryReplyContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null);
    }
}
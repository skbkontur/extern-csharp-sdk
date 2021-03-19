using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Documents;
using Kontur.Extern.Client.Models.Drafts;

namespace Kontur.Extern.Client.Clients.Replies
{
    public interface IRepliesClient
    {
        /// <summary>
        /// Получение ответного документа по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ApiReplyDocument> GetReplyAsync(
            Guid accountId,
            Guid docflowId,
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
        /// <param name="certificateContent">Сертификат</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ApiReplyDocument> GenerateReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent,
            TimeSpan? timeout = null);

        /// <summary>
        /// Создание ответного документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который формируется ответный документ</param>
        /// <param name="documentType">Тип генерируемого ответного документа</param>
        /// <param name="declineNoticeErrorCodes">Коды причины отправки уведомления об отказе (используется при documentType = fns534-demand-acceptance-result-negative)</param>
        /// <param name="certificateContent">Сертификат</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ApiReplyDocument> GenerateReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            string[] declineNoticeErrorCodes,
            byte[] certificateContent,
            TimeSpan? timeout = null);

        /// <summary>
        /// Отправка ответного документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документа</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="senderIp">IP адрес отправителя</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<Docflow> SendReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string senderIp,
            TimeSpan? timeout = null);

        /// <summary>
        /// Добавление подписи к ответному документу
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документа</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="signature">Подпись</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ApiReplyDocument> UpdateReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null);

        /// <summary>
        /// Обновление контента ответного документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документа</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="content">Контент ответного документа</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ApiReplyDocument> UpdateReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null);

        /// <summary>
        /// Подписание ответного документа сертификатом
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документа</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="timeout"></param>
        /// <returns>Задача подписания ответного документа</returns>
        Task<SignInitResult> DssSignReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Проверка статуса задачи подписания ответного документа по TaskId
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа, на который был сформирован ответный документа</param>
        /// <param name="replyId">Идентификатор ответного документа</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="timeout"></param>
        /// <returns>Задача подписания ответного документа</returns>
        Task<ApiTaskResult<CryptOperationStatusResult>> GetDssSignReplyTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            Guid taskId,
            TimeSpan? timeout = null);
    }
}
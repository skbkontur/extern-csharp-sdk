using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Documents;
using Kontur.Extern.Client.Models.Drafts;

namespace Kontur.Extern.Client.Clients.InventoryDocflows
{
    public interface IInventoryDocflowsClient
    {
        /// <summary>
        /// Поиск документооборотов описей
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор основного документа из связанного документооборота</param>
        /// <param name="filter">Параметры поиска</param>
        /// <param name="timeout"></param>
        /// <returns>Список документооборотов</returns>
        Task<DocflowPage> GetDocflowsAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter = null,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение документооборота описи по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="timeout"></param>
        /// <returns>Документооборот описи</returns>
        Task<Docflow> GetDocflowAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа, печатную форму которого нужно получить</param>
        /// <param name="contentId">Идентификатор расшифрованного документа в сервисе контентов</param>
        /// <param name="timeout"></param>
        /// <returns>Результат печати с идентификатором печатной формы документа в сервисе контентов</returns>
        Task<PrintDocumentResult> PrintDocumentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа, печатную форму которого нужно получить</param>
        /// <param name="contentId">Идентификатор расшифрованного документа в сервисе контентов</param>
        /// <param name="timeout"></param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа из документооборота описи</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="timeout"></param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> GetPrintDocumentTaskAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid taskId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение контента подписи документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа из документооборота описи</param>
        /// <param name="signatureId">Идентификатор подписи документа</param>
        /// <param name="timeout"></param>
        /// <returns>Контент подписи документа</returns>
        Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Создание ответного документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа от налогового органа, на который нужно сформировать ответный документ</param>
        /// <param name="documentType">Тип ответного документа</param>
        /// <param name="certificate">Сертификат</param>
        /// <param name="timeout"></param>
        /// <returns>Ответный документ</returns>
        Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Urn documentType,
            byte[] certificate,
            TimeSpan? timeout = null);

        Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] senderIpContent,
            TimeSpan? timeout = null);

        Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null);

        Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null);

        Task<ApiReplyDocument> GetDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null);

        Task<SignResult> ConfirmCloudSignDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            Guid requestId,
            string code,
            TimeSpan? timeout = null);

        Task<ApiTaskResult<CryptOperationStatusResult>> GetDocflowReplyDocumentTaskAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            Guid apiTaskId,
            TimeSpan? timeout = null);

        Task<SignInitResult> CloudSignDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            bool forceConfirmation = true,
            TimeSpan? timeout = null);

        Task<byte[]> ConfirmDocumentContentDecryptionAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid requestId,
            string code,
            bool unzip = false,
            TimeSpan? timeout = null);

        Task<CloudDecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            byte[] certificateContent,
            TimeSpan? timeout = null);
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Docflows;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;
using Kontur.Extern.Api.Client.Models.Docflows.DocumentsRequests;
using Microsoft.AspNetCore.JsonPatch;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Docflows
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    [ApiPathSection]
    public interface IDocflowsClient
    {
        /// <summary>
        /// Поиск документооборотов по заданным параметрам
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="filter">Параметры поиска</param>
        /// <param name="timeout"></param>
        /// <returns>Список документооборотов</returns>
        Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter? filter = null, TimeSpan? timeout = null);

        /// <summary>
        /// Получение связанных документооборотов
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Документооборот, к которому относится связанный документооборот</param>
        /// <param name="relatedDocumentId">Документ, к которому относится связанный документооборот</param>
        /// <param name="filter">Параметры поиска</param>
        /// <param name="timeout"></param>
        /// <returns>Список документооборотов</returns>
        Task<DocflowPage> GetRelatedDocflows(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter? filter = null,
            TimeSpan? timeout = null);

        /// <summary>
        /// Поиск документооборотов описей
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Документооборот, к которому относится документооборот описи</param>
        /// <param name="relatedDocumentId">Документ, к которому относится документооборот описи</param>
        /// <param name="filter">Параметры поиска</param>
        /// <param name="timeout"></param>
        /// <returns>Список документооборотов</returns>
        Task<DocflowPage> GetInventoryDocflowsAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter? filter = null,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение документооборота по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="timeout"></param>
        /// <returns>Документооборот</returns>
        Task<IDocflowWithDocuments> GetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение документооборота по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="timeout"></param>
        /// <returns>Документооборот или null, если документооброт с указанными ID не существует</returns>
        Task<IDocflowWithDocuments?> TryGetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение документооборота описи по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="timeout"></param>
        /// <returns>Документооборот описи</returns>
        Task<IDocflowWithDocuments> GetInventoryDocflowAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение документооборота описи по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="timeout"></param>
        /// <returns>Документооборот описи или null</returns>
        Task<IDocflowWithDocuments?> TryGetInventoryDocflowAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение всех документов в документообороте
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="timeout"></param>
        /// <returns>Список документов из документооборота</returns>
        Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение документа из документооборота по его идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Документ из документооборота</returns>
        Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение документа из документооборота по его идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Документ из документооборота или null</returns>
        Task<Document?> TryGetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null);


        /// <summary>
        ///  Изменение реквизитов документа требования
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="patch">Список операций для изменения реквизитов</param>
        /// <param name="timeout"></param>
        /// <returns>Документ из документооборота</returns>
        Task<Document> PatchDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, JsonPatchDocument<Document> patch, TimeSpan? timeout = null);

        /// <summary>
        /// Изменение реквизитов документооборота
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="patch">Список операций для изменения реквизитов</param>
        /// <param name="timeout"></param>
        /// <returns>Документооборот</returns>
        Task<IDocflowWithDocuments> PatchDocflowAsync(Guid accountId, Guid docflowId, JsonPatchDocument<IDocflowWithDocuments> patch, TimeSpan? timeout = null);

        /// <summary>
        /// Получение описания документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Описание документа</returns>
        Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение списка подписей документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Подписи документа</returns>
        Task<List<Signature>> GetDocumentSignaturesAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение подписи документа по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureId">Идентификатор подписи</param>
        /// <param name="timeout"></param>
        /// <returns>Подпись документа</returns>
        Task<Signature> GetSignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение контента подписи документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureId">Идентификатор подписи</param>
        /// <param name="timeout"></param>
        /// <returns>Контент подписи документа</returns>
        Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение контента подписи документа документооборота описи
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа из документооборота описи</param>
        /// <param name="signatureId">Идентификатор подписи документа</param>
        /// <param name="timeout"></param>
        /// <returns>Контент подписи документа</returns>
        Task<byte[]> GetInventorySignatureContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="contentId">Идентификатор расшифрованного документа в сервисе контентов</param>
        /// <param name="timeout"></param>
        /// <returns>Результат печати с идентификатором печатной формы документа в сервисе контентов</returns>
        Task<PrintDocumentResult> PrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid contentId, TimeSpan? timeout = null);

        /// <summary>
        /// Печать документа документооборота описи
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа, печатную форму которого нужно получить</param>
        /// <param name="contentId">Идентификатор расшифрованного документа в сервисе контентов</param>
        /// <param name="timeout"></param>
        /// <returns>Результат печати с идентификатором печатной формы документа в сервисе контентов</returns>
        Task<PrintDocumentResult> PrintInventoryDocumentAsync(
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
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="contentId">Идентификатор расшифрованного документа в сервисе контентов</param>
        /// <param name="timeout"></param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid contentId, TimeSpan? timeout = null);

        /// <summary>
        /// Печать документа документооборота описи
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа, печатную форму которого нужно получить</param>
        /// <param name="contentId">Идентификатор расшифрованного документа в сервисе контентов</param>
        /// <param name="timeout"></param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> StartPrintInventoryDocumentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Проверка статуса задачи печати по TaskId
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="timeout"></param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> GetPrintDocumentTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid taskId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Проверка статуса задачи печати документа документооборота описи по TaskId
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="relatedDocflowId">Идентификатор связанного документооборота</param>
        /// <param name="relatedDocumentId">Идентификатор документа из связанного документооборота</param>
        /// <param name="inventoryId">Идентификатор документооборота описи</param>
        /// <param name="documentId">Идентификатор документа из документооборота описи</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="timeout"></param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> GetPrintInventoryDocumentTaskAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid taskId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Распознавание требования
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа требования с типом fns534-demand-attachment</param>
        /// <param name="contentId"></param>
        /// <param name="timeout"></param>
        /// <returns>Распознанные данные из файла требования</returns>
        Task<RecognizeResult> RecognizeDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение запроса на получение входящих документов ФСС
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="requestId">Идентификатор запроса</param>
        /// <param name="timeout"></param>
        /// <returns>Запрос на получение входящих документов ФСС</returns>
        Task<DocumentsRequest> GetDocumentsRequestAsync(
            Guid accountId,
            Guid docflowId,
            Guid requestId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Создание запроса на получение входящих документов ФСС
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="certificate">Сертификат</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<DocumentsRequest> GenerateDocumentsRequestAsync(
            Guid accountId,
            Guid docflowId,
            byte[] certificate,
            TimeSpan? timeout = null);

        /// <summary>
        /// Отправка запроса на получение входящих документов ФСС
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="requestId">Идентификатор запроса</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<IDocflowWithDocuments> SendDocumentsRequestAsync(
            Guid accountId,
            Guid docflowId,
            Guid requestId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Добавление подписи к запросу на получение входящих документов ФСС
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="requestId">Идентификатор запроса</param>
        /// <param name="signature"></param>
        /// <param name="timeout">Сертификат</param>
        /// <returns></returns>
        Task<DocumentsRequest> UpdateDocumentsRequestSignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid requestId,
            byte[] signature,
            TimeSpan? timeout = null);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Docflows;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Documents;

namespace Kontur.Extern.Client.Engine.Services.Docflows
{
    public interface IDocflowsService
    {
        IDocflowsClient Client { get; }

        /// <summary>
        /// Поиск документооборотов по заданным параметрам
        /// </summary>
        /// <param name="filter">Параметры поиска</param>
        /// <returns>Список документооборотов</returns>
        Task<DocflowPage> GetDocflowsAsync(DocflowFilter filter = null);

        /// <summary>
        /// Получение связанных документооборотов
        /// </summary>
        /// <param name="relatedDocflowId">Документооборот, к которому относится связанный документооборот</param>
        /// <param name="relatedDocumentId">Документ, к которому относится связанный документооборот</param>
        /// <param name="filter">Параметры поиска</param>
        /// <returns>Список документооборотов</returns>
        Task<DocflowPage> GetRelatedDocflows(
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter = null);

        /// <summary>
        /// Получение документооборота по идентификатору
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <returns>Документооборот</returns>
        Task<Docflow> GetDocflowAsync(Guid docflowId);

        /// <summary>
        /// Получение всех документов в документообороте
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <returns>Список документов из документооборота</returns>
        Task<List<Document>> GetDocumentsAsync(Guid docflowId);

        /// <summary>
        /// Получение документа из документооборота по его идентификатору
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <returns>Документ из документооборота</returns>
        Task<Document> GetDocumentAsync( Guid docflowId, Guid documentId);

        /// <summary>
        /// Получение описания документа
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <returns>Описание документа</returns>
        Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid docflowId,
            Guid documentId
        );

        /// <summary>
        /// Получение списка подписей документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <returns>Подписи документа</returns>
        Task<List<Signature>> GetDocumentSignaturesAsync(
            Guid docflowId,
            Guid documentId);

        /// <summary>
        /// Получение подписи документа по идентификатору
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureId">Идентификатор подписи</param>
        /// <returns>Подпись документа</returns>
        Task<Signature> GetSignatureAsync(
            Guid docflowId,
            Guid documentId,
            Guid signatureId);

        /// <summary>
        /// Получение контента подписи документа
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureId">Идентификатор подписи</param>
        /// <returns>Контент подписи документа</returns>
        Task<byte[]> GetSignatureContentAsync(
            Guid docflowId,
            Guid documentId,
            Guid signatureId
        );

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="contentId">Идентификатор расшифрованного документа в сервисе контентов</param>
        /// <returns>Результат печати с идентификатором печатной формы документа в сервисе контентов</returns>
        Task<PrintDocumentResult> PrintDocumentAsync( Guid docflowId, Guid documentId, Guid contentId );

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="contentId">Идентификатор расшифрованного документа в сервисе контентов</param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync( Guid docflowId, Guid documentId, Guid contentId);

        /// <summary>
        /// Проверка статуса задачи печати по TaskId
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> GetPrintTaskAsync(
            Guid docflowId,
            Guid documentId,
            Guid taskId
        );

        /// <summary>
        /// Дешифрование контента документа Контур.Сертификатом
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="certificate">Сертификат</param>
        /// <returns>Инициировано дешифрование документа</returns>
        Task<CloudDecryptionInitResult> StartCloudDecryptDocumentAsync(
            Guid docflowId,
            Guid documentId,
            byte[] certificate
        );

        /// <summary>
        /// Подтверждение дешифрования документа Контур.Сертификатом
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="requestId">Идентификатор запроса, который инициировал дешифрование контента документа</param>
        /// <param name="code">Код из смс</param>
        /// <param name="unzip">Флаг распаковки архива. По умолчанию false, контент возвращается сжатым</param>
        /// <returns></returns>
        Task<DecryptDocumentResult> ConfirmDocumentDecryptionAsync(
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool? unzip = null
        );

        /// <summary>
        /// Дешифрование контента документа сертификатом DSS
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="certificate">Сертификат</param>
        /// <param name="unzip">Флаг необходимости распаковки архива. По умолчанию false, контент возвращается сжатым</param>
        /// <returns>Инициировано дешифрование документа</returns>
        Task<DssDecryptionInitResult> StartDssDecryptDocumentAsync(
            Guid docflowId,
            Guid documentId,
            byte[] certificate,
            bool? unzip = null
        );

        /// <summary>
        /// Проверка статуса задачи дешифрования по TaskId
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <returns>Задача дешифрования документа</returns>
        Task<ApiTaskResult<DecryptDocumentResult>> GetDssDecryptDocumentTaskAsync(
            Guid docflowId,
            Guid documentId,
            Guid taskId
        );

        /// <summary>
        /// Распознавание требования
        /// </summary>
        /// <param name="docflowId">Идентификатор документооборота</param>
        /// <param name="documentId">Идентификатор документа требования с типом fns534-demand-attachment</param>
        /// <param name="contentId"></param>
        /// <returns>Распознанные данные из файла требования</returns>
        Task<RecognizeResult> RecognizeDocumentAsync(
            Guid docflowId,
            Guid documentId,
            Guid contentId
        );
    }
}
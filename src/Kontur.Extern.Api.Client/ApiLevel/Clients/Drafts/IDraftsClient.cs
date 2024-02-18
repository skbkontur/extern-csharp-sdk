using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Signatures;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.ApiTasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Check;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Send;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Drafts;
using Kontur.Extern.Api.Client.Models.Drafts.Documents;
using Kontur.Extern.Api.Client.Models.Drafts.Meta;
using Kontur.Extern.Api.Client.Models.Drafts.Prepare;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public interface IDraftsClient
    {
        /// <summary>
        /// Создание черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="meta">Метаинформация черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Черновик</returns>
        Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest meta, TimeSpan? timeout = null);

        /// <summary>
        /// Получение черновика по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Черновик</returns>
        Task<Draft> GetDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);
        
        /// <summary>
        /// Получение черновика по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Черновик или null, если черновик с указанными идентификаторами не существует</returns>
        Task<Draft?> TryGetDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);

        /// <summary>
        /// Удаление черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Возвращает true, если черновик успешно удален; false, если черновик не существует.</returns>
        Task<bool> DeleteDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение метаинформации черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Метаинформация черновика</returns>
        Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);

        /// <summary>
        /// Редактирование метаинформации черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="meta">Метаинформация черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Метаинформация черновика</returns>
        Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, DraftMetaRequest meta, TimeSpan? timeout = null);

        /// <summary>
        /// Создание документа в черновике
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentRequest"></param>
        /// <param name="timeout"></param>
        /// <returns>Документ черновика</returns>
        Task<DraftDocument> CreateDocumentAsync(Guid accountId, Guid draftId, DocumentRequest documentRequest, TimeSpan? timeout = null);

        /// <summary>
        /// Получение документа в черновике
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Документ черновика</returns>
        Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);

        /// <summary>
        /// Удаление документа из черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Возвращает true, если документ успешно удален; false, если документ не существует.</returns>
        Task<bool> DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);

        /// <summary>
        /// Редактирование документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="documentRequest"></param>
        /// <param name="timeout"></param>
        /// <returns>Документ черновика</returns>
        Task<DraftDocument> UpdateDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentRequest documentRequest,
            TimeSpan? timeout = null);

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Печатная форма документа</returns>
        Task<byte[]> PrintDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);

        /// <summary>
        /// Печать документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Задача печати документа</returns>
        Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);

        /// <summary>
        /// Создание подписи документа в черновике
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureRequest"></param>
        /// <param name="timeout"></param>
        /// <returns>Подпись документа</returns>
        Task<Signature> CreateSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            SignatureRequest? signatureRequest = null,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение подписи документа в черновике
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureId">Идентификатор подписи</param>
        /// <param name="timeout"></param>
        /// <returns>Подпись документа</returns>
        Task<Signature> GetSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Удаление подписи документа в черновике
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureId">Идентификатор подписи</param>
        /// <param name="timeout"></param>
        /// <returns>Возвращает true, если подпись успешно удален; false, если подпись не существует.</returns>
        Task<bool> DeleteSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Редактирование подписи документа в черновике
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureId">Идентификатор подписи</param>
        /// <param name="signatureRequest"></param>
        /// <param name="timeout"></param>
        /// <returns>Подпись документа</returns>
        Task<Signature> UpdateSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest signatureRequest,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение содержимого подписи документа в черновике
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="signatureId">Идентификатор подписи</param>
        /// <param name="timeout"></param>
        /// <returns>Контент подписи документа в формате Base64</returns>
        Task<string> GetSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Проверка документов в черновике
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Результат проверки черновика</returns>
        Task<CheckResult> CheckDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);

        /// <summary>
        /// Проверка документов в черновике
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Задача проверки черновика</returns>
        Task<ApiTaskResult<CheckResult>> StartCheckDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение статуса задачи проверки черновика по <paramref name="taskId"/>
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="timeout"></param>
        /// <returns>Статус задачи проверки черновика</returns>
        Task<ApiTaskResult<CheckResult>> GetCheckDraftTaskStatusAsync(Guid accountId, Guid draftId, Guid taskId, TimeSpan? timeout = null);

        /// <summary>
        /// Отправка документов черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="force">Флаг отправки в ПФР отчета с ошибками</param>
        /// <param name="timeout"></param>
        /// <returns>Результат отправки черновика</returns>
        Task<IDocflowWithDocuments> SendDraftAsync(Guid accountId, Guid draftId, bool? force = null, TimeSpan? timeout = null);

        /// <summary>
        /// Отправка документов черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="force">Флаг отправки в ПФР отчета с ошибками</param>
        /// <param name="timeout"></param>
        /// <returns>Состояние созданной задачи отправки черновика. В случае успешного завершения возвращает созданный <see cref="Docflow">документооборот</see></returns>
        Task<ApiTaskResult<IDocflowWithDocuments, SendFailure>> StartSendDraftAsync(
            Guid accountId,
            Guid draftId,
            bool? force = null,
            TimeSpan? timeout = null);

        /// <summary>
        /// Проверка статуса задачи отправки черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="timeout"></param>
        /// <returns>Состояние задачи отправки черновика. В случае успешного завершения возвращает созданный <see cref="Docflow">документооборот</see></returns>
        Task<ApiTaskResult<IDocflowWithDocuments, SendFailure>> GetSendDraftTaskStatusAsync(Guid accountId, Guid draftId, Guid taskId, TimeSpan? timeout = null);

        /// <summary>
        /// Создание файла в документе по контракту (УСН, ИОН, ЭДОК)
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="type">Тип документа</param>
        /// <param name="version"></param>
        /// <param name="contract">JSON-контракт для создания документа</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task BuildDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentFormatType type,
            int? version,
            string contract,
            TimeSpan? timeout = null);

        /// <summary>
        /// Создание документа по контракту (УСН, ИОН, ЭДОК)
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="type">Тип документа</param>
        /// <param name="version">Версия документа</param>
        /// <param name="contract">JSON-контракт для создания документа</param>
        /// <param name="timeout"></param>
        /// <returns>Документ черновика</returns>
        Task<DraftDocument> BuildDocumentAsync(
            Guid accountId,
            Guid draftId,
            DocumentFormatType type,
            int? version,
            string contract,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение списка задач черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="skip">Число пропускаемых элементов</param>
        /// <param name="take">Число элементов в возвращаемом списке</param>
        /// <param name="includeReleased">Показать завершенные задачи</param>
        /// <param name="timeout"></param>
        /// <returns>Список задач</returns>
        Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            int? skip = null,
            int? take = null,
            bool? includeReleased = null,
            TimeSpan? timeout = null);

        /// <summary>
        /// Подготовка черновика к отправке
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        Task<PrepareResult> PrepareDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);
    }
}
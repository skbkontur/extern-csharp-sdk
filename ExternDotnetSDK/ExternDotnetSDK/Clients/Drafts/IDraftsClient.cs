﻿using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Drafts;
using Kontur.Extern.Client.Models.Drafts.Check;
using Kontur.Extern.Client.Models.Drafts.Meta;
using Kontur.Extern.Client.Models.Drafts.Prepare;
using Kontur.Extern.Client.Models.Drafts.Requests;

namespace Kontur.Extern.Client.Clients.Drafts
{
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
        /// Удаление черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task DeleteDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);

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
        /// <returns></returns>
        Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);

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
            SignatureRequest signatureRequest = null,
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
        /// <returns></returns>
        Task DeleteSignatureAsync(
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
        /// <returns>Контент подписи документа</returns>
        Task<byte[]> GetSignatureContentAsync(
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
        /// Подготовка документов в черновике к отправке
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Результат подготовки черновика</returns>
        Task<PrepareResult> PrepareDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);

        /// <summary>
        /// Подготовка документов в черновике к отправке
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Задача подготовки черновика</returns>
        Task<ApiTaskResult<PrepareResult>> StartPrepareDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);

        /// <summary>
        /// Отправка документов черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="force">Флаг отправки в ПФР отчета с ошибками</param>
        /// <param name="timeout"></param>
        /// <returns>Результат отправки черновика</returns>
        Task<Docflow> SendDraftAsync(Guid accountId, Guid draftId, bool? force = null, TimeSpan? timeout = null);

        /// <summary>
        /// Отправка документов черновика
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="force">Флаг отправки в ПФР отчета с ошибками</param>
        /// <param name="timeout"></param>
        /// <returns>Задача отправки черновика</returns>
        Task<ApiTaskResult<Docflow>> StartSendDraftAsync(
            Guid accountId,
            Guid draftId,
            bool? force = null,
            TimeSpan? timeout = null);

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
            FormatType type,
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
            FormatType type,
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
        /// Подписание документов в черновике
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="timeout"></param>
        /// <returns>Задача подписания</returns>
        Task<SignInitResult> DssSignAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);

        /// <summary>
        /// Проверка статуса задачи подписания по TaskId
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftId">Идентификатор черновика</param>
        /// <param name="taskId">Идентификатор задачи</param>
        /// <param name="timeout"></param>
        /// <returns>Задача подписания</returns>
        Task<ApiTaskResult<CryptOperationStatusResult>> GetDssSignTask(
            Guid accountId,
            Guid draftId,
            Guid taskId,
            TimeSpan? timeout = null);
    }
}
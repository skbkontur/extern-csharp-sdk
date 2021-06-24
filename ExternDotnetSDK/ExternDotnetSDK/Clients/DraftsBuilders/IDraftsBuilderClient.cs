using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Client.Models.DraftsBuilders.DocumentFiles;
using Kontur.Extern.Client.Models.DraftsBuilders.Documents;

namespace Kontur.Extern.Client.Clients.DraftsBuilders
{
    public interface IDraftsBuilderClient
    {
        /// <summary>
        /// Создание DraftsBuilder
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="meta">Метаинформация DraftsBuilder</param>
        /// <param name="timeout"></param>
        /// <returns>DraftsBuilder</returns>
        Task<DraftsBuilder> CreateDraftsBuilderAsync(Guid accountId, DraftsBuilderMetaRequest meta, TimeSpan? timeout = null);

        /// <summary>
        /// Получение DraftsBuilder по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="timeout"></param>
        /// <returns>DraftsBuilder</returns>
        Task<DraftsBuilder> GetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null);

        /// <summary>
        /// Удаление DraftsBuilder
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task DeleteDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение метаинформации DraftsBuilder
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="timeout"></param>
        /// <returns>Метаинформация DraftsBuilder</returns>
        Task<DraftsBuilderMeta> GetDraftsBuilderMetaAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null);

        /// <summary>
        /// Редактирование метаинформации DraftsBuilder
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="meta">Метаинформация DraftsBuilder</param>
        /// <param name="timeout"></param>
        /// <returns>Метаинформация DraftsBuilder</returns>
        Task<DraftsBuilderMeta> UpdateDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null);

        /// <summary>
        /// Сборка содержимого DraftsBuilder в черновик
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="timeout"></param>
        /// <returns>Результат сборки</returns>
        Task<DraftsBuilderBuildResult> BuildDraftsAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null);

        /// <summary>
        /// Сборка содержимого DraftsBuilder в черновик
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="timeout"></param>
        /// <returns>Задача сборки</returns>
        Task<ApiTaskResult<DraftsBuilderBuildResult>> StartBuildDraftsAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Проверка статуса задачи сборки DraftsBuilder
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="taskId">Идентификатор задачи сборки</param>
        /// <param name="timeout"></param>
        /// <returns>Задача сборки</returns>
        Task<ApiTaskResult<DraftsBuilderBuildResult>> GetBuildDraftsTaskAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid taskId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Создание документа в DraftsBuilder
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="meta"></param>
        /// <param name="timeout"></param>
        /// <returns>Документ DraftsBuilder</returns>
        Task<DraftsBuilderDocument> CreateDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderDocumentMetaRequest meta,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение списка документов DraftsBuilder
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="timeout"></param>
        /// <returns>Список документов DraftsBuilder</returns>
        Task<IReadOnlyCollection<DraftsBuilderDocument>> GetDocumentsAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение документа по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Документ DraftsBuilder</returns>
        Task<DraftsBuilderDocument> GetDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Удаление документа в DraftBuilder
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task DeleteDocumentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId, TimeSpan? timeout = null);

        /// <summary>
        /// Получение метаинформации документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="timeout"></param>
        /// <returns>Метаинформация документа</returns>
        Task<DraftsBuilderDocumentMeta> GetDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Редактирование метаинформации документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа</param>
        /// <param name="meta">Метаинформация документа</param>
        /// <param name="timeout"></param>
        /// <returns>Метаинформация документа</returns>
        Task<DraftsBuilderDocumentMeta> UpdateDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null);

        /// <summary>
        /// Создание файла в документе
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа в DraftsBuilder</param>
        /// <param name="fileRequest"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<DraftsBuilderDocumentFile> CreateFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderFileRequest fileRequest,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение списка файлов в документе DraftsBuilder
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа в DraftsBuilder</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<DraftsBuilderDocumentFile>> GetFilesAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение файла по идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа в DraftsBuilder</param>
        /// <param name="fileId">Идентификатор файла в документе</param>
        /// <param name="timeout"></param>
        /// <returns>Файл DraftsBuilder</returns>
        Task<DraftsBuilderDocumentFile> GetFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Редактирование файла и подписи в документе
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа в DraftsBuilder</param>
        /// <param name="fileId">Идентификатор файла в документе</param>
        /// <param name="fileRequest"></param>
        /// <param name="timeout"></param>
        /// <returns>Метаинформация файла</returns>
        Task<DraftsBuilderDocumentFile> UpdateFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderFileRequest fileRequest,
            TimeSpan? timeout = null);

        /// <summary>
        /// Удаление файла документа
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа в DraftsBuilder</param>
        /// <param name="fileId">Идентификатор файла в документе</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        Task DeleteFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение метаинформации файла
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа в DraftsBuilder</param>
        /// <param name="fileId">Идентификатор файла в документе</param>
        /// <param name="timeout"></param>
        /// <returns>Метаинформация файла</returns>
        Task<DraftsBuilderDocumentFileMeta> GetFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null);

        /// <summary>
        /// Редактирование метаинформации файла
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа в DraftsBuilder</param>
        /// <param name="fileId">Идентификатор файла в документе</param>
        /// <param name="meta">Метаинформация файла</param>
        /// <param name="timeout"></param>
        /// <returns>Метаинформация файла</returns>
        Task<DraftsBuilderDocumentFileMeta> UpdateFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderFileMetaRequest meta,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение подписи файла
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="draftsBuilderId">Идентификатор DraftsBuilder</param>
        /// <param name="documentId">Идентификатор документа в DraftsBuilder</param>
        /// <param name="fileId">Идентификатор файла в документе</param>
        /// <param name="timeout"></param>
        /// <returns>Контент подписи</returns>
        Task<byte[]> GetSignatureAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null);
    }
}
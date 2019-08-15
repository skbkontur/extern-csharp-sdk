using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.DraftsBuilders.Builders;
using ExternDotnetSDK.Models.DraftsBuilders.DocumentFiles;
using ExternDotnetSDK.Models.DraftsBuilders.Documents;
using Refit;

namespace ExternDotnetSDK.Clients.DraftsBuilders
{
    public class DraftsBuilderClient : InnerCommonClient, IDraftsBuilderClient
    {
        public DraftsBuilderClient(ILogError logError, HttpClient client)
            : base(logError, client)
            => ClientRefit = RestService.For<IDraftsBuildersClientRefit>(client);

        public IDraftsBuildersClientRefit ClientRefit { get; }

        public async Task<DraftsBuilder> CreateDraftsBuilderAsync(Guid accountId, DraftsBuilderMetaRequest meta) =>
            await TryExecuteTask(ClientRefit.CreateDraftsBuilderAsync(accountId, meta));

        public async Task DeleteDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId) =>
            await TryExecuteTask(ClientRefit.DeleteDraftsBuilderAsync(accountId, draftsBuilderId));

        public async Task<DraftsBuilder> GetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId) =>
            await TryExecuteTask(ClientRefit.GetDraftsBuilderAsync(accountId, draftsBuilderId));

        public async Task<DraftsBuilderMeta> GetDraftsBuilderMetaAsync(Guid accountId, Guid draftsBuilderId) =>
            await TryExecuteTask(ClientRefit.GetDraftsBuilderMetaAsync(accountId, draftsBuilderId));

        public async Task<DraftsBuilderMeta> UpdateDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderMetaRequest meta) =>
            await TryExecuteTask(ClientRefit.UpdateDraftsBuilderMetaAsync(accountId, draftsBuilderId, meta));

        public async Task<DraftsBuilderBuildResult> BuildDraftsAsync(Guid accountId, Guid draftsBuilderId) =>
            await TryExecuteTask(ClientRefit.BuildDraftsAsync(accountId, draftsBuilderId));

        public async Task<ApiTaskResult<DraftsBuilderBuildResult>>
            BuildDeferredDraftsAsync(Guid accountId, Guid draftsBuilderId) =>
            await TryExecuteTask(ClientRefit.BuildDeferredDraftsAsync(accountId, draftsBuilderId));

        public async Task<ApiTaskResult<DraftsBuilderBuildResult>> GetBuildResultAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid apiTaskId) => await TryExecuteTask(ClientRefit.GetBuildResultAsync(accountId, draftsBuilderId, apiTaskId));

        public async Task<DraftsBuilderDocumentFile[]> GetDraftsBuilderDocumentFilesAsync(
            Guid accountId,
            Guid draftsBuildersId,
            Guid documentId) => await TryExecuteTask(
            ClientRefit.GetDraftsBuilderDocumentFilesAsync(accountId, draftsBuildersId, documentId));

        public async Task<DraftsBuilderDocumentFile> CreateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderDocumentFileContents contents) =>
            await TryExecuteTask(
                ClientRefit.CreateDraftsBuilderDocumentFileAsync(accountId, draftsBuilderId, documentId, contents));

        public async Task DeleteDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await TryExecuteTask(
                ClientRefit.DeleteDraftsBuilderDocumentFileAsync(accountId, draftsBuilderId, documentId, fileId));

        public async Task<DraftsBuilderDocumentFile> GetDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) => await TryExecuteTask(
            ClientRefit.GetDraftsBuilderDocumentFileAsync(accountId, draftsBuilderId, documentId, fileId));

        public async Task<DraftsBuilderDocumentFile> UpdateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderDocumentFileContents contents) => await TryExecuteTask(
            ClientRefit.UpdateDraftsBuilderDocumentFileAsync(
                accountId,
                draftsBuilderId,
                documentId,
                fileId,
                contents));

        public async Task<string> GetDraftsBuilderDocumentFileContentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await TryExecuteTask(
                ClientRefit.GetDraftsBuilderDocumentFileContentAsync(accountId, draftsBuilderId, documentId, fileId));

        public async Task<string> GetDraftsBuilderDocumentFileSignatureAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await TryExecuteTask(
                ClientRefit.GetDraftsBuilderDocumentFileSignatureAsync(accountId, draftsBuilderId, documentId, fileId));

        public async Task<DraftsBuilderDocumentFileMeta> GetDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await TryExecuteTask(
                ClientRefit.GetDraftsBuilderDocumentFileMetaAsync(accountId, draftsBuilderId, documentId, fileId));

        public async Task<DraftsBuilderDocumentFileMeta> UpdateDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderDocumentFileMetaRequest meta) =>
            await TryExecuteTask(
                ClientRefit.UpdateDraftsBuilderDocumentFileMetaAsync(accountId, draftsBuilderId, documentId, fileId, meta));

        public async Task<DraftsBuilderDocument[]> GetDraftsBuilderDocumentsAsync(Guid accountId, Guid draftsBuilderId) =>
            await TryExecuteTask(ClientRefit.GetDraftsBuilderDocumentsAsync(accountId, draftsBuilderId));

        public async Task<DraftsBuilderDocument> CreateDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderDocumentMetaRequest meta) =>
            await TryExecuteTask(ClientRefit.CreateDraftsBuilderDocumentAsync(accountId, draftsBuilderId, meta));

        public async Task DeleteDraftsBuilderDocumentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.DeleteDraftsBuilderDocumentAsync(accountId, draftsBuilderId, documentId));

        public async Task<DraftsBuilderDocument> GetDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId) =>
            await TryExecuteTask(ClientRefit.GetDraftsBuilderDocumentAsync(accountId, draftsBuilderId, documentId));

        public async Task<DraftsBuilderDocumentMeta> GetDraftsBuilderDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId) => await TryExecuteTask(
            ClientRefit.GetDraftsBuilderDocumentMetaAsync(accountId, draftsBuilderId, documentId));

        public async Task<DraftsBuilderDocumentMeta> UpdateDraftsBuilderDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderMetaRequest meta) =>
            await TryExecuteTask(ClientRefit.UpdateDraftsBuilderDocumentMetaAsync(accountId, draftsBuilderId, documentId, meta));
    }
}
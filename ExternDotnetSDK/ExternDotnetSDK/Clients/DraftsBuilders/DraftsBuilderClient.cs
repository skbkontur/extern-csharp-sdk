using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.DraftsBuilders.Builders;
using ExternDotnetSDK.Models.DraftsBuilders.DocumentFiles;
using ExternDotnetSDK.Models.DraftsBuilders.Documents;
using Refit;

namespace ExternDotnetSDK.Clients.DraftsBuilders
{
    public class DraftsBuilderClient : IDraftsBuilderClient
    {
        public DraftsBuilderClient(HttpClient client) => ClientRefit = RestService.For<IDraftsBuildersClientRefit>(client);

        public IDraftsBuildersClientRefit ClientRefit { get; }

        public async Task<DraftsBuilder> CreateDraftsBuilderAsync(Guid accountId, DraftsBuilderMetaRequest meta) =>
            await ClientRefit.CreateDraftsBuilderAsync(accountId, meta);

        public async Task DeleteDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId) =>
            await ClientRefit.DeleteDraftsBuilderAsync(accountId, draftsBuilderId);

        public async Task<DraftsBuilder> GetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId) =>
            await ClientRefit.GetDraftsBuilderAsync(accountId, draftsBuilderId);

        public async Task<DraftsBuilderMeta> GetDraftsBuilderMetaAsync(Guid accountId, Guid draftsBuilderId) =>
            await ClientRefit.GetDraftsBuilderMetaAsync(accountId, draftsBuilderId);

        public async Task<DraftsBuilderMeta> UpdateDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderMetaRequest meta) => await ClientRefit.UpdateDraftsBuilderMetaAsync(accountId, draftsBuilderId, meta);

        public async Task<DraftsBuilderBuildResult> BuildDraftsAsync(Guid accountId, Guid draftsBuilderId) =>
            await ClientRefit.BuildDraftsAsync(accountId, draftsBuilderId);

        public async Task<ApiTaskResult<DraftsBuilderBuildResult>> BuildDeferredDraftsAsync(Guid accountId, Guid draftsBuilderId) =>
            await ClientRefit.BuildDeferredDraftsAsync(accountId, draftsBuilderId);

        public async Task<ApiTaskResult<DraftsBuilderBuildResult>> GetBuildResultAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid apiTaskId) => await ClientRefit.GetBuildResultAsync(accountId, draftsBuilderId, apiTaskId);

        public async Task<DraftsBuilderDocumentFile[]> GetDraftsBuilderDocumentFilesAsync(
            Guid accountId,
            Guid draftsBuildersId,
            Guid documentId) => await ClientRefit.GetDraftsBuilderDocumentFilesAsync(accountId, draftsBuildersId, documentId);

        public async Task<DraftsBuilderDocumentFile> CreateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderDocumentFileContents contents) =>
            await ClientRefit.CreateDraftsBuilderDocumentFileAsync(accountId, draftsBuilderId, documentId, contents);

        public async Task DeleteDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await ClientRefit.DeleteDraftsBuilderDocumentFileAsync(accountId, draftsBuilderId, documentId, fileId);

        public async Task<DraftsBuilderDocumentFile> GetDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) => await ClientRefit.GetDraftsBuilderDocumentFileAsync(accountId, draftsBuilderId, documentId, fileId);

        public async Task<DraftsBuilderDocumentFile> UpdateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderDocumentFileContents contents) => await ClientRefit.UpdateDraftsBuilderDocumentFileAsync(
            accountId,
            draftsBuilderId,
            documentId,
            fileId,
            contents);

        public async Task<string> GetDraftsBuilderDocumentFileContentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await ClientRefit.GetDraftsBuilderDocumentFileContentAsync(accountId, draftsBuilderId, documentId, fileId);

        public async Task<string> GetDraftsBuilderDocumentFileSignatureAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await ClientRefit.GetDraftsBuilderDocumentFileSignatureAsync(accountId, draftsBuilderId, documentId, fileId);

        public async Task<DraftsBuilderDocumentFileMeta> GetDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await ClientRefit.GetDraftsBuilderDocumentFileMetaAsync(accountId, draftsBuilderId, documentId, fileId);

        public async Task<DraftsBuilderDocumentFileMeta> UpdateDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderDocumentFileMetaRequest meta) =>
            await ClientRefit.UpdateDraftsBuilderDocumentFileMetaAsync(accountId, draftsBuilderId, documentId, fileId, meta);

        public async Task<DraftsBuilderDocument[]> GetDraftsBuilderDocumentsAsync(Guid accountId, Guid draftsBuilderId) =>
            await ClientRefit.GetDraftsBuilderDocumentsAsync(accountId, draftsBuilderId);

        public async Task<DraftsBuilderDocument> CreateDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderDocumentMetaRequest meta) =>
            await ClientRefit.CreateDraftsBuilderDocumentAsync(accountId, draftsBuilderId, meta);

        public async Task DeleteDraftsBuilderDocumentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId) =>
            await ClientRefit.DeleteDraftsBuilderDocumentAsync(accountId, draftsBuilderId, documentId);

        public async Task<DraftsBuilderDocument> GetDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId) => await ClientRefit.GetDraftsBuilderDocumentAsync(accountId, draftsBuilderId, documentId);

        public async Task<DraftsBuilderDocumentMeta> GetDraftsBuilderDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId) => await ClientRefit.GetDraftsBuilderDocumentMetaAsync(accountId, draftsBuilderId, documentId);

        public async Task<DraftsBuilderDocumentMeta> UpdateDraftsBuilderDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderMetaRequest meta) =>
            await ClientRefit.UpdateDraftsBuilderDocumentMetaAsync(accountId, draftsBuilderId, documentId, meta);
    }
}
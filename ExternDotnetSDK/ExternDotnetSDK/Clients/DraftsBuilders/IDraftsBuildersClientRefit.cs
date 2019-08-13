using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.DraftsBuilders.Builders;
using ExternDotnetSDK.Models.DraftsBuilders.DocumentFiles;
using ExternDotnetSDK.Models.DraftsBuilders.Documents;
using Refit;

namespace ExternDotnetSDK.Clients.DraftsBuilders
{
    //todo Cover all these methods with tests. Use KeApiClient for that.
    public interface IDraftsBuildersClientRefit
    {
        [Post("/v1/{accountId}/drafts/builders")]
        Task<DraftsBuilder> CreateDraftsBuilderAsync(Guid accountId, [Body] DraftsBuilderMetaRequest meta);

        [Delete("/v1/{accountId}/drafts/builders/{draftsBuilderId}")]
        Task DeleteDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}")]
        Task<DraftsBuilder> GetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta")]
        Task<DraftsBuilderMeta> GetDraftsBuilderMetaAsync(Guid accountId, Guid draftsBuilderId);

        [Put("/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta")]
        Task<DraftsBuilderMeta> UpdateDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            [Body] DraftsBuilderMetaRequest meta);

        [Post("/v1/{accountId}/drafts/builders/{draftsBuilderId}/build?deferred=false")]
        Task<DraftsBuilderBuildResult> BuildDraftsAsync(Guid accountId, Guid draftsBuilderId);

        [Post("/v1/{accountId}/drafts/builders/{draftsBuilderId}/build?deferred=true")]
        Task<ApiTaskResult<DraftsBuilderBuildResult>> BuildDeferredDraftsAsync(Guid accountId, Guid draftsBuilderId);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}/tasks/{apiTaskId}")]
        Task<ApiTaskResult<DraftsBuilderBuildResult>> GetBuildResultAsync(Guid accountId, Guid draftsBuilderId, Guid apiTaskId);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files")]
        Task<DraftsBuilderDocumentFile[]> GetDraftsBuilderDocumentFilesAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId);

        [Post("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files")]
        Task<DraftsBuilderDocumentFile> CreateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            [Body] DraftsBuilderDocumentFileContents contents);

        [Delete("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}")]
        Task DeleteDraftsBuilderDocumentFileAsync(Guid accountId, Guid draftsBuilderId, Guid documentId, Guid fileId);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}")]
        Task<DraftsBuilderDocumentFile> GetDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId);

        [Put("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}")]
        Task<DraftsBuilderDocumentFile> UpdateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            [Body] DraftsBuilderDocumentFileContents contents);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/content")]
        Task<string> GetDraftsBuilderDocumentFileContentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId, Guid fileId);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/signature")]
        Task<string> GetDraftsBuilderDocumentFileSignatureAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta")]
        Task<DraftsBuilderDocumentFileMeta> GetDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId);

        [Put("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta")]
        Task<DraftsBuilderDocumentFileMeta> UpdateDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            [Body] DraftsBuilderDocumentFileMetaRequest meta);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents")]
        Task<DraftsBuilderDocument[]> GetDraftsBuilderDocumentsAsync(Guid accountId, Guid draftsBuilderId);

        [Post("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents")]
        Task<DraftsBuilderDocument> CreateDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            [Body] DraftsBuilderDocumentMetaRequest meta);

        [Delete("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}")]
        Task DeleteDraftsBuilderDocumentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}")]
        Task<DraftsBuilderDocument> GetDraftsBuilderDocumentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId);

        [Get("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta")]
        Task<DraftsBuilderDocumentMeta> GetDraftsBuilderDocumentMetaAsync(Guid accountId, Guid draftsBuilderId, Guid documentId);

        [Put("/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta")]
        Task<DraftsBuilderDocumentMeta> UpdateDraftsBuilderDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            [Body] DraftsBuilderMetaRequest meta);
    }
}
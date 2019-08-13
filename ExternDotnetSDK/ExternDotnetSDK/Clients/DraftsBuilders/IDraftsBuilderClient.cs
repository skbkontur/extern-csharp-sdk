using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.DraftsBuilders.Builders;
using ExternDotnetSDK.Models.DraftsBuilders.DocumentFiles;
using ExternDotnetSDK.Models.DraftsBuilders.Documents;

namespace ExternDotnetSDK.Clients.DraftsBuilders
{
    public interface IDraftsBuilderClient
    {
        IDraftsBuildersClientRefit ClientRefit { get; }

        Task<DraftsBuilder> CreateDraftsBuilderAsync(Guid accountId, DraftsBuilderMetaRequest meta);
        Task DeleteDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId);
        Task<DraftsBuilder> GetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId);
        Task<DraftsBuilderMeta> GetDraftsBuilderMetaAsync(Guid accountId, Guid draftsBuilderId);
        Task<DraftsBuilderMeta> UpdateDraftsBuilderMetaAsync(Guid accountId, Guid draftsBuilderId, DraftsBuilderMetaRequest meta);
        Task<DraftsBuilderBuildResult> BuildDraftsAsync(Guid accountId, Guid draftsBuilderId);
        Task<ApiTaskResult<DraftsBuilderBuildResult>> BuildDeferredDraftsAsync(Guid accountId, Guid draftsBuilderId);
        Task<ApiTaskResult<DraftsBuilderBuildResult>> GetBuildResultAsync(Guid accountId, Guid draftsBuilderId, Guid apiTaskId);

        Task<DraftsBuilderDocumentFile[]> GetDraftsBuilderDocumentFilesAsync(
            Guid accountId,
            Guid draftsBuildersId,
            Guid documentId);

        Task<DraftsBuilderDocumentFile> CreateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderDocumentFileContents contents);

        Task DeleteDraftsBuilderDocumentFileAsync(Guid accountId, Guid draftsBuilderId, Guid documentId, Guid fileId);

        Task<DraftsBuilderDocumentFile> GetDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId);

        Task<DraftsBuilderDocumentFile> UpdateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderDocumentFileContents contents);

        Task<string> GetDraftsBuilderDocumentFileContentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId, Guid fileId);

        Task<string> GetDraftsBuilderDocumentFileSignatureAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId);

        Task<DraftsBuilderDocumentFileMeta> GetDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId);

        Task<DraftsBuilderDocumentFileMeta> UpdateDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderDocumentFileMetaRequest meta);

        Task<DraftsBuilderDocument[]> GetDraftsBuilderDocumentsAsync(Guid accountId, Guid draftsBuilderId);

        Task<DraftsBuilderDocument> CreateDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderDocumentMetaRequest meta);

        Task DeleteDraftsBuilderDocumentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId);
        Task<DraftsBuilderDocument> GetDraftsBuilderDocumentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId);
        Task<DraftsBuilderDocumentMeta> GetDraftsBuilderDocumentMetaAsync(Guid accountId, Guid draftsBuilderId, Guid documentId);

        Task<DraftsBuilderDocumentMeta> UpdateDraftsBuilderDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderMetaRequest meta);
    }
}
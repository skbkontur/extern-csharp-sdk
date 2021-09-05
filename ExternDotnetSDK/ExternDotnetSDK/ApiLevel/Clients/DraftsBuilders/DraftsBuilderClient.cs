using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Requests.DraftBulders.Builders;
using Kontur.Extern.Client.ApiLevel.Models.Requests.DraftBulders.DocumentFiles;
using Kontur.Extern.Client.ApiLevel.Models.Requests.DraftBulders.Documents;
using Kontur.Extern.Client.ApiLevel.Models.Responses.DraftBuilders.Builders;
using Kontur.Extern.Client.Http;
using Kontur.Extern.Client.Models.ApiTasks;
using Kontur.Extern.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Client.Models.DraftsBuilders.DocumentFiles;
using Kontur.Extern.Client.Models.DraftsBuilders.Documents;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.ApiLevel.Clients.DraftsBuilders
{
    public class DraftsBuilderClient : IDraftsBuilderClient
    {
        private readonly IHttpRequestsFactory http;

        public DraftsBuilderClient(IHttpRequestsFactory http) => this.http = http;

        public Task<DraftsBuilder> CreateDraftsBuilderAsync(
            Guid accountId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<DraftsBuilder>($"/v1/{accountId}/drafts/builders", timeout);
        }

        public Task<DraftsBuilder> GetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null) => 
            http.GetAsync<DraftsBuilder>($"/v1/{accountId}/drafts/builders/{draftsBuilderId}", timeout);

        public Task DeleteDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null) => 
            http.DeleteAsync($"/v1/{accountId}/drafts/builders/{draftsBuilderId}", timeout);

        public Task<DraftsBuilderMeta> GetDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<DraftsBuilderMeta>($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta", timeout);
        }

        public Task<DraftsBuilderMeta> UpdateDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<DraftsBuilderMetaRequest, DraftsBuilderMeta>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta",
                meta,
                timeout
            );
        }

        public Task<DraftsBuilderBuildResult> BuildDraftsAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<DraftsBuilderBuildResult>($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/build", timeout);
        }

        public Task<ApiTaskResult<DraftsBuilderBuildResult>> StartBuildDraftsAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/build")
                .AppendToQuery("deferred", true)
                .Build();
            return http.PostAsync<ApiTaskResult<DraftsBuilderBuildResult>>(url, timeout);
        }

        public Task<ApiTaskResult<DraftsBuilderBuildResult>> GetBuildDraftsTaskAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<ApiTaskResult<DraftsBuilderBuildResult>>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/tasks/{taskId}",
                timeout
            );
        }

        public Task<DraftsBuilderDocument> CreateDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderDocumentMetaRequest meta,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<DraftsBuilderDocument>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents",
                timeout
            );
        }

        public Task<IReadOnlyCollection<DraftsBuilderDocument>> GetDocumentsAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<IReadOnlyCollection<DraftsBuilderDocument>>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents",
                timeout
            );
        }

        public Task<DraftsBuilderDocument> GetDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<DraftsBuilderDocument>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}",
                timeout
            );
        }

        public Task DeleteDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.DeleteAsync(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}",
                timeout
            );
        }

        public Task<DraftsBuilderDocumentMeta> GetDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<DraftsBuilderDocumentMeta>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta",
                timeout
            );
        }

        public Task<DraftsBuilderDocumentMeta> UpdateDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<DraftsBuilderMetaRequest, DraftsBuilderDocumentMeta>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta",
                meta,
                timeout
            );
        }

        public Task<DraftsBuilderDocumentFile> CreateFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderFileRequest fileRequest,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<DraftsBuilderFileRequest, DraftsBuilderDocumentFile>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files",
                fileRequest,
                timeout
            );
        }

        public Task<IReadOnlyCollection<DraftsBuilderDocumentFile>> GetFilesAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<IReadOnlyCollection<DraftsBuilderDocumentFile>>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files",
                timeout
            );
        }

        public Task<DraftsBuilderDocumentFile> GetFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<DraftsBuilderDocumentFile>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}",
                timeout
            );
        }

        public Task DeleteFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            return http.DeleteAsync(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}",
                timeout
            );
        }

        public Task<DraftsBuilderDocumentFile> UpdateFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderFileRequest fileRequest,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<DraftsBuilderFileRequest, DraftsBuilderDocumentFile>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}",
                fileRequest,
                timeout
            );
        }

        public Task<DraftsBuilderDocumentFileMeta> GetFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<DraftsBuilderDocumentFileMeta>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta",
                timeout
            );
        }

        public Task<DraftsBuilderDocumentFileMeta> UpdateFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderFileMetaRequest meta,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<DraftsBuilderFileMetaRequest, DraftsBuilderDocumentFileMeta>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta",
                meta,
                timeout
            );
        }

        public Task<byte[]> GetSignatureAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            return http.GetBytesAsync($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/signature", timeout);
        }
    }
}
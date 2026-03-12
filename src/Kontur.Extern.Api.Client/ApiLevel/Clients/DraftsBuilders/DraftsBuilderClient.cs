using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.Builders;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.Documents;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.DraftBuilders.Builders;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents;
using Kontur.Extern.Api.Client.Http;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.DraftsBuilders
{
    public class DraftsBuilderClient : IDraftsBuilderClient
    {
        private readonly IHttpRequestFactory http;

        public DraftsBuilderClient(IHttpRequestFactory http) => this.http = http;

        public Task<DraftsBuilder> CreateDraftsBuilderAsync(
            Guid accountId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<DraftsBuilderMetaRequest, DraftsBuilder>($"/v1/{accountId}/drafts/builders", meta, $"{nameof(DraftsBuilderClient)}.{nameof(CreateDraftsBuilderAsync)}", timeout);
        }

        public Task<DraftsBuilder> GetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null) =>
            http.GetAsync<DraftsBuilder>($"/v1/{accountId}/drafts/builders/{draftsBuilderId}", $"{nameof(DraftsBuilderClient)}.{nameof(CreateDraftsBuilderAsync)}_WithDraftsBuilderId", timeout);

        public Task<DraftsBuilder?> TryGetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null) =>
            http.TryGetAsync<DraftsBuilder>($"/v1/{accountId}/drafts/builders/{draftsBuilderId}", $"{nameof(DraftsBuilderClient)}.{nameof(TryGetDraftsBuilderAsync)}", timeout);

        public Task<bool> DeleteDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null) =>
            http.TryDeleteAsync($"/v1/{accountId}/drafts/builders/{draftsBuilderId}", $"{nameof(DraftsBuilderClient)}.{nameof(DeleteDraftsBuilderAsync)}", timeout);

        public Task<DraftsBuilderMeta> GetDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<DraftsBuilderMeta>($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta", $"{nameof(DraftsBuilderClient)}.{nameof(GetDraftsBuilderMetaAsync)}", timeout);
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
                $"{nameof(DraftsBuilderClient)}.{nameof(UpdateDraftsBuilderMetaAsync)}",
                timeout
            );
        }

        public Task<DraftsBuilderBuildResult> BuildDraftsAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<DraftsBuilderBuildResult>($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/build", $"{nameof(DraftsBuilderClient)}.{nameof(BuildDraftsAsync)}", timeout);
        }

        public Task<ApiTaskResult<DraftsBuilderBuildResult>> StartBuildDraftsAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/build")
                .AppendToQuery("deferred", true)
                .Build();
            return http.PostAsync<ApiTaskResult<DraftsBuilderBuildResult>>(url, $"{nameof(DraftsBuilderClient)}.{nameof(StartBuildDraftsAsync)}",timeout);
        }

        public Task<ApiTaskResult<DraftsBuilderBuildResult>> GetBuildDraftsTaskAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<ApiTaskResult<DraftsBuilderBuildResult>>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/tasks/{taskId}",
                $"{nameof(DraftsBuilderClient)}.{nameof(GetBuildDraftsTaskAsync)}",
                timeout
            );
        }

        public Task<ApiTaskResult<DraftsBuilderPrepareDocumentsResult>> GetPrepareDocumentsTaskAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<ApiTaskResult<DraftsBuilderPrepareDocumentsResult>>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/tasks/{taskId}",
                $"{nameof(DraftsBuilderClient)}.{nameof(GetPrepareDocumentsTaskAsync)}",
                timeout
            );
        }

        public Task<DraftsBuilderDocument> CreateDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderDocumentMetaRequest meta,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<DraftsBuilderDocumentMetaRequest, DraftsBuilderDocument>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents",
                meta,
                $"{nameof(DraftsBuilderClient)}.{nameof(CreateDocumentAsync)}",
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
                $"{nameof(DraftsBuilderClient)}.{nameof(GetDocumentsAsync)}",
                timeout
            );
        }

        public Task<ApiTaskResult<DraftsBuilderPrepareDocumentsResult>> StartPrepareDocumentsAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<ApiTaskResult<DraftsBuilderPrepareDocumentsResult>>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/prepare",
                $"{nameof(DraftsBuilderClient)}.{nameof(StartPrepareDocumentsAsync)}",
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
                $"{nameof(DraftsBuilderClient)}.{nameof(GetDocumentAsync)}",
                timeout
            );
        }

        public Task<DraftsBuilderDocument?> TryGetDocumentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId, TimeSpan? timeout = null)
        {
            return http.TryGetAsync<DraftsBuilderDocument>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}",
                $"{nameof(DraftsBuilderClient)}.{nameof(TryGetDocumentAsync)}",
                timeout
            );
        }

        public Task<bool> DeleteDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.TryDeleteAsync(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}",
                $"{nameof(DraftsBuilderClient)}.{nameof(DeleteDocumentAsync)}",
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
                $"{nameof(DraftsBuilderClient)}.{nameof(GetDocumentMetaAsync)}",

                timeout
            );
        }

        public Task<DraftsBuilderDocumentMeta?> TryGetDocumentMetaAsync(Guid accountId, Guid draftsBuilderId, Guid documentId, TimeSpan? timeout = null)
        {
            return http.TryGetAsync<DraftsBuilderDocumentMeta>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta",
                $"{nameof(DraftsBuilderClient)}.{nameof(TryGetDocumentMetaAsync)}",
                timeout
            );
        }

        public Task<DraftsBuilderDocumentMeta> UpdateDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderDocumentMetaRequest meta,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<DraftsBuilderDocumentMetaRequest, DraftsBuilderDocumentMeta>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta",
                meta,
                $"{nameof(DraftsBuilderClient)}.{nameof(UpdateDocumentMetaAsync)}",
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
                $"{nameof(DraftsBuilderClient)}.{nameof(CreateFileAsync)}",
                timeout
            );
        }

        public async Task<DraftsBuilderDocumentFile> GenerateFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            int? version,
            string contract,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/generate")
                .AppendToQuery("version", version)
                .Build();
            var response = await http.Post(url)
                .WithJson(contract)
                .CallingMethod($"{nameof(DraftsBuilderClient)}.{nameof(GenerateFileAsync)}")
                .SendAsync(timeout);
                
             return await response.GetMessageAsync<DraftsBuilderDocumentFile>().ConfigureAwait(false); 
        }

        public Task<IReadOnlyCollection<DraftsBuilderDocumentFile>> GetFilesAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<IReadOnlyCollection<DraftsBuilderDocumentFile>>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files",
                $"{nameof(DraftsBuilderClient)}.{nameof(GetFilesAsync)}",
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
                $"{nameof(DraftsBuilderClient)}.{nameof(GetFileAsync)}",
                timeout
            );
        }

        public Task<DraftsBuilderDocumentFile?> TryGetFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            return http.TryGetAsync<DraftsBuilderDocumentFile>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}",
                $"{nameof(DraftsBuilderClient)}.{nameof(TryGetFileAsync)}",
                timeout
            );
        }

        public Task<bool> DeleteFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            return http.TryDeleteAsync(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}",
                $"{nameof(DraftsBuilderClient)}.{nameof(DeleteFileAsync)}",
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
                $"{nameof(DraftsBuilderClient)}.{nameof(UpdateFileAsync)}",
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
                $"{nameof(DraftsBuilderClient)}.{nameof(GetFileMetaAsync)}",
                timeout
            );
        }

        public Task<DraftsBuilderDocumentFileMeta?> TryGetFileMetaAsync(Guid accountId, Guid draftsBuilderId, Guid documentId, Guid fileId, TimeSpan? timeout = null)
        {
            return http.TryGetAsync<DraftsBuilderDocumentFileMeta>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta",
                $"{nameof(DraftsBuilderClient)}.{nameof(TryGetFileMetaAsync)}",
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
                $"{nameof(DraftsBuilderClient)}.{nameof(UpdateFileMetaAsync)}",
                timeout
            );
        }

        public async Task<byte[]> GetSignatureAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            var base64String = await http.GetAsync<string>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/signature",
                $"{nameof(DraftsBuilderClient)}.{nameof(GetSignatureAsync)}",
                timeout
            );
            return Convert.FromBase64String(base64String);
        }

        public async Task<byte[]?> TryGetSignatureAsync(Guid accountId, Guid draftsBuilderId, Guid documentId, Guid fileId, TimeSpan? timeout = null)
        {
            var base64String = await http.TryGetAsync<string>(
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/signature", 
                $"{nameof(DraftsBuilderClient)}.{nameof(TryGetSignatureAsync)}",
                timeout
            );
            if (base64String is null)
                return null;
            return Convert.FromBase64String(base64String);
        }
    }
}
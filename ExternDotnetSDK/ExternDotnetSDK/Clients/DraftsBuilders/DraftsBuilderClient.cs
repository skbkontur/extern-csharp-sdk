using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Client.Models.DraftsBuilders.DocumentFiles;
using Kontur.Extern.Client.Models.DraftsBuilders.Documents;

namespace Kontur.Extern.Client.Clients.DraftsBuilders
{
    //todo Сделать нормальные тесты для методов.
    public class DraftsBuilderClient : IDraftsBuilderClient
    {
        private readonly InnerCommonClient client;

        public DraftsBuilderClient(ILogger logger, IRequestSender requestSender) =>
            client = new InnerCommonClient(logger, requestSender);

        public async Task<DraftsBuilder> CreateDraftsBuilderAsync(
            Guid accountId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilder>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/builders",
                contentDto: meta,
                timeout: timeout);

        public async Task DeleteDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync(
                HttpMethod.Delete,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}",
                timeout: timeout);

        public async Task<DraftsBuilder> GetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilder>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}",
                timeout: timeout);

        public async Task<DraftsBuilderMeta> GetDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderMeta>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta",
                timeout: timeout);

        public async Task<DraftsBuilderMeta> UpdateDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderMeta>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta",
                timeout: timeout);

        public async Task<DraftsBuilderBuildResult> BuildDraftsAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderBuildResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/build",
                new Dictionary<string, object> {["deferred"] = false},
                timeout: timeout);

        public async Task<ApiTaskResult<DraftsBuilderBuildResult>>
            BuildDeferredDraftsAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiTaskResult<DraftsBuilderBuildResult>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/build",
                new Dictionary<string, object> {["deferred"] = true},
                timeout: timeout);

        public async Task<ApiTaskResult<DraftsBuilderBuildResult>> GetBuildResultAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid apiTaskId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiTaskResult<DraftsBuilderBuildResult>>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/tasks/{apiTaskId}",
                timeout: timeout);

        public async Task<DraftsBuilderDocumentFile[]> GetDraftsBuilderDocumentFilesAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFile[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files",
                timeout: timeout);

        public async Task<DraftsBuilderDocumentFile> CreateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderDocumentFileContents contents,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFile>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files",
                contentDto: contents,
                timeout: timeout);

        public async Task DeleteDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync(
                HttpMethod.Delete,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}",
                timeout: timeout);

        public async Task<DraftsBuilderDocumentFile> GetDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFile>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}",
                timeout: timeout);

        public async Task<DraftsBuilderDocumentFile> UpdateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderDocumentFileContents contents,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFile>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}",
                contentDto: contents,
                timeout: timeout);

        public async Task<string> GetDraftsBuilderDocumentFileContentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/content",
                timeout: timeout);

        public async Task<string> GetDraftsBuilderDocumentFileSignatureAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/signature",
                timeout: timeout);

        public async Task<DraftsBuilderDocumentFileMeta> GetDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFileMeta>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta",
                timeout: timeout);

        public async Task<DraftsBuilderDocumentFileMeta> UpdateDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderDocumentFileMetaRequest meta,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFileMeta>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta",
                contentDto: meta,
                timeout: timeout);

        public async Task<DraftsBuilderDocument[]> GetDraftsBuilderDocumentsAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocument[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents",
                timeout: timeout);

        public async Task<DraftsBuilderDocument> CreateDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderDocumentMetaRequest meta,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents",
                contentDto: meta,
                timeout: timeout);

        public async Task DeleteDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync(
                HttpMethod.Delete,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}",
                timeout: timeout);

        public async Task<DraftsBuilderDocument> GetDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocument>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}",
                timeout: timeout);

        public async Task<DraftsBuilderDocumentMeta> GetDraftsBuilderDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocumentMeta>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta",
                timeout: timeout);

        public async Task<DraftsBuilderDocumentMeta> UpdateDraftsBuilderDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftsBuilderDocumentMeta>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta",
                contentDto: meta,
                timeout: timeout);
    }
}
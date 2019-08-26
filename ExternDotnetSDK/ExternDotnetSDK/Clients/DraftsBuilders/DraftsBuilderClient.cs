using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.Logging;
using ExternDotnetSDK.Clients.Common.RequestSenders;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.DraftsBuilders.Builders;
using ExternDotnetSDK.Models.DraftsBuilders.DocumentFiles;
using ExternDotnetSDK.Models.DraftsBuilders.Documents;

namespace ExternDotnetSDK.Clients.DraftsBuilders
{
    public class DraftsBuilderClient : IDraftsBuilderClient
    {
        private readonly InnerCommonClient client;

        public DraftsBuilderClient(ILogger logger, IRequestSender requestSender) =>
            client = new InnerCommonClient(logger, requestSender);

        public async Task<DraftsBuilder> CreateDraftsBuilderAsync(Guid accountId, DraftsBuilderMetaRequest meta) =>
            await client.SendRequestAsync<DraftsBuilder>(HttpMethod.Post, $"/v1/{accountId}/drafts/builders", contentDto: meta);

        public async Task DeleteDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId) =>
            await client.SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}/drafts/builders/{draftsBuilderId}");

        public async Task<DraftsBuilder> GetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId) =>
            await client.SendRequestAsync<DraftsBuilder>(HttpMethod.Get, $"/v1/{accountId}/drafts/builders/{draftsBuilderId}");

        public async Task<DraftsBuilderMeta> GetDraftsBuilderMetaAsync(Guid accountId, Guid draftsBuilderId) =>
            await client.SendRequestAsync<DraftsBuilderMeta>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta");

        public async Task<DraftsBuilderMeta> UpdateDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderMetaRequest meta) =>
            await client.SendRequestAsync<DraftsBuilderMeta>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta");

        public async Task<DraftsBuilderBuildResult> BuildDraftsAsync(Guid accountId, Guid draftsBuilderId) =>
            await client.SendRequestAsync<DraftsBuilderBuildResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/build",
                new Dictionary<string, object> {["deferred"] = false});

        public async Task<ApiTaskResult<DraftsBuilderBuildResult>>
            BuildDeferredDraftsAsync(Guid accountId, Guid draftsBuilderId) =>
            await client.SendRequestAsync<ApiTaskResult<DraftsBuilderBuildResult>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/build",
                new Dictionary<string, object> {["deferred"] = true});

        public async Task<ApiTaskResult<DraftsBuilderBuildResult>> GetBuildResultAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid apiTaskId) =>
            await client.SendRequestAsync<ApiTaskResult<DraftsBuilderBuildResult>>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/tasks/{apiTaskId}");

        public async Task<DraftsBuilderDocumentFile[]> GetDraftsBuilderDocumentFilesAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFile[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files");

        public async Task<DraftsBuilderDocumentFile> CreateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderDocumentFileContents contents) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFile>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files",
                contentDto: contents);

        public async Task DeleteDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await client.SendRequestAsync(
                HttpMethod.Delete,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}");

        public async Task<DraftsBuilderDocumentFile> GetDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFile>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}");

        public async Task<DraftsBuilderDocumentFile> UpdateDraftsBuilderDocumentFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderDocumentFileContents contents) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFile>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}",
                contentDto: contents);

        public async Task<string> GetDraftsBuilderDocumentFileContentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/content");

        public async Task<string> GetDraftsBuilderDocumentFileSignatureAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/signature");

        public async Task<DraftsBuilderDocumentFileMeta> GetDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFileMeta>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta");

        public async Task<DraftsBuilderDocumentFileMeta> UpdateDraftsBuilderDocumentFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderDocumentFileMetaRequest meta) =>
            await client.SendRequestAsync<DraftsBuilderDocumentFileMeta>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta",
                contentDto: meta);

        public async Task<DraftsBuilderDocument[]> GetDraftsBuilderDocumentsAsync(Guid accountId, Guid draftsBuilderId) =>
            await client.SendRequestAsync<DraftsBuilderDocument[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents");

        public async Task<DraftsBuilderDocument> CreateDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderDocumentMetaRequest meta) =>
            await client.SendRequestAsync<DraftsBuilderDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents",
                contentDto: meta);

        public async Task DeleteDraftsBuilderDocumentAsync(Guid accountId, Guid draftsBuilderId, Guid documentId) =>
            await client.SendRequestAsync(
                HttpMethod.Delete,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}");

        public async Task<DraftsBuilderDocument> GetDraftsBuilderDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId) =>
            await client.SendRequestAsync<DraftsBuilderDocument>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}");

        public async Task<DraftsBuilderDocumentMeta> GetDraftsBuilderDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId) =>
            await client.SendRequestAsync<DraftsBuilderDocumentMeta>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta");

        public async Task<DraftsBuilderDocumentMeta> UpdateDraftsBuilderDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderMetaRequest meta) =>
            await client.SendRequestAsync<DraftsBuilderDocumentMeta>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta",
                contentDto: meta);
    }
}
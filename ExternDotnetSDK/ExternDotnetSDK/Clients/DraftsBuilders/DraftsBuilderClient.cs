using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Client.Models.DraftsBuilders.DocumentFiles;
using Kontur.Extern.Client.Models.DraftsBuilders.Documents;

namespace Kontur.Extern.Client.Clients.DraftsBuilders
{
    public class DraftsBuilderClient : IDraftsBuilderClient
    {
        private readonly InnerCommonClient client;
        private readonly IRequestBodySerializer requestBodySerializer;

        public DraftsBuilderClient(ILogger logger, IRequestSender requestSender, IRequestBodySerializer requestBodySerializer)
        {
            this.requestBodySerializer = requestBodySerializer;
            client = new InnerCommonClient(logger, requestSender);
        }

        public Task<DraftsBuilder> CreateDraftsBuilderAsync(
            Guid accountId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/drafts/builders");
            return client.SendJsonRequestAsync<DraftsBuilder>(request, timeout);
        }

        public Task<DraftsBuilder> GetDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/builders/{draftsBuilderId}");
            return client.SendJsonRequestAsync<DraftsBuilder>(request, timeout);
        }

        public Task DeleteDraftsBuilderAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null)
        {
            var request = Request.Delete($"/v1/{accountId}/drafts/builders/{draftsBuilderId}");
            return client.SendJsonRequestAsync(request, timeout);
        }

        public Task<DraftsBuilderMeta> GetDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta");
            return client.SendJsonRequestAsync<DraftsBuilderMeta>(request, timeout);
        }

        public Task<DraftsBuilderMeta> UpdateDraftsBuilderMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null)
        {
            var request = Request.Put($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/meta");
            return client.SendJsonRequestAsync<DraftsBuilderMeta>(request, timeout);
        }

        public Task<DraftsBuilderBuildResult> BuildDraftsAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/build");
            return client.SendJsonRequestAsync<DraftsBuilderBuildResult>(request, timeout);
        }

        public Task<ApiTaskResult<DraftsBuilderBuildResult>> StartBuildDraftsAsync(Guid accountId, Guid draftsBuilderId, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/build")
                .AppendToQuery("deferred", true)
                .Build();
            var request = Request.Post(url);
            return client.SendJsonRequestAsync<ApiTaskResult<DraftsBuilderBuildResult>>(request, timeout);
        }

        public Task<ApiTaskResult<DraftsBuilderBuildResult>> GetBuildDraftsTaskAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/tasks/{taskId}");
            return client.SendJsonRequestAsync<ApiTaskResult<DraftsBuilderBuildResult>>(request, timeout);
        }

        public Task<DraftsBuilderDocument> CreateDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            DraftsBuilderDocumentMetaRequest meta,
            TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents");
            return client.SendJsonRequestAsync<DraftsBuilderDocument>(request, timeout);
        }

        public Task<IReadOnlyCollection<DraftsBuilderDocument>> GetDocumentsAsync(
            Guid accountId,
            Guid draftsBuilderId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents");
            return client.SendJsonRequestAsync<IReadOnlyCollection<DraftsBuilderDocument>>(request, timeout);
        }

        public Task<DraftsBuilderDocument> GetDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}");
            return client.SendJsonRequestAsync<DraftsBuilderDocument>(request, timeout);
        }

        public Task DeleteDocumentAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            var request = Request.Delete($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}");
            return client.SendJsonRequestAsync(request, timeout);
        }

        public Task<DraftsBuilderDocumentMeta> GetDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta");
            return client.SendJsonRequestAsync<DraftsBuilderDocumentMeta>(request, timeout);
        }

        public Task<DraftsBuilderDocumentMeta> UpdateDocumentMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderMetaRequest meta,
            TimeSpan? timeout = null)
        {
            var request = Request.Put($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/meta");
            return client.SendJsonRequestAsync<DraftsBuilderDocumentMeta>(request, timeout);
        }

        public Task<DraftsBuilderDocumentFile> CreateFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            DraftsBuilderFileRequest fileRequest,
            TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files")
                .WithContent(requestBodySerializer.SerializeToJson(fileRequest));
            return client.SendJsonRequestAsync<DraftsBuilderDocumentFile>(request, timeout);
        }

        public Task<IReadOnlyCollection<DraftsBuilderDocumentFile>> GetFilesAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files");
            return client.SendJsonRequestAsync<IReadOnlyCollection<DraftsBuilderDocumentFile>>(request, timeout);
        }

        public Task<DraftsBuilderDocumentFile> GetFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}");
            return client.SendJsonRequestAsync<DraftsBuilderDocumentFile>(request, timeout);
        }

        public Task DeleteFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            var request = Request.Delete($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}");
            return client.SendJsonRequestAsync(request, timeout);
        }

        public Task<DraftsBuilderDocumentFile> UpdateFileAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderFileRequest fileRequest,
            TimeSpan? timeout = null)
        {
            var request = Request.Put($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}")
                .WithContent(requestBodySerializer.SerializeToJson(fileRequest));
            return client.SendJsonRequestAsync<DraftsBuilderDocumentFile>(request, timeout);
        }

        public Task<DraftsBuilderDocumentFileMeta> GetFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta");
            return client.SendJsonRequestAsync<DraftsBuilderDocumentFileMeta>(request, timeout);
        }

        public Task<DraftsBuilderDocumentFileMeta> UpdateFileMetaAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            DraftsBuilderFileMetaRequest meta,
            TimeSpan? timeout = null)
        {
            var request = Request.Put($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/meta")
                .WithContent(requestBodySerializer.SerializeToJson(meta));
            return client.SendJsonRequestAsync<DraftsBuilderDocumentFileMeta>(request, timeout);
        }

        public Task<byte[]> GetSignatureAsync(
            Guid accountId,
            Guid draftsBuilderId,
            Guid documentId,
            Guid fileId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/builders/{draftsBuilderId}/documents/{documentId}/files/{fileId}/signature");
            return client.SendJsonRequestAsync<byte[]>(request, timeout);
        }
    }
}
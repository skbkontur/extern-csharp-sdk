using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Drafts;
using Kontur.Extern.Client.Models.Drafts.Check;
using Kontur.Extern.Client.Models.Drafts.Meta;
using Kontur.Extern.Client.Models.Drafts.Prepare;
using Kontur.Extern.Client.Models.Drafts.Requests;

namespace Kontur.Extern.Client.Clients.Drafts
{
    public class DraftsClient : IDraftsClient
    {
        private readonly InnerCommonClient client;
        private readonly IRequestBodySerializer requestBodySerializer;

        public DraftsClient(ILogger logger, _IRequestSender requestSender, IRequestBodySerializer requestBodySerializer)
        {
            this.requestBodySerializer = requestBodySerializer;
            client = new InnerCommonClient(logger, requestSender);
        }

        public Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest meta, TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/drafts")
                .WithContent(requestBodySerializer.SerializeToJson(meta));
            return client.SendJsonRequestAsync<Draft>(request, timeout);
        }

        public Task<Draft> GetDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/{draftId}");
            return client.SendJsonRequestAsync<Draft>(request, timeout);
        }

        public Task DeleteDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null)
        {
            var request = Request.Delete($"/v1/{accountId}/drafts/{draftId}");
            return client.SendJsonRequestAsync(request, timeout);
        }

        public Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/{draftId}/meta");
            return client.SendJsonRequestAsync<DraftMeta>(request, timeout);
        }

        public Task<DraftMeta> UpdateDraftMetaAsync(
            Guid accountId,
            Guid draftId,
            DraftMetaRequest meta,
            TimeSpan? timeout = null)
        {
            var request = Request.Put($"/v1/{accountId}/drafts/{draftId}/meta")
                .WithContent(requestBodySerializer.SerializeToJson(meta));
            return client.SendJsonRequestAsync<DraftMeta>(request, timeout);
        }

        public Task<DraftDocument> CreateDocumentAsync(
            Guid accountId,
            Guid draftId,
            DocumentRequest documentRequest,
            TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/drafts/{draftId}/documents")
                .WithContent(requestBodySerializer.SerializeToJson(documentRequest));
            return client.SendJsonRequestAsync<DraftDocument>(request, timeout);
        }

        public Task<DraftDocument> GetDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}");
            return client.SendJsonRequestAsync<DraftDocument>(request, timeout);
        }

        public Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null)
        {
            var request = Request.Delete($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}");
            return client.SendJsonRequestAsync(request, timeout);
        }

        public Task<DraftDocument> UpdateDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentRequest documentRequest,
            TimeSpan? timeout = null)
        {
            var request = Request.Put($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}")
                .WithContent(requestBodySerializer.SerializeToJson(documentRequest));
            return client.SendJsonRequestAsync<DraftDocument>(request, timeout);
        }

        public Task<byte[]> PrintDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print");
            return client.SendJsonRequestAsync<byte[]>(request, timeout);
        }

        public Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print")
                .AppendToQuery("deferred", true)
                .Build();
            var request = Request.Get(url);
            return client.SendJsonRequestAsync<ApiTaskResult<PrintDocumentResult>>(request, timeout);
        }

        public Task<Signature> CreateSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            SignatureRequest signatureRequest = null,
            TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures")
                .WithContent(requestBodySerializer.SerializeToJson(signatureRequest));
            return client.SendJsonRequestAsync<Signature>(request, timeout);
        }

        public Task<Signature> GetSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}");
            return client.SendJsonRequestAsync<Signature>(request, timeout);
        }

        public Task DeleteSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            var request = Request.Delete($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}");
            return client.SendJsonRequestAsync(request, timeout);
        }

        public Task<Signature> UpdateSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest signatureRequest,
            TimeSpan? timeout = null)
        {
            var request = Request.Put($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}")
                .WithContent(requestBodySerializer.SerializeToJson(signatureRequest));
            return client.SendJsonRequestAsync<Signature>(request, timeout);
        }

        public Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}/content");
            return client.SendJsonRequestAsync<byte[]>(request, timeout);
        }

        public Task<CheckResult> CheckDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/drafts/{draftId}/check");
            return client.SendJsonRequestAsync<CheckResult>(request, timeout);
        }

        public Task<ApiTaskResult<CheckResult>> StartCheckDraftAsync(
            Guid accountId,
            Guid draftId,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/check")
                .AppendToQuery("deferred", true)
                .Build();
            var request = Request.Post(url);
            return client.SendJsonRequestAsync<ApiTaskResult<CheckResult>>(request, timeout);
        }

        public Task<PrepareResult> PrepareDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/drafts/{draftId}/prepare");
            return client.SendJsonRequestAsync<PrepareResult>(request, timeout);
        }

        public Task<ApiTaskResult<PrepareResult>> StartPrepareDraftAsync(
            Guid accountId,
            Guid draftId,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/prepare")
                .AppendToQuery("deferred", true)
                .Build();
            var request = Request.Post(url);
            return client.SendJsonRequestAsync<ApiTaskResult<PrepareResult>>(request, timeout);
        }

        public Task<Docflow> SendDraftAsync(Guid accountId, Guid draftId, bool? force = null, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/send")
                .AppendToQuery("force", force)
                .Build();
            var request = Request.Post(url);
            return client.SendJsonRequestAsync<Docflow>(request, timeout);
        }

        public Task<ApiTaskResult<Docflow>> StartSendDraftAsync(
            Guid accountId,
            Guid draftId,
            bool? force = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/send")
                .AppendToQuery("deferred", true)
                .AppendToQuery("force", force)
                .Build();
            var request = Request.Post(url);
            return client.SendJsonRequestAsync<ApiTaskResult<Docflow>>(request, timeout);
        }

        public Task BuildDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            FormatType type,
            int? version,
            string contract,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/build")
                .AppendToQuery("type", type)
                .AppendToQuery("version", version)
                .Build();
            var request = Request.Post(url)
                .WithContent(contract);
            return client.SendJsonRequestAsync(request, timeout);
        }

        public Task<DraftDocument> BuildDocumentAsync(
            Guid accountId,
            Guid draftId,
            FormatType type,
            int? version,
            string contract,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/build-document")
                .AppendToQuery("type", type)
                .AppendToQuery("version", version)
                .Build();
            var request = Request.Post(url)
                .WithContent(contract);
            return client.SendJsonRequestAsync<DraftDocument>(request, timeout);
        }

        public Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            int? skip = null,
            int? take = null,
            bool? includeReleased = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/tasks")
                .AppendToQuery("skip", skip)
                .AppendToQuery("take", take)
                .AppendToQuery("includeReleased", includeReleased)
                .Build();
            var request = Request.Get(url);
            return client.SendJsonRequestAsync<ApiTaskPage>(request, timeout);
        }

        public Task<SignInitResult> DssSignAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/drafts/{draftId}/cloud-sign");
            return client.SendJsonRequestAsync<SignInitResult>(request, timeout);
        }

        public Task<ApiTaskResult<CryptOperationStatusResult>> GetDssSignTask(
            Guid accountId,
            Guid draftId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/drafts/{draftId}/tasks/{taskId}");
            return client.SendJsonRequestAsync<ApiTaskResult<CryptOperationStatusResult>>(request, timeout);
        }
    }
}
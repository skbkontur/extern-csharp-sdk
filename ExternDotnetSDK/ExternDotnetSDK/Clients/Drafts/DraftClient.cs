using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KeApiOpenSdk.Clients.Common;
using KeApiOpenSdk.Clients.Common.Logging;
using KeApiOpenSdk.Clients.Common.RequestSenders;
using KeApiOpenSdk.Models.Api;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.Docflows;
using KeApiOpenSdk.Models.Drafts;
using KeApiOpenSdk.Models.Drafts.Check;
using KeApiOpenSdk.Models.Drafts.Meta;
using KeApiOpenSdk.Models.Drafts.Prepare;
using KeApiOpenSdk.Models.Drafts.Requests;

namespace KeApiOpenSdk.Clients.Drafts
{
    //todo Сделать нормальные тесты для методов.
    public class DraftClient : IDraftClient
    {
        private readonly InnerCommonClient client;

        public DraftClient(ILogger logger, IRequestSender requestSender) => client = new InnerCommonClient(logger, requestSender);

        public async Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Draft>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts",
                contentDto: draftRequest,
                timeout: timeout);

        public async Task DeleteDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}/drafts/{draftId}", timeout: timeout);

        public async Task<Draft> GetDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Draft>(HttpMethod.Get, $"/v1/{accountId}/drafts/{draftId}", timeout: timeout);

        public async Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftMeta>(HttpMethod.Get, $"/v1/{accountId}/drafts/{draftId}/meta", timeout: timeout);

        public async Task<DraftMeta> UpdateDraftMetaAsync(
            Guid accountId,
            Guid draftId,
            DraftMetaRequest newMeta,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftMeta>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/meta",
                contentDto: newMeta,
                timeout: timeout);

        public async Task<DraftDocument> AddDocumentAsync(
            Guid accountId,
            Guid draftId,
            DocumentContents content,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/documents",
                contentDto: content,
                timeout: timeout);

        public async Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync(
                HttpMethod.Delete,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}",
                timeout: timeout);

        public async Task<DraftDocument> GetDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftDocument>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}",
                timeout: timeout);

        public async Task<DraftDocument> UpdateDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentContents content,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}",
                contentDto: content,
                timeout: timeout);

        public async Task<string> GetDocumentPrintAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print",
                timeout: timeout);

        public async Task<string> GetDocumentDecryptedContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/decrypted-content",
                timeout: timeout);

        public async Task UpdateDocumentDecryptedContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            byte[] content,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/decrypted-content",
                contentDto: Convert.ToBase64String(content),
                timeout: timeout);

        public async Task<string> GetDocumentSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signature",
                timeout: timeout);

        public async Task UpdateDocumentSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            byte[] content,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signature",
                contentDto: Convert.ToBase64String(content),
                timeout: timeout);

        public async Task<Signature> AddDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            SignatureRequest request = null,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Signature>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures",
                contentDto: request,
                timeout: timeout);

        public async Task DeleteDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync(
                HttpMethod.Delete,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}",
                timeout: timeout);

        public async Task<Signature> GetDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Signature>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}",
                timeout: timeout);

        public async Task<Signature> UpdateDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest request,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Signature>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}",
                contentDto: request,
                timeout: timeout);

        public async Task<string> GetDocumentSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}/content",
                timeout: timeout);

        public async Task<CheckResult> CheckDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<CheckResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/check",
                new Dictionary<string, object> {["deferred"] = false},
                timeout: timeout);

        public async Task<ApiTaskResult<CheckResult>> StartCheckDraftAsync(
            Guid accountId,
            Guid draftId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiTaskResult<CheckResult>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/check",
                new Dictionary<string, object> {["deferred"] = true},
                timeout: timeout);

        public async Task<PrepareResult> PrepareDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<PrepareResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/prepare",
                new Dictionary<string, object> {["deferred"] = false},
                timeout: timeout);

        public async Task<ApiTaskResult<PrepareResult>> StartPrepareDraftAsync(
            Guid accountId,
            Guid draftId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiTaskResult<PrepareResult>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/prepare",
                new Dictionary<string, object> {["deferred"] = true},
                timeout: timeout);

        public async Task<Docflow> SendDraftAsync(Guid accountId, Guid draftId, bool force = false, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Docflow>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/send",
                new Dictionary<string, object>
                {
                    ["deferred"] = false,
                    [nameof(force)] = force
                },
                timeout: timeout);

        public async Task<ApiTaskResult<Docflow>> StartSendDraftAsync(
            Guid accountId,
            Guid draftId,
            bool force = false,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiTaskResult<Docflow>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/send",
                new Dictionary<string, object>
                {
                    ["deferred"] = true,
                    [nameof(force)] = force
                },
                timeout: timeout);

        public async Task<string> GetDocumentEncryptedContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                "/v1/{accountId}/drafts/{draftId}/documents/{documentId}/encrypted-content",
                timeout: timeout);

        public async Task BuildDocumentContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            FormatType type,
            string content,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/build",
                new Dictionary<string, object>
                {
                    ["type"] = type,
                    ["version"] = 1
                },
                content,
                timeout);

        public async Task<DraftDocument> CreateDocumentWithContentFromFormatAsync(
            Guid accountId,
            Guid draftId,
            FormatType type,
            int version,
            string content,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DraftDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/build-document",
                new Dictionary<string, object>
                {
                    ["type"] = type,
                    ["version"] = version
                },
                content,
                timeout);

        public async Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            long skip = 0,
            int take = int.MaxValue,
            bool includeReleased = true,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiTaskPage>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/tasks",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take,
                    ["includeReleased"] = includeReleased
                },
                timeout: timeout);

        public async Task<ApiTaskResult<CryptOperationStatusResult>> GetDraftTask(
            Guid accountId,
            Guid draftId,
            Guid apiTaskId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiTaskResult<CryptOperationStatusResult>>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/tasks/{apiTaskId}",
                timeout: timeout);

        public async Task<SignInitResult> CloudSignDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<SignInitResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/cloud-sign",
                timeout: timeout);

        public async Task<SignResult> CloudSignConfirmDraftAsync(
            Guid accountId,
            Guid draftId,
            Guid requestId,
            string code,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<SignResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/cloud-sign-confirm",
                new Dictionary<string, object>
                {
                    ["requestId"] = requestId,
                    ["code"] = code
                },
                timeout: timeout);
    }
}
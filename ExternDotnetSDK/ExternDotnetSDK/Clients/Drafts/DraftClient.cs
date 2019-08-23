using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces;
using ExternDotnetSDK.Clients.Common.ImplementableInterfaces.Logging;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Drafts;
using ExternDotnetSDK.Models.Drafts.Check;
using ExternDotnetSDK.Models.Drafts.Meta;
using ExternDotnetSDK.Models.Drafts.Prepare;
using ExternDotnetSDK.Models.Drafts.Requests;

namespace ExternDotnetSDK.Clients.Drafts
{
    public class DraftClient : IDraftClient
    {
        private readonly InnerCommonClient client;

        public DraftClient(ILogger logger, IRequestSender sender, IRequestFactory requestFactory) =>
            client = new InnerCommonClient(logger, sender, requestFactory);

        public async Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest) =>
            await client.SendRequestAsync<Draft>(HttpMethod.Post, $"/v1/{accountId}/drafts", contentDto: draftRequest);

        public async Task DeleteDraftAsync(Guid accountId, Guid draftId) =>
            await client.SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}/drafts/{draftId}");

        public async Task<Draft> GetDraftAsync(Guid accountId, Guid draftId) =>
            await client.SendRequestAsync<Draft>(HttpMethod.Get, $"/v1/{accountId}/drafts/{draftId}");

        public async Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId) =>
            await client.SendRequestAsync<DraftMeta>(HttpMethod.Get, $"/v1/{accountId}/drafts/{draftId}/meta");

        public async Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, DraftMetaRequest newMeta) =>
            await client.SendRequestAsync<DraftMeta>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/meta",
                contentDto: newMeta);

        public async Task<DraftDocument> AddDocumentAsync(Guid accountId, Guid draftId, DocumentContents content) =>
            await client.SendRequestAsync<DraftDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/documents",
                contentDto: content);

        public async Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await client.SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}");

        public async Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await client.SendRequestAsync<DraftDocument>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}");

        public async Task<DraftDocument> UpdateDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentContents content) =>
            await client.SendRequestAsync<DraftDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}",
                contentDto: content);

        public async Task<string> GetDocumentPrintAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print");

        public async Task<string> GetDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/decrypted-content");

        public async Task UpdateDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content) =>
            await client.SendRequestAsync(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/decrypted-content",
                contentDto: Convert.ToBase64String(content));

        public async Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signature");

        public async Task UpdateDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content) =>
            await client.SendRequestAsync(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signature",
                contentDto: Convert.ToBase64String(content));

        public async Task<Signature> AddDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            SignatureRequest request = null) =>
            await client.SendRequestAsync<Signature>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures",
                contentDto: request);

        public async Task DeleteDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId) =>
            await client.SendRequestAsync(
                HttpMethod.Delete,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}");

        public async Task<Signature> GetDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId) =>
            await client.SendRequestAsync<Signature>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}");

        public async Task<Signature> UpdateDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest request) =>
            await client.SendRequestAsync<Signature>(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}",
                contentDto: request);

        public async Task<string> GetDocumentSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}/content");

        public async Task<CheckResult> CheckDraftAsync(Guid accountId, Guid draftId) =>
            await client.SendRequestAsync<CheckResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/check",
                new Dictionary<string, object> {["deferred"] = false});

        public async Task<ApiTaskResult<CheckResult>> StartCheckDraftAsync(Guid accountId, Guid draftId) =>
            await client.SendRequestAsync<ApiTaskResult<CheckResult>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/check",
                new Dictionary<string, object> {["deferred"] = true});

        public async Task<PrepareResult> PrepareDraftAsync(Guid accountId, Guid draftId) =>
            await client.SendRequestAsync<PrepareResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/prepare",
                new Dictionary<string, object> {["deferred"] = false});

        public async Task<ApiTaskResult<PrepareResult>> StartPrepareDraftAsync(Guid accountId, Guid draftId) =>
            await client.SendRequestAsync<ApiTaskResult<PrepareResult>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/prepare",
                new Dictionary<string, object> {["deferred"] = true});

        public async Task<Docflow> SendDraftAsync(Guid accountId, Guid draftId, bool force = false) =>
            await client.SendRequestAsync<Docflow>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/send",
                new Dictionary<string, object>
                {
                    ["deferred"] = false,
                    [nameof(force)] = force
                });

        public async Task<ApiTaskResult<Docflow>> StartSendDraftAsync(Guid accountId, Guid draftId, bool force = false) =>
            await client.SendRequestAsync<ApiTaskResult<Docflow>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/send",
                new Dictionary<string, object>
                {
                    ["deferred"] = true,
                    [nameof(force)] = force
                });

        public async Task<string> GetDocumentEncryptedContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Get,
                "/v1/{accountId}/drafts/{draftId}/documents/{documentId}/encrypted-content");

        public async Task BuildDocumentContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            FormatType type,
            string content) =>
            await client.SendRequestAsync(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/build",
                new Dictionary<string, object>
                {
                    ["type"] = type,
                    ["version"] = 1
                },
                content);

        public async Task<DraftDocument> CreateDocumentWithContentFromFormatAsync(
            Guid accountId,
            Guid draftId,
            FormatType type,
            int version,
            string content) =>
            await client.SendRequestAsync<DraftDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/build-document",
                new Dictionary<string, object>
                {
                    ["type"] = type,
                    ["version"] = version
                },
                content);

        public async Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            long skip = 0,
            int take = int.MaxValue,
            bool includeReleased = true) =>
            await client.SendRequestAsync<ApiTaskPage>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/tasks",
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take,
                    ["includeReleased"] = includeReleased
                });

        public async Task<ApiTaskResult<CryptOperationStatusResult>> GetDraftTask(Guid accountId, Guid draftId, Guid apiTaskId) =>
            await client.SendRequestAsync<ApiTaskResult<CryptOperationStatusResult>>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/tasks/{apiTaskId}");

        public async Task<SignInitResult> CloudSignDraftAsync(Guid accountId, Guid draftId) =>
            await client.SendRequestAsync<SignInitResult>(HttpMethod.Post, $"/v1/{accountId}/drafts/{draftId}/cloud-sign");

        public async Task<SignResult> CloudSignConfirmDraftAsync(Guid accountId, Guid draftId, Guid requestId, string code) =>
            await client.SendRequestAsync<SignResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/cloud-sign-confirm",
                new Dictionary<string, object>
                {
                    ["requestId"] = requestId,
                    ["code"] = code
                });
    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
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
    public class DraftClient : InnerCommonClient, IDraftClient
    {
        public DraftClient(ILogError logError, HttpClient client)
            : base(logError, client)
        {
        }

        public async Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest) =>
            await SendRequestAsync<Draft>(HttpMethod.Post, $"/v1/{accountId}/drafts", draftRequest);

        public async Task DeleteDraftAsync(Guid accountId, Guid draftId) =>
            await SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}/drafts/{draftId}");

        public async Task<Draft> GetDraftAsync(Guid accountId, Guid draftId) =>
            await SendRequestAsync<Draft>(HttpMethod.Get, $"/v1/{accountId}/drafts/{draftId}");

        public async Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId) =>
            await SendRequestAsync<DraftMeta>(HttpMethod.Get, $"/v1/{accountId}/drafts/{draftId}/meta");

        public async Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, DraftMetaRequest newMeta) =>
            await SendRequestAsync<DraftMeta>(HttpMethod.Put, $"/v1/{accountId}/drafts/{draftId}/meta", newMeta);

        public async Task<DraftDocument> AddDocumentAsync(Guid accountId, Guid draftId, DocumentContents content) =>
            await SendRequestAsync<DraftDocument>(HttpMethod.Post, $"/v1/{accountId}/drafts/{draftId}/documents", content);

        public async Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await SendRequestAsync(HttpMethod.Delete, $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}");

        public async Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await SendRequestAsync<DraftDocument>(HttpMethod.Get, $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}");

        public async Task<DraftDocument> UpdateDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentContents content) => await SendRequestAsync<DraftDocument>(
            HttpMethod.Put,
            $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}",
            content);

        public async Task<string> GetDocumentPrintAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await SendRequestAsync<string>(HttpMethod.Get, $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print");

        public async Task<string> GetDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await SendRequestAsync<string>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/decrypted-content");

        public async Task UpdateDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content) =>
            await SendRequestAsync(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/decrypted-content",
                contentDto: Convert.ToBase64String(content));

        public async Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await SendRequestAsync<string>(HttpMethod.Get, $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signature");

        public async Task UpdateDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content) =>
            await SendRequestAsync(
                HttpMethod.Put,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signature",
                contentDto: Convert.ToBase64String(content));

        public async Task<Signature> AddDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            SignatureRequest request = null) => await SendRequestAsync<Signature>(
            HttpMethod.Post,
            $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures",
            request);

        public async Task DeleteDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId) =>
            await SendRequestAsync(
                HttpMethod.Delete,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}");

        public async Task<Signature> GetDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId) =>
            await SendRequestAsync<Signature>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}");

        public async Task<Signature> UpdateDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest request) => await SendRequestAsync<Signature>(
            HttpMethod.Put,
            $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}",
            request);

        public async Task<string> GetDocumentSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId) => await SendRequestAsync<string>(
            HttpMethod.Get,
            $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}/content");

        public async Task<CheckResult> CheckDraftAsync(Guid accountId, Guid draftId) =>
            await SendRequestAsync<CheckResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/check",
                uriQueryParams: "?deferred=false");

        public async Task<ApiTaskResult<CheckResult>> StartCheckDraftAsync(Guid accountId, Guid draftId) =>
            await SendRequestAsync<ApiTaskResult<CheckResult>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/check",
                uriQueryParams: "?deferred=true");

        public async Task<PrepareResult> PrepareDraftAsync(Guid accountId, Guid draftId) =>
            await SendRequestAsync<PrepareResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/prepare",
                uriQueryParams: "?deferred=false");

        public async Task<ApiTaskResult<PrepareResult>> StartPrepareDraftAsync(Guid accountId, Guid draftId) =>
            await SendRequestAsync<ApiTaskResult<PrepareResult>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/prepare",
                uriQueryParams: "?deferred=true");

        public async Task<Docflow> SendDraftAsync(Guid accountId, Guid draftId, bool force = false) =>
            await SendRequestAsync<Docflow>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/send",
                uriQueryParams: StringifyUriQueryParams(
                    new Dictionary<string, object>
                    {
                        ["deferred"] = false,
                        [nameof(force)] = force
                    }));

        public async Task<ApiTaskResult<Docflow>> StartSendDraftAsync(Guid accountId, Guid draftId, bool force = false)=>
            await SendRequestAsync<ApiTaskResult<Docflow>>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/send",
                uriQueryParams: StringifyUriQueryParams(
                    new Dictionary<string, object>
                    {
                        ["deferred"] = true,
                        [nameof(force)] = force
                    }));

        public async Task<string> GetDocumentEncryptedContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await SendRequestAsync<string>(
                HttpMethod.Get,
                "/v1/{accountId}/drafts/{draftId}/documents/{documentId}/encrypted-content");

        public async Task BuildDocumentContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            FormatType type,
            string content) => await SendRequestAsync(
            HttpMethod.Post,
            $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/build",
            content,
            StringifyUriQueryParams(
                new Dictionary<string, object>
                {
                    ["type"] = type,
                    ["version"] = 1
                }));

        public async Task<DraftDocument> CreateDocumentWithContentFromFormatAsync(
            Guid accountId,
            Guid draftId,
            FormatType type,
            int version,
            string content) => await SendRequestAsync<DraftDocument>(
            HttpMethod.Post,
            $"/v1/{accountId}/drafts/{draftId}/build-document",
            content,
            StringifyUriQueryParams(
                new Dictionary<string, object>
                {
                    ["type"] = type,
                    ["version"] = version,
                }));

        public async Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            long skip = 0,
            int take = int.MaxValue,
            bool includeReleased = true) => await SendRequestAsync<ApiTaskPage>(
            HttpMethod.Get,
            $"/v1/{accountId}/drafts/{draftId}/tasks",
            uriQueryParams: StringifyUriQueryParams(
                new Dictionary<string, object>
                {
                    ["skip"] = skip,
                    ["take"] = take,
                    ["includeReleased"] = includeReleased
                }));

        public async Task<ApiTaskResult<CryptOperationStatusResult>> GetDraftTask(Guid accountId, Guid draftId, Guid apiTaskId) =>
            await SendRequestAsync<ApiTaskResult<CryptOperationStatusResult>>(
                HttpMethod.Get,
                $"/v1/{accountId}/drafts/{draftId}/tasks/{apiTaskId}");

        public async Task<SignInitResult> CloudSignDraftAsync(Guid accountId, Guid draftId) =>
            await SendRequestAsync<SignInitResult>(HttpMethod.Post, $"/v1/{accountId}/drafts/{draftId}/cloud-sign");

        public async Task<SignResult> CloudSignConfirmDraftAsync(Guid accountId, Guid draftId, Guid requestId, string code) =>
            await SendRequestAsync<SignResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/drafts/{draftId}/cloud-sign-confirm",
                uriQueryParams: StringifyUriQueryParams(
                    new Dictionary<string, object>
                    {
                        ["requestId"] = requestId,
                        ["code"] = code
                    }));
    }
}
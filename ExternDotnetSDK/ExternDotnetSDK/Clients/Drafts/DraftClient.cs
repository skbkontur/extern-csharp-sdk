using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts;
using ExternDotnetSDK.Models.Drafts.Meta;
using ExternDotnetSDK.Models.Drafts.Requests;
using Refit;

namespace ExternDotnetSDK.Clients.Drafts
{
    public class DraftClient : InnerCommonClient, IDraftClient
    {
        public DraftClient(ILogError logError, HttpClient client)
            : base(logError) =>
            ClientRefit = RestService.For<IDraftClientRefit>(client);

        public IDraftClientRefit ClientRefit { get; }

        public async Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest) =>
            await TryExecuteTask(ClientRefit.CreateDraftAsync(accountId, draftRequest));

        public async Task DeleteDraftAsync(Guid accountId, Guid draftId) =>
            await TryExecuteTask(ClientRefit.DeleteDraftAsync(accountId, draftId));

        public async Task<Draft> GetDraftAsync(Guid accountId, Guid draftId) =>
            await TryExecuteTask(ClientRefit.GetDraftAsync(accountId, draftId));

        public async Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId) =>
            await TryExecuteTask(ClientRefit.GetDraftMetaAsync(accountId, draftId));

        public async Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, DraftMetaRequest newMeta) =>
            await TryExecuteTask(ClientRefit.UpdateDraftMetaAsync(accountId, draftId, newMeta));

        public async Task<DraftDocument> AddDocumentAsync(Guid accountId, Guid draftId, [Body] DocumentContents content) =>
            await TryExecuteTask(ClientRefit.AddDocumentAsync(accountId, draftId, content));

        public async Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.DeleteDocumentAsync(accountId, draftId, documentId));

        public async Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.GetDocumentAsync(accountId, draftId, documentId));

        public async Task<DraftDocument> UpdateDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentContents content) =>
            await TryExecuteTask(ClientRefit.UpdateDocumentAsync(accountId, draftId, documentId, content));

        public async Task<string> GetDocumentPrintAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.GetDocumentPrintAsync(accountId, draftId, documentId));

        public async Task<string> GetDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.GetDocumentDecryptedContentAsync(accountId, draftId, documentId));

        public async Task UpdateDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content) =>
            await TryExecuteTask(
                ClientRefit.UpdateDocumentDecryptedContentAsync(
                    accountId,
                    draftId,
                    documentId,
                    Convert.ToBase64String(content)));

        public async Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.GetDocumentSignatureContentAsync(accountId, draftId, documentId));

        public async Task UpdateDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content) =>
            await TryExecuteTask(
                ClientRefit.UpdateDocumentSignatureContentAsync(
                    accountId,
                    draftId,
                    documentId,
                    Convert.ToBase64String(content)));

        public async Task<Signature> AddDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            SignatureRequest request = null) =>
            await TryExecuteTask(ClientRefit.AddDocumentSignatureAsync(accountId, draftId, documentId, request));

        public async Task DeleteDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId) =>
            await TryExecuteTask(ClientRefit.DeleteDocumentSignatureAsync(accountId, draftId, documentId, signatureId));

        public async Task<Signature> GetDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId) =>
            await TryExecuteTask(ClientRefit.GetDocumentSignatureAsync(accountId, draftId, documentId, signatureId));

        public async Task<Signature> UpdateDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest request) =>
            await TryExecuteTask(ClientRefit.UpdateDocumentSignatureAsync(accountId, draftId, documentId, signatureId, request));

        public async Task<string> GetDocumentSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId) => await TryExecuteTask(
            ClientRefit.GetDocumentSignatureContentAsync(accountId, draftId, documentId, signatureId));

        public async Task<string> CheckDraftAsync(Guid accountId, Guid draftId, bool deferred = false) =>
            await TryExecuteTask(ClientRefit.CheckDraftAsync(accountId, draftId, deferred));

        public async Task<string> PrepareDraftAsync(Guid accountId, Guid draftId, bool deferred = false) =>
            await TryExecuteTask(ClientRefit.PrepareDraftAsync(accountId, draftId, deferred));

        public async Task<string> SendDraftAsync(Guid accountId, Guid draftId, bool deferred = false, bool force = false) =>
            await TryExecuteTask(ClientRefit.SendDraftAsync(accountId, draftId, deferred, force));

        public async Task<string> GetDocumentEncryptedContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.GetDocumentEncryptedContentAsync(accountId, draftId, documentId));

        public async Task BuildDocumentContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            FormatType type,
            string content) => await TryExecuteTask(
            ClientRefit.BuildDocumentContentAsync(
                accountId,
                draftId,
                documentId,
                type.ToString(),
                1,
                content));

        public async Task<DraftDocument> CreateDocumentWithContentFromFormatAsync(
            Guid accountId,
            Guid draftId,
            FormatType type,
            string content) => await TryExecuteTask(
            ClientRefit.CreateDocumentWithContentFromFormatAsync(
                accountId,
                draftId,
                type.ToString(),
                1,
                content));

        public async Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            long skip = 0,
            int take = int.MaxValue,
            bool includeReleased = true) =>
            await TryExecuteTask(ClientRefit.GetDraftTasks(accountId, draftId, skip, take, includeReleased));

        public async Task<ApiTaskResult<CryptOperationStatusResult>> GetDraftTask(Guid accountId, Guid draftId, Guid apiTaskId) =>
            await TryExecuteTask(ClientRefit.GetDraftTask(accountId, draftId, apiTaskId));

        public async Task<SignInitResult> CloudSignDraftAsync(Guid accountId, Guid draftId) =>
            await TryExecuteTask(ClientRefit.CloudSignDraftAsync(accountId, draftId));

        public async Task<SignResult> CloudSignConfirmDraftAsync(Guid accountId, Guid draftId, Guid requestId, string code) =>
            await TryExecuteTask(ClientRefit.CloudSignConfirmDraftAsync(accountId, draftId, requestId, code));
    }
}
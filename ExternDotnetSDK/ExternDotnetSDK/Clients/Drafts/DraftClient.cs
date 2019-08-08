using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts;
using ExternDotnetSDK.Models.Drafts.Meta;
using ExternDotnetSDK.Models.Drafts.Requests;
using Refit;

namespace ExternDotnetSDK.Clients.Drafts
{
    public class DraftClient : IDraftClient
    {
        public IDraftClientRefit ClientRefit { get; }

        public DraftClient(HttpClient client) => ClientRefit = RestService.For<IDraftClientRefit>(client);

        public async Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest) =>
            await ClientRefit.CreateDraftAsync(accountId, draftRequest);

        public async Task DeleteDraftAsync(Guid accountId, Guid draftId) =>
            await ClientRefit.DeleteDraftAsync(accountId, draftId);

        public async Task<Draft> GetDraftAsync(Guid accountId, Guid draftId) =>
            await ClientRefit.GetDraftAsync(accountId, draftId);

        public async Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId) =>
            await ClientRefit.GetDraftMetaAsync(accountId, draftId);

        public async Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, DraftMetaRequest newMeta) =>
            await ClientRefit.UpdateDraftMetaAsync(accountId, draftId, newMeta);

        public async Task<DraftDocument> AddDocumentAsync(Guid accountId, Guid draftId, [Body] DocumentContents content) =>
            await ClientRefit.AddDocumentAsync(accountId, draftId, content);

        public async Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await ClientRefit.DeleteDocumentAsync(accountId, draftId, documentId);

        public async Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await ClientRefit.GetDocumentAsync(accountId, draftId, documentId);

        public async Task<DraftDocument> UpdateDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentContents content) => await ClientRefit.UpdateDocumentAsync(accountId, draftId, documentId, content);

        public async Task<string> GetDocumentPrintAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await ClientRefit.GetDocumentPrintAsync(accountId, draftId, documentId);

        public async Task<string> GetDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await ClientRefit.GetDocumentDecryptedContentAsync(accountId, draftId, documentId);

        public async Task UpdateDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content)
        {
            var convertedContent = Convert.ToBase64String(content);
            await ClientRefit.UpdateDocumentDecryptedContentAsync(accountId, draftId, documentId, convertedContent);
        }

        public async Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await ClientRefit.GetDocumentSignatureContentAsync(accountId, draftId, documentId);

        public async Task UpdateDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content)
        {
            var convertedContent = Convert.ToBase64String(content);
            await ClientRefit.UpdateDocumentSignatureContentAsync(accountId, draftId, documentId, convertedContent);
        }

        public async Task<Signature> AddDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            SignatureRequest request = null) =>
            await ClientRefit.AddDocumentSignatureAsync(accountId, draftId, documentId, request);

        public async Task DeleteDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId) =>
            await ClientRefit.DeleteDocumentSignatureAsync(accountId, draftId, documentId, signatureId);

        public async Task<Signature> GetDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId) =>
            await ClientRefit.GetDocumentSignatureAsync(accountId, draftId, documentId, signatureId);

        public async Task<Signature> UpdateDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest request) =>
            await ClientRefit.UpdateDocumentSignatureAsync(accountId, draftId, documentId, signatureId, request);

        public async Task<string> GetDocumentSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId) => await ClientRefit.GetDocumentSignatureContentAsync(accountId, draftId, documentId, signatureId);

        public async Task<string> CheckDraftAsync(Guid accountId, Guid draftId, bool deferred = false) =>
            await ClientRefit.CheckDraftAsync(accountId, draftId, deferred);

        public async Task<string> PrepareDraftAsync(Guid accountId, Guid draftId, bool deferred = false) =>
            await ClientRefit.PrepareDraftAsync(accountId, draftId, deferred);

        public async Task<string> SendDraftAsync(Guid accountId, Guid draftId, bool deferred = false, bool force = false) =>
            await ClientRefit.SendDraftAsync(accountId, draftId, deferred, force);

        public async Task<string> GetDocumentEncryptedContentAsync(Guid accountId, Guid draftId, Guid documentId) =>
            await ClientRefit.GetDocumentEncryptedContentAsync(accountId, draftId, documentId);

        public async Task BuildDocumentContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            FormatType type,
            string content) => await ClientRefit.BuildDocumentContentAsync(
            accountId,
            draftId,
            documentId,
            type.ToString(),
            1,
            content);

        public async Task<DraftDocument> CreateDocumentWithContentFromFormatAsync(
            Guid accountId,
            Guid draftId,
            FormatType type,
            string content) => await ClientRefit.CreateDocumentWithContentFromFormatAsync(
            accountId,
            draftId,
            type.ToString(),
            1,
            content);

        public async Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            long skip = 0,
            int take = int.MaxValue,
            bool includeReleased = true) =>
            await ClientRefit.GetDraftTasks(accountId, draftId, skip, take, includeReleased);

        public async Task<ApiTaskResult<CryptOperationStatusResult>> GetDraftTask(Guid accountId, Guid draftId, Guid apiTaskId) =>
            await ClientRefit.GetDraftTask(accountId, draftId, apiTaskId);

        public async Task<SignInitResult> CloudSignDraftAsync(Guid accountId, Guid draftId) =>
            await ClientRefit.CloudSignDraftAsync(accountId, draftId);

        public async Task<SignResult> CloudSignConfirmDraftAsync(Guid accountId, Guid draftId, Guid requestId, string code) =>
            await ClientRefit.CloudSignConfirmDraftAsync(accountId, draftId, requestId, code);
    }
}
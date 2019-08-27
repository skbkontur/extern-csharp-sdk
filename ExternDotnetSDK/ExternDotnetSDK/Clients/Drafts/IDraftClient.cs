using System;
using System.Threading.Tasks;
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
    public interface IDraftClient
    {
        Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest, TimeSpan? timeout = null);
        Task DeleteDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);
        Task<Draft> GetDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);
        Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);
        Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, DraftMetaRequest newMeta, TimeSpan? timeout = null);
        Task<DraftDocument> AddDocumentAsync(Guid accountId, Guid draftId, DocumentContents content, TimeSpan? timeout = null);
        Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);
        Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);

        Task<DraftDocument> UpdateDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentContents content,
            TimeSpan? timeout = null);

        Task<string> GetDocumentPrintAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);
        Task<string> GetDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);

        Task UpdateDocumentDecryptedContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            byte[] content,
            TimeSpan? timeout = null);

        Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);

        Task UpdateDocumentSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            byte[] content,
            TimeSpan? timeout = null);

        Task<Signature> AddDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            SignatureRequest request = null,
            TimeSpan? timeout = null);

        Task DeleteDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        Task<Signature> GetDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        Task<Signature> UpdateDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest request,
            TimeSpan? timeout = null);

        Task<string> GetDocumentSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        Task<CheckResult> CheckDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);
        Task<ApiTaskResult<CheckResult>> StartCheckDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);
        Task<PrepareResult> PrepareDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);
        Task<ApiTaskResult<PrepareResult>> StartPrepareDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);
        Task<Docflow> SendDraftAsync(Guid accountId, Guid draftId, bool force = false, TimeSpan? timeout = null);

        Task<ApiTaskResult<Docflow>> StartSendDraftAsync(
            Guid accountId,
            Guid draftId,
            bool force = false,
            TimeSpan? timeout = null);

        Task<string> GetDocumentEncryptedContentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null);

        Task BuildDocumentContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            FormatType type,
            string content,
            TimeSpan? timeout = null);

        Task<DraftDocument> CreateDocumentWithContentFromFormatAsync(
            Guid accountId,
            Guid draftId,
            FormatType type,
            int version,
            string content,
            TimeSpan? timeout = null);

        Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            long skip = 0,
            int take = int.MaxValue,
            bool includeReleased = true,
            TimeSpan? timeout = null);

        Task<ApiTaskResult<CryptOperationStatusResult>> GetDraftTask(
            Guid accountId,
            Guid draftId,
            Guid apiTaskId,
            TimeSpan? timeout = null);

        Task<SignInitResult> CloudSignDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null);

        Task<SignResult> CloudSignConfirmDraftAsync(
            Guid accountId,
            Guid draftId,
            Guid requestId,
            string code,
            TimeSpan? timeout = null);
    }
}
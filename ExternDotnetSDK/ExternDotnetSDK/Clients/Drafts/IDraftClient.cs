using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Drafts;
using ExternDotnetSDK.Models.Drafts.Check;
using ExternDotnetSDK.Models.Drafts.Meta;
using ExternDotnetSDK.Models.Drafts.Prepare;
using ExternDotnetSDK.Models.Drafts.Requests;
using Refit;

namespace ExternDotnetSDK.Clients.Drafts
{
    public interface IDraftClient
    {
        Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest);
        Task DeleteDraftAsync(Guid accountId, Guid draftId);
        Task<Draft> GetDraftAsync(Guid accountId, Guid draftId);
        Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId);
        Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, DraftMetaRequest newMeta);
        Task<DraftDocument> AddDocumentAsync(Guid accountId, Guid draftId, [Body] DocumentContents content);
        Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId);
        Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId);
        Task<DraftDocument> UpdateDocumentAsync(Guid accountId, Guid draftId, Guid documentId, DocumentContents content);

        //todo make test when it works properly
        Task<string> GetDocumentPrintAsync(Guid accountId, Guid draftId, Guid documentId);

        Task<string> GetDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId);
        Task UpdateDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content);
        Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId);
        Task UpdateDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content);
        Task<Signature> AddDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, SignatureRequest request = null);
        Task DeleteDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId);
        Task<Signature> GetDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId);

        Task<Signature> UpdateDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest request);

        Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId);

        //todo make test when this method works properly
        Task<CheckResult> CheckDraftAsync(Guid accountId, Guid draftId);

        Task<ApiTaskResult<CheckResult>> StartCheckDraftAsync(Guid accountId, Guid draftId);

        //todo make test when this method works properly
        Task<PrepareResult> PrepareDraftAsync(Guid accountId, Guid draftId);

        Task<ApiTaskResult<PrepareResult>> StartPrepareDraftAsync(Guid accountId, Guid draftId);

        //todo make test when this method works properly
        Task<Docflow> SendDraftAsync(Guid accountId, Guid draftId, bool force = false);

        Task<ApiTaskResult<Docflow>> StartSendDraftAsync(Guid accountId, Guid draftId, bool force = false);

        //todo make test when this method works properly
        Task<string> GetDocumentEncryptedContentAsync(Guid accountId, Guid draftId, Guid documentId);

        //todo make test when this method works properly
        Task BuildDocumentContentAsync(Guid accountId, Guid draftId, Guid documentId, FormatType type, string content);

        //todo make test when this method works properly
        Task<DraftDocument> CreateDocumentWithContentFromFormatAsync(
            Guid accountId,
            Guid draftId,
            FormatType type,
            int version,
            string content);

        Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            long skip = 0,
            int take = int.MaxValue,
            bool includeReleased = true);

        Task<ApiTaskResult<CryptOperationStatusResult>> GetDraftTask(Guid accountId, Guid draftId, Guid apiTaskId);

        //todo make test when this method works properly
        Task<SignInitResult> CloudSignDraftAsync(Guid accountId, Guid draftId);

        //todo make tests
        Task<SignResult> CloudSignConfirmDraftAsync(Guid accountId, Guid draftId, Guid requestId, string code);
    }
}
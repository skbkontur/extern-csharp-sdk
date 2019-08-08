using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts;
using ExternDotnetSDK.Models.Drafts.Meta;
using ExternDotnetSDK.Models.Drafts.Requests;
using Refit;

namespace ExternDotnetSDK.Clients.Drafts
{
    public interface IDraftClientRefit
    {
        [Post("/v1/{accountId}/drafts")]
        Task<Draft> CreateDraftAsync(Guid accountId, [Body] DraftMetaRequest draftRequest);

        [Delete("/v1/{accountId}/drafts/{draftId}")]
        Task DeleteDraftAsync(Guid accountId, Guid draftId);

        [Get("/v1/{accountId}/drafts/{draftId}")]
        Task<Draft> GetDraftAsync(Guid accountId, Guid draftId);

        [Get("/v1/{accountId}/drafts/{draftId}/meta")]
        Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId);

        [Put("/v1/{accountId}/drafts/{draftId}/meta")]
        Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, [Body] DraftMetaRequest newMeta);

        [Post("/v1/{accountId}/drafts/{draftId}/documents")]
        Task<DraftDocument> AddDocumentAsync(Guid accountId, Guid draftId, [Body] DocumentContents content);

        [Delete("/v1/{accountId}/drafts/{draftId}/documents/{documentId}")]
        Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId);

        [Get("/v1/{accountId}/drafts/{draftId}/documents/{documentId}")]
        Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId);

        [Put("/v1/{accountId}/drafts/{draftId}/documents/{documentId}")]
        Task<DraftDocument> UpdateDocumentAsync(Guid accountId, Guid draftId, Guid documentId, [Body] DocumentContents content);

        //todo make test when it works properly
        [Get("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print")]
        Task<string> GetDocumentPrintAsync(Guid accountId, Guid draftId, Guid documentId);

        [Get("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/decrypted-content")]
        Task<string> GetDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId);

        [Put("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/decrypted-content")]
        Task UpdateDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId, [Body] string content);

        [Get("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signature")]
        Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId);

        [Put("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signature")]
        Task UpdateDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, [Body] string content);

        [Post("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures")]
        Task<Signature> AddDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, [Body] SignatureRequest request);

        [Delete("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}")]
        Task DeleteDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId);

        [Get("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}")]
        Task<Signature> GetDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId);

        [Put("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}")]
        Task<Signature> UpdateDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            [Body] SignatureRequest request);

        [Get("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}/content")]
        Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId);

        //todo make test when this method works properly && deferred = false
        //todo JSON string doesn't return what is written in swagger. Object looks like ApiTaskResult, i'll leave a string and let users deserialize themselves
        [Post("/v1/{accountId}/drafts/{draftId}/check")]
        Task<string> CheckDraftAsync(Guid accountId, Guid draftId, bool deferred = false);

        //todo make test when this method works properly && deferred = false
        //todo JSON string doesn't return what is written in swagger. Object looks like ApiTaskResult, i'll leave a string and let users deserialize themselves
        //this method implicitly invokes actions that are invoked by CheckDraftAsync method
        [Post("/v1/{accountId}/drafts/{draftId}/prepare")]
        Task<string> PrepareDraftAsync(Guid accountId, Guid draftId, bool deferred = false);

        //todo make test when this method works properly && deferred = false
        //todo JSON string doesn't return what is written in swagger. Object looks like ApiTaskResult, i'll leave a string and let users deserialize themselves
        //this method implicitly invokes actions that are invoked by PrepareDraftAsync method
        [Post("/v1/{accountId}/drafts/{draftId}/send")]
        Task<string> SendDraftAsync(Guid accountId, Guid draftId, bool deferred = false, bool force = false);

        //todo make test when this method works properly
        [Get("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/encrypted-content")]
        Task<string> GetDocumentEncryptedContentAsync(Guid accountId, Guid draftId, Guid documentId);

        //todo make test when this method works properly
        [Post("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/build")]
        Task BuildDocumentContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            string type,
            int version,
            [Body] string content);

        //todo make test when this method works properly
        [Post("/v1/{accountId}/drafts/{draftId}/build-document")]
        Task<DraftDocument> CreateDocumentWithContentFromFormatAsync(
            Guid accountId,
            Guid draftId,
            string type,
            int version,
            [Body] string content);

        [Get("/v1/{accountId}/drafts/{draftId}/tasks")]
        Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            long skip = 0,
            int take = int.MaxValue,
            bool includeReleased = true);

        [Get("/v1/{accountId}/drafts/{draftId}/tasks/{apiTaskId}")]
        Task<ApiTaskResult<CryptOperationStatusResult>> GetDraftTask(Guid accountId, Guid draftId, Guid apiTaskId);

        //todo make test when this method works properly
        [Post("/v1/{accountId}/drafts/{draftId}/cloud-sign")]
        Task<SignInitResult> CloudSignDraftAsync(Guid accountId, Guid draftId);

        //todo make tests
        [Post("/v1/{accountId}/drafts/{draftId}/cloud-sign-confirm")]
        Task<SignResult> CloudSignConfirmDraftAsync(Guid accountId, Guid draftId, Guid requestId, string code);
    }
}
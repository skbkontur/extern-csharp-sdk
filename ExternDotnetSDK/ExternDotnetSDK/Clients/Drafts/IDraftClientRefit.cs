using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.Drafts;
using ExternDotnetSDK.Drafts.Meta;
using ExternDotnetSDK.Drafts.Requests;
using Refit;

namespace ExternDotnetSDK.Clients.Drafts
{
    internal interface IDraftClientRefit
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
    }
}
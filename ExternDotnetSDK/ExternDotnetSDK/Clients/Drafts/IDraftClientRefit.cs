using System;
using System.Threading.Tasks;
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

        [Get("/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print")]
        Task<string> GetDocumentPrintAsync(Guid accountId, Guid draftId, Guid documentId);
    }
}
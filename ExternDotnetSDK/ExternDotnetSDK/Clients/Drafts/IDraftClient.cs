using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts;
using ExternDotnetSDK.Models.Drafts.Meta;
using ExternDotnetSDK.Models.Drafts.Requests;
using Refit;

namespace ExternDotnetSDK.Clients.Drafts
{
    public interface IDraftClient
    {
        IDraftClientRefit ClientRefit { get; }

        Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest);
        Task DeleteDraftAsync(Guid accountId, Guid draftId);
        Task<Draft> GetDraftAsync(Guid accountId, Guid draftId);
        Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId);
        Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, DraftMetaRequest newMeta);
        Task<DraftDocument> AddDocumentAsync(Guid accountId, Guid draftId, [Body] DocumentContents content);
        Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId);
        Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId);
        Task<DraftDocument> UpdateDocumentAsync(Guid accountId, Guid draftId, Guid documentId, DocumentContents content);
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
    }
}
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts;
using ExternDotnetSDK.Models.Drafts.Meta;
using ExternDotnetSDK.Models.Drafts.Requests;
using Refit;

namespace ExternDotnetSDK.Clients.Drafts
{
    public class DraftClient : IDraftClient
    {
        public DraftClient(HttpClient client)
        {
            ClientRefit = RestService.For<IDraftClientRefit>(client);
        }

        public IDraftClientRefit ClientRefit { get; }

        public async Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest)
        {
            return await ClientRefit.CreateDraftAsync(accountId, draftRequest);
        }

        public async Task DeleteDraftAsync(Guid accountId, Guid draftId)
        {
            await ClientRefit.DeleteDraftAsync(accountId, draftId);
        }

        public async Task<Draft> GetDraftAsync(Guid accountId, Guid draftId)
        {
            return await ClientRefit.GetDraftAsync(accountId, draftId);
        }

        public async Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId)
        {
            return await ClientRefit.GetDraftMetaAsync(accountId, draftId);
        }

        public async Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, DraftMetaRequest newMeta)
        {
            return await ClientRefit.UpdateDraftMetaAsync(accountId, draftId, newMeta);
        }

        public async Task<DraftDocument> AddDocumentAsync(Guid accountId, Guid draftId, [Body] DocumentContents content)
        {
            return await ClientRefit.AddDocumentAsync(accountId, draftId, content);
        }

        public async Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId)
        {
            await ClientRefit.DeleteDocumentAsync(accountId, draftId, documentId);
        }

        public async Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId)
        {
            return await ClientRefit.GetDocumentAsync(accountId, draftId, documentId);
        }

        public async Task<DraftDocument> UpdateDocumentAsync(Guid accountId, Guid draftId, Guid documentId, DocumentContents content)
        {
            return await ClientRefit.UpdateDocumentAsync(accountId, draftId, documentId, content);
        }

        public async Task<string> GetDocumentPrintAsync(Guid accountId, Guid draftId, Guid documentId)
        {
            return await ClientRefit.GetDocumentPrintAsync(accountId, draftId, documentId);
        }

        public async Task<string> GetDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId)
        {
            return await ClientRefit.GetDocumentDecryptedContentAsync(accountId, draftId, documentId);
        }

        public async Task UpdateDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content)
        {
            var convertedContent = Convert.ToBase64String(content);
            await ClientRefit.UpdateDocumentDecryptedContentAsync(accountId, draftId, documentId, convertedContent);
        }

        public async Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId)
        {
            return await ClientRefit.GetDocumentSignatureContentAsync(accountId, draftId, documentId);
        }

        public async Task UpdateDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content)
        {
            var convertedContent = Convert.ToBase64String(content);
            await ClientRefit.UpdateDocumentSignatureContentAsync(accountId, draftId, documentId, convertedContent);
        }

        public async Task<Signature> AddDocumentSignatureAsync(
            Guid accountId, Guid draftId, Guid documentId, SignatureRequest request = null)
        {
            return await ClientRefit.AddDocumentSignatureAsync(accountId, draftId, documentId, request);
        }

        public async Task DeleteDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId)
        {
            await ClientRefit.DeleteDocumentSignatureAsync(accountId, draftId, documentId, signatureId);
        }

        public async Task<Signature> GetDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId)
        {
            return await ClientRefit.GetDocumentSignatureAsync(accountId, draftId, documentId, signatureId);
        }

        public async Task<Signature> UpdateDocumentSignatureAsync(
            Guid accountId, Guid draftId, Guid documentId, Guid signatureId, SignatureRequest request)
        {
            return await ClientRefit.UpdateDocumentSignatureAsync(accountId, draftId, documentId, signatureId, request);
        }

        public async Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId)
        {
            return await ClientRefit.GetDocumentSignatureContentAsync(accountId, draftId, documentId, signatureId);
        }
    }
}
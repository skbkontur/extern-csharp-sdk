using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.Drafts;
using ExternDotnetSDK.Drafts.Meta;
using ExternDotnetSDK.Drafts.Requests;
using Refit;

namespace ExternDotnetSDK.Clients.Drafts
{
    public class DraftClient 
    {
        private readonly IDraftClientRefit clientRefit;

        public DraftClient(HttpClient client)
        {
            clientRefit = RestService.For<IDraftClientRefit>(client);
        }

        public async Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest draftRequest)
        {
            return await clientRefit.CreateDraftAsync(accountId, draftRequest);
        }

        public async Task DeleteDraftAsync(Guid accountId, Guid draftId)
        {
            await clientRefit.DeleteDraftAsync(accountId, draftId);
        }

        public async Task<Draft> GetDraftAsync(Guid accountId, Guid draftId)
        {
            return await clientRefit.GetDraftAsync(accountId, draftId);
        }

        public async Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId)
        {
            return await clientRefit.GetDraftMetaAsync(accountId, draftId);
        }

        public async Task<DraftMeta> UpdateDraftMetaAsync(Guid accountId, Guid draftId, DraftMetaRequest newMeta)
        {
            return await clientRefit.UpdateDraftMetaAsync(accountId, draftId, newMeta);
        }

        public async Task<DraftDocument> AddDocumentAsync(Guid accountId, Guid draftId, [Body] DocumentContents content)
        {
            return await clientRefit.AddDocumentAsync(accountId, draftId, content);
        }

        public async Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId)
        {
            await clientRefit.DeleteDocumentAsync(accountId, draftId, documentId);
        }

        public async Task<DraftDocument> GetDocumentAsync(Guid accountId, Guid draftId, Guid documentId)
        {
            return await clientRefit.GetDocumentAsync(accountId, draftId, documentId);
        }

        public async Task<DraftDocument> UpdateDocumentAsync(Guid accountId, Guid draftId, Guid documentId, DocumentContents content)
        {
            return await clientRefit.UpdateDocumentAsync(accountId, draftId, documentId, content);
        }

        public async Task<string> GetDocumentPrintAsync(Guid accountId, Guid draftId, Guid documentId)
        {
            return await clientRefit.GetDocumentPrintAsync(accountId, draftId, documentId);
        }

        public async Task<string> GetDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId)
        {
            return await clientRefit.GetDocumentDecryptedContentAsync(accountId, draftId, documentId);
        }

        public async Task UpdateDocumentDecryptedContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content)
        {
            var convertedContent = Convert.ToBase64String(content);
            await clientRefit.UpdateDocumentDecryptedContentAsync(accountId, draftId, documentId, convertedContent);
        }

        public async Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId)
        {
            return await clientRefit.GetDocumentSignatureContentAsync(accountId, draftId, documentId);
        }

        public async Task UpdateDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, byte[] content)
        {
            var convertedContent = Convert.ToBase64String(content);
            await clientRefit.UpdateDocumentSignatureContentAsync(accountId, draftId, documentId, convertedContent);
        }

        public async Task<Signature> AddDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, SignatureRequest request)
        {
            return await clientRefit.AddDocumentSignatureAsync(accountId, draftId, documentId, request);
        }

        public async Task DeleteDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId)
        {
            await clientRefit.DeleteDocumentSignatureAsync(accountId, draftId, documentId, signatureId);
        }

        public async Task<Signature> GetDocumentSignatureAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId)
        {
            return await clientRefit.GetDocumentSignatureAsync(accountId, draftId, documentId, signatureId);
        }

        public async Task<Signature> UpdateDocumentSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest request)
        {
            return await clientRefit.UpdateDocumentSignatureAsync(accountId, draftId, documentId, signatureId, request);
        }

        public async Task<string> GetDocumentSignatureContentAsync(Guid accountId, Guid draftId, Guid documentId, Guid signatureId)
        {
            return await clientRefit.GetDocumentSignatureContentAsync(accountId, draftId, documentId, signatureId);
        }
    }
}
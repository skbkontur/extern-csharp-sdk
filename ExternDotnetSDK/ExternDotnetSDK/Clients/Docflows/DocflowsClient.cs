using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Api;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.Docflows;
using ExternDotnetSDK.Documents;
using Refit;

namespace ExternDotnetSDK.Clients.Docflows
{
    public class DocflowsClient
    {
        private readonly IDocflowsClientRefit clientRefit;

        public DocflowsClient(HttpClient client) => 
            clientRefit = RestService.For<IDocflowsClientRefit>(client);

        public async Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null) =>
            await clientRefit.GetDocflowsAsync(accountId, filter ?? new DocflowFilter());

        public async Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId) =>
            await clientRefit.GetDocflowAsync(accountId, docflowId);

        public async Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId) =>
            await clientRefit.GetDocumentsAsync(accountId, docflowId);

        public async Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await clientRefit.GetDocumentAsync(accountId, docflowId, documentId);

        public async Task<DocflowDocumentDescription>
            GetDocumentDescriptionAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await clientRefit.GetDocumentDescriptionAsync(accountId, docflowId, documentId);

        public async Task<byte[]> GetEncryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await clientRefit.GetEncryptedDocumentContentAsync(accountId, docflowId, documentId);

        public async Task<byte[]> GetDecryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await clientRefit.GetDecryptedDocumentContentAsync(accountId, docflowId, documentId);

        public async Task<List<Signature>> GetDocumentSignaturesAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await clientRefit.GetDocumentSignaturesAsync(accountId, docflowId, documentId);

        public async Task<Signature> GetSignatureAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId) =>
            await clientRefit.GetSignatureAsync(accountId, docflowId, documentId, signatureId);

        public async Task<byte[]> GetSignatureContentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId) =>
            await clientRefit.GetSignatureContentAsync(accountId, docflowId, documentId, signatureId);

        public async Task<ApiTaskResult<byte[]>> GetApiTaskAsync(Guid accountId, Guid docflowId, Guid documentId, Guid apiTaskId) =>
            await clientRefit.GetApiTaskAsync(accountId, docflowId, documentId, apiTaskId);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExternDotnetSDK.Api;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.Docflows;
using ExternDotnetSDK.Documents;
using Refit;

namespace ExternDotnetSDK.Clients.Docflows
{
    internal interface IDocflowsClientRefit
    {
        [Get("/v1/{accountId}/docflows")]
        Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter);

        [Get("/v1/{accountId}/docflows/{docflowId}")]
        Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId);

        [Get("/v1/{accountId}/docflows/{docflowId}/documents")]
        Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId);

        [Get("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}")]
        Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId);

        [Get("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/description")]
        Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(Guid accountId, Guid docflowId, Guid documentId);

        [Get("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/encrypted-content")]
        Task<byte[]> GetEncryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId);

        [Get("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypted-content")]
        Task<byte[]> GetDecryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId);

        [Get("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures")]
        Task<List<Signature>> GetDocumentSignaturesAsync(Guid accountId, Guid docflowId, Guid documentId);

        [Get("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}")]
        Task<Signature> GetSignatureAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId);

        [Get("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}/content")]
        Task<byte[]> GetSignatureContentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId);

        [Get("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/tasks/{apiTaskId}")]
        Task<ApiTaskResult<byte[]>> GetApiTaskAsync(Guid accountId, Guid docflowId, Guid documentId, Guid apiTaskId);
    }
}
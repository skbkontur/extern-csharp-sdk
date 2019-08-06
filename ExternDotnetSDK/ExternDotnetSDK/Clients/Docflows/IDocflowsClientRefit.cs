using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Documents;
using ExternDotnetSDK.Models.Documents.Data;
using Refit;

namespace ExternDotnetSDK.Clients.Docflows
{
    public interface IDocflowsClientRefit
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

        //todo add test where it works with valid parameters
        [Get("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/tasks/{apiTaskId}")]
        Task<ApiTaskResult<byte[]>> GetApiTaskAsync(Guid accountId, Guid docflowId, Guid documentId, Guid apiTaskId);

        //todo add test where it works with valid parameters
        [Get("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}")]
        Task<ApiReplyDocument> GetDocumentReplyAsync(Guid accountId, Guid docflowId, Guid documentId, Guid replyId);

        //todo add test where it works with valid parameters
        [Post("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/print")]
        Task<string> PrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, [Body] PrintDocumentData data);

        //todo add test where it works with valid parameters
        [Post("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content")]
        Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId, Guid docflowId, Guid documentId, [Body] DecryptDocumentRequestData data);

        //todo make tests when you know where to find required data
        [Post("/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content-confirm")]
        Task<byte> ConfirmDocumentContentDecryptionAsync(
            Guid accountId, Guid docflowId, Guid documentId, string requestId, string code, bool unzip = false);

        //todo make all Post methods for docflows and all methods for docflows: replies
    }
}
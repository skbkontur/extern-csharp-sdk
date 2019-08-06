using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Documents;
using ExternDotnetSDK.Models.Documents.Data;

namespace ExternDotnetSDK.Clients.Docflows
{
    public interface IDocflowsClient
    {
        IDocflowsClientRefit ClientRefit { get; }

        Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null);
        Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId);
        Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId);
        Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId);
        Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(Guid accountId, Guid docflowId, Guid documentId);
        Task<byte[]> GetEncryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId);
        Task<byte[]> GetDecryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId);
        Task<List<Signature>> GetDocumentSignaturesAsync(Guid accountId, Guid docflowId, Guid documentId);
        Task<Signature> GetSignatureAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId);
        Task<byte[]> GetSignatureContentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId);
        Task<ApiTaskResult<byte[]>> GetApiTaskAsync(Guid accountId, Guid docflowId, Guid documentId, Guid apiTaskId);
        Task<ApiReplyDocument> GetDocumentReplyAsync(Guid accountId, Guid docflowId, Guid documentId, Guid replyId);
        Task<string> PrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, byte[] data);
        Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId, Guid docflowId, Guid documentId, DecryptDocumentRequestData data);
        Task<byte> ConfirmDocumentContentDecryptionAsync(
            Guid accountId,Guid docflowId,Guid documentId,string requestId,string code,bool unzip = false);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Documents;
using ExternDotnetSDK.Models.Documents.Data;
using ExternDotnetSDK.Models.Drafts;

namespace ExternDotnetSDK.Clients.Docflows
{
    public interface IDocflowsClient : IHttpClient
    {
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

        //todo add test where it works with valid parameters
        Task<ApiTaskResult<byte[]>> GetApiTaskAsync(Guid accountId, Guid docflowId, Guid documentId, Guid apiTaskId);

        //todo add test where it works with valid parameters
        Task<ApiReplyDocument> GetDocumentReplyAsync(Guid accountId, Guid docflowId, Guid documentId, Guid replyId);

        //todo add test where it works with valid parameters
        Task<string> PrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, byte[] data);

        //todo add test where it works with valid parameters
        Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            DecryptDocumentRequestData data);

        //todo make tests when you know where to find required data
        Task<byte> ConfirmDocumentContentDecryptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool unzip = false);

        //todo add test where it works with valid parameters
        Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent);

        //todo add test where it works with valid parameters and documents that support recognition
        Task<RecognizedMeta> RecognizeDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, byte[] content);

        //todo add tests after "GenerateDocumentReplyAsync" method has valid parameters to work with
        Task<Docflow> SendDocumentReplyAsync(Guid accountId, Guid docflowId, Guid documentId, Guid replyId, string senderIp);

        //todo add tests after "GenerateDocumentReplyAsync" method has valid parameters to work with
        Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature);

        //todo add tests after "GenerateDocumentReplyAsync" method has valid parameters to work with
        Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content);

        //todo add tests after "GenerateDocumentReplyAsync" method has valid parameters to work with
        Task<SignInitResult> CloudSignDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            bool forceConfirmation);

        //todo add tests after "GenerateDocumentReplyAsync" method has valid parameters to work with
        Task<SignResult> CloudSignConfirmDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string code,
            string requestId);
    }
}
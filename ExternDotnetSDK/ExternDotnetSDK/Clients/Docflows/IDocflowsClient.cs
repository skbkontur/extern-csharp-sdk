using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Documents;
using Kontur.Extern.Client.Models.Documents.Data;
using Kontur.Extern.Client.Models.Drafts;

namespace Kontur.Extern.Client.Clients.Docflows
{
    public interface IDocflowsClient
    {
        Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null, TimeSpan? timeout = null);
        Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null);
        Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null);
        Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null);

        Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null);

        Task<byte[]> GetEncryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null);
        Task<byte[]> GetDecryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null);

        Task<List<Signature>> GetDocumentSignaturesAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null);

        Task<Signature> GetSignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null);

        Task<ApiTaskResult<byte[]>> GetApiTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid apiTaskId,
            TimeSpan? timeout = null);

        Task<ApiReplyDocument> GetDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null);

        Task<string> PrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, byte[] data, TimeSpan? timeout = null);

        Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            DecryptDocumentRequestData data,
            TimeSpan? timeout = null);

        Task<byte> ConfirmDocumentContentDecryptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool unzip = false,
            TimeSpan? timeout = null);

        Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent,
            TimeSpan? timeout = null);

        Task<RecognizedMeta> RecognizeDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] content,
            TimeSpan? timeout = null);

        Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string senderIp,
            TimeSpan? timeout = null);

        Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null);

        Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null);

        Task<SignInitResult> CloudSignDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            bool forceConfirmation,
            TimeSpan? timeout = null);

        Task<SignResult> CloudSignConfirmDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string code,
            string requestId,
            TimeSpan? timeout = null);

        Task<DocflowPage> GetRelatedDocflows(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter,
            TimeSpan? timeout = null);
    }
}
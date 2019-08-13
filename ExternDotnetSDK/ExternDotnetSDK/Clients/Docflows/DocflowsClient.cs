using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Documents;
using ExternDotnetSDK.Models.Documents.Data;
using ExternDotnetSDK.Models.Drafts;
using Refit;

namespace ExternDotnetSDK.Clients.Docflows
{
    public class DocflowsClient : InnerCommonClient, IDocflowsClient
    {
        public DocflowsClient(ILog log, HttpClient client)
            : base(log) =>
            ClientRefit = RestService.For<IDocflowsClientRefit>(client);

        public IDocflowsClientRefit ClientRefit { get; }

        public async Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null) =>
            await TryExecuteTask(ClientRefit.GetDocflowsAsync(accountId, filter ?? new DocflowFilter()));

        public async Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId) =>
            await TryExecuteTask(ClientRefit.GetDocflowAsync(accountId, docflowId));

        public async Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId) =>
            await TryExecuteTask(ClientRefit.GetDocumentsAsync(accountId, docflowId));

        public async Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.GetDocumentAsync(accountId, docflowId, documentId));

        public async Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId) => await TryExecuteTask(ClientRefit.GetDocumentDescriptionAsync(accountId, docflowId, documentId));

        public async Task<byte[]> GetEncryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.GetEncryptedDocumentContentAsync(accountId, docflowId, documentId));

        public async Task<byte[]> GetDecryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.GetDecryptedDocumentContentAsync(accountId, docflowId, documentId));

        public async Task<List<Signature>> GetDocumentSignaturesAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await TryExecuteTask(ClientRefit.GetDocumentSignaturesAsync(accountId, docflowId, documentId));

        public async Task<Signature> GetSignatureAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId) =>
            await TryExecuteTask(ClientRefit.GetSignatureAsync(accountId, docflowId, documentId, signatureId));

        public async Task<byte[]> GetSignatureContentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId) =>
            await TryExecuteTask(ClientRefit.GetSignatureContentAsync(accountId, docflowId, documentId, signatureId));

        public async Task<ApiTaskResult<byte[]>> GetApiTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid apiTaskId) => await TryExecuteTask(ClientRefit.GetApiTaskAsync(accountId, docflowId, documentId, apiTaskId));

        public async Task<ApiReplyDocument> GetDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId) => await TryExecuteTask(ClientRefit.GetDocumentReplyAsync(accountId, docflowId, documentId, replyId));

        public async Task<string> PrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, byte[] data) =>
            await TryExecuteTask(ClientRefit.PrintDocumentAsync(
                accountId,
                docflowId,
                documentId,
                new PrintDocumentData {Content = Convert.ToBase64String(data)}));

        public async Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            DecryptDocumentRequestData data) =>
            await TryExecuteTask(ClientRefit.DecryptDocumentContentAsync(accountId, docflowId, documentId, data));

        public async Task<byte> ConfirmDocumentContentDecryptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool unzip = false) =>
            await TryExecuteTask(
                ClientRefit.ConfirmDocumentContentDecryptionAsync(accountId, docflowId, documentId, requestId, code, unzip));

        public async Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent) => await TryExecuteTask(
            ClientRefit.GenerateDocumentReplyAsync(
                accountId,
                docflowId,
                documentId,
                documentType.ToString(),
                new GenerateReplyDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)}));

        public async Task<RecognizedMeta> RecognizeDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] content) => await TryExecuteTask(ClientRefit.RecognizeDocumentAsync(accountId, docflowId, documentId, content));

        public async Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string senderIp) => await TryExecuteTask(
            ClientRefit.SendDocumentReplyAsync(
                accountId,
                docflowId,
                documentId,
                replyId,
                new SendReplyDocumentRequest {SenderIp = senderIp}));

        public async Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature) => await TryExecuteTask(
            ClientRefit.UpdateDocumentReplySignatureAsync(
                accountId,
                docflowId,
                documentId,
                replyId,
                Convert.ToBase64String(signature)));

        public async Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content) => await TryExecuteTask(
            ClientRefit.UpdateDocumentReplyContentAsync(
                accountId,
                docflowId,
                documentId,
                replyId,
                Convert.ToBase64String(content)));

        public async Task<string> CloudSignDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            bool forceConfirmation) => await TryExecuteTask(
            ClientRefit.CloudSignDocumentReplyAsync(
                accountId,
                docflowId,
                documentId,
                replyId,
                forceConfirmation));

        public async Task<SignResult> CloudSignConfirmDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string code,
            string requestId) => await TryExecuteTask(
            ClientRefit.CloudSignConfirmDocumentReplyAsync(
                accountId,
                docflowId,
                documentId,
                replyId,
                code,
                requestId));
    }
}
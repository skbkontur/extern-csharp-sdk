using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Documents;
using ExternDotnetSDK.Models.Documents.Data;
using ExternDotnetSDK.Models.Drafts;
using Refit;

namespace ExternDotnetSDK.Clients.InventoryDocflows
{
    public class InventoryDocflowsClient : IInventoryDocflowsClient
    {
        public IInventoryDocflowsClientRefit ClientRefit { get; }

        public async Task<DocflowPage> GetAllInventoryDocflowsAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter = null) =>
            await ClientRefit.GetAllInventoryDocflowsAsync(accountId, relatedDocflowId, relatedDocumentId, filter);

        public async Task<Docflow> GetInventoryDocflowAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId) =>
            await ClientRefit.GetInventoryDocflowAsync(accountId, relatedDocflowId, relatedDocumentId, inventoryId);

        public async Task<byte[]> PrintInventoryDocflowDocumentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            byte[] decryptedDocumentContent) => Convert.FromBase64String(
            await ClientRefit.PrintInventoryDocflowDocumentAsync(
                accountId,
                relatedDocflowId,
                relatedDocumentId,
                inventoryId,
                documentId,
                new PrintDocumentData {Content = Convert.ToBase64String(decryptedDocumentContent)}));

        public async Task<byte[]> GetInventoryDocflowDocumentEncryptedContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId) => Convert.FromBase64String(
            await ClientRefit.GetInventoryDocflowDocumentEncryptedContentAsync(
                accountId,
                relatedDocflowId,
                relatedDocumentId,
                inventoryId,
                documentId));

        public async Task<byte[]> GetInventoryDocflowDocumentDecryptedContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId) => Convert.FromBase64String(
            await ClientRefit.GetInventoryDocflowDocumentDecryptedContentAsync(
                accountId,
                relatedDocflowId,
                relatedDocumentId,
                inventoryId,
                documentId));

        public async Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid signatureId) => Convert.FromBase64String(
            await ClientRefit.GetSignatureContentAsync(
                accountId,
                relatedDocflowId,
                relatedDocumentId,
                inventoryId,
                documentId,
                signatureId));

        public async Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent) => await ClientRefit.GenerateDocumentReplyAsync(
                accountId,
                relatedDocflowId,
                relatedDocumentId,
                inventoryId,
                documentId,
                documentType.ToString(),
                new GenerateReplyDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)});

        public async Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] senderIpContent) => await ClientRefit.SendDocumentReplyAsync(
                accountId,
                relatedDocflowId,
                relatedDocumentId,
                inventoryId,
                documentId,
                replyId,
                new SendReplyDocumentRequest {SenderIp = Convert.ToBase64String(senderIpContent)});

        public async Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] content) => await ClientRefit.UpdateDocumentReplyContentAsync(
                accountId,
                relatedDocflowId,
                relatedDocumentId,
                inventoryId,
                documentId,
                replyId,
                Convert.ToBase64String(content));

        public async Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] signature) => await ClientRefit.UpdateDocumentReplySignatureAsync(
                accountId,
                relatedDocflowId,
                relatedDocumentId,
                inventoryId,
                documentId,
                replyId,
                Convert.ToBase64String(signature));

        public async Task<ApiReplyDocument> GetDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId) => await ClientRefit.GetDocumentReplyAsync(
                accountId,
                relatedDocflowId,
                relatedDocumentId,
                inventoryId,
                documentId,
                replyId);

        public async Task<SignResult> ConfirmCloudSignDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            Guid requestId,
            string code) => await ClientRefit.ConfirmCloudSignDocumentReplyAsync(
            accountId,
            relatedDocflowId,
            relatedDocumentId,
            inventoryId,
            documentId,
            replyId,
            requestId,
            code);

        public async Task<ApiTaskResult<CryptOperationStatusResult>> GetDocflowReplyDocumentTaskAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            Guid apiTaskId) => await ClientRefit.GetDocflowReplyDocumentTaskAsync(
            accountId,
            relatedDocflowId,
            relatedDocumentId,
            inventoryId,
            documentId,
            replyId,
            apiTaskId);

        public async Task<SignInitResult> CloudSignDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            bool forceConfirmation = true) => await ClientRefit.CloudSignDocumentReplyAsync(
            accountId,
            relatedDocflowId,
            relatedDocumentId,
            inventoryId,
            documentId,
            replyId,
            forceConfirmation);

        public async Task<byte[]> ConfirmDocumentContentDecryptionAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid requestId,
            string code,
            bool unzip = false) => await ClientRefit.ConfirmDocumentContentDecryptionAsync(
            accountId,
            relatedDocflowId,
            relatedDocumentId,
            inventoryId,
            documentId,
            requestId,
            code,
            unzip);

        public async Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            byte[] certificateContent) => await ClientRefit.DecryptDocumentContentAsync(
            accountId,
            relatedDocflowId,
            relatedDocumentId,
            inventoryId,
            documentId,
            new DecryptDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)});

        public InventoryDocflowsClient(HttpClient client) => ClientRefit = RestService.For<IInventoryDocflowsClientRefit>(client);
    }
}
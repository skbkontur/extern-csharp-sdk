using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.Logging;
using ExternDotnetSDK.Clients.Common.RequestSenders;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Documents;
using ExternDotnetSDK.Models.Documents.Data;
using ExternDotnetSDK.Models.Drafts;

namespace ExternDotnetSDK.Clients.InventoryDocflows
{
    public class InventoryDocflowsClient : IInventoryDocflowsClient
    {
        private readonly InnerCommonClient client;

        public InventoryDocflowsClient(ILogger logger, IRequestSender requestSender) =>
            client = new InnerCommonClient(logger, requestSender);

        public async Task<DocflowPage> GetAllInventoryDocflowsAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter = null) =>
            await client.SendRequestAsync<DocflowPage>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories",
                filter?.ConvertToQueryParameters());

        public async Task<Docflow> GetInventoryDocflowAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId) =>
            await client.SendRequestAsync<Docflow>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}");

        public async Task<byte[]> PrintInventoryDocflowDocumentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            byte[] decryptedDocumentContent) =>
            await client.SendRequestAsync<byte[]>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/print",
                contentDto: new PrintDocumentData {Content = Convert.ToBase64String(decryptedDocumentContent)});

        public async Task<byte[]> GetInventoryDocflowDocumentEncryptedContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId) =>
            await client.SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/encrypted-content");

        public async Task<byte[]> GetInventoryDocflowDocumentDecryptedContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId) =>
            await client.SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/decrypted-content");

        public async Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid signatureId) =>
            await client.SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/signatures/{signatureId}/content");

        public async Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent) =>
            await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/generate-reply",
                new Dictionary<string, object> {["documentType"] = documentType},
                new GenerateReplyDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)});

        public async Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] senderIpContent) =>
            await client.SendRequestAsync<Docflow>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/send",
                contentDto: new SendReplyDocumentRequest {SenderIp = Convert.ToBase64String(senderIpContent)});

        public async Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] content) =>
            await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/content",
                contentDto: Convert.ToBase64String(content));

        public async Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] signature) =>
            await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/signature",
                contentDto: Convert.ToBase64String(signature));

        public async Task<ApiReplyDocument> GetDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId) =>
            await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}");

        public async Task<SignResult> ConfirmCloudSignDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            Guid requestId,
            string code) =>
            await client.SendRequestAsync<SignResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/cloud-sign-confirm",
                new Dictionary<string, object>
                {
                    ["code"] = code,
                    ["requestId"] = requestId
                });

        public async Task<ApiTaskResult<CryptOperationStatusResult>> GetDocflowReplyDocumentTaskAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            Guid apiTaskId) =>
            await client.SendRequestAsync<ApiTaskResult<CryptOperationStatusResult>>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/tasks/{apiTaskId}");

        public async Task<SignInitResult> CloudSignDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            bool forceConfirmation = true) =>
            await client.SendRequestAsync<SignInitResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/cloud-sign",
                new Dictionary<string, object> {["forceConfirmation"] = forceConfirmation});

        public async Task<byte[]> ConfirmDocumentContentDecryptionAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid requestId,
            string code,
            bool unzip = false) =>
            await client.SendRequestAsync<byte[]>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/decrypt-content-confirm");

        public async Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            byte[] certificateContent) =>
            await client.SendRequestAsync<DecryptionInitResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/decrypt-content",
                contentDto: new DecryptDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)});
    }
}
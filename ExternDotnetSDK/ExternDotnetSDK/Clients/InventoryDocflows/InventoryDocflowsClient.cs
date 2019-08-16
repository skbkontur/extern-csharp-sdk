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

namespace ExternDotnetSDK.Clients.InventoryDocflows
{
    public class InventoryDocflowsClient : InnerCommonClient, IInventoryDocflowsClient
    {
        public InventoryDocflowsClient(ILogError logError, HttpClient client)
            : base(logError, client)
        {
        }

        public async Task<DocflowPage> GetAllInventoryDocflowsAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter = null) =>
            await SendRequestAsync<DocflowPage>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories",
                filter?.ConvertToQueryParameters());

        public async Task<Docflow> GetInventoryDocflowAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId) =>
            await SendRequestAsync<Docflow>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}");

        public async Task<byte[]> PrintInventoryDocflowDocumentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            byte[] decryptedDocumentContent) =>
            await SendRequestAsync<byte[]>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/print",
                new PrintDocumentData {Content = Convert.ToBase64String(decryptedDocumentContent)});

        public async Task<byte[]> GetInventoryDocflowDocumentEncryptedContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId) =>
            await SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/encrypted-content");

        public async Task<byte[]> GetInventoryDocflowDocumentDecryptedContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId) =>
            await SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/decrypted-content");

        public async Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid signatureId) =>
            await SendRequestAsync<byte[]>(
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
            await SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/generate-reply",
                new GenerateReplyDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)},
                new Dictionary<string, object> {["documentType"] = documentType});

        public async Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] senderIpContent) =>
            await SendRequestAsync<Docflow>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/send",
                new SendReplyDocumentRequest {SenderIp = Convert.ToBase64String(senderIpContent)});

        public async Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] content) =>
            await SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/content",
                Convert.ToBase64String(content));

        public async Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] signature) =>
            await SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/signature",
                Convert.ToBase64String(signature));

        public async Task<ApiReplyDocument> GetDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId) =>
            await SendRequestAsync<ApiReplyDocument>(
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
            await SendRequestAsync<SignResult>(
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
            await SendRequestAsync<ApiTaskResult<CryptOperationStatusResult>>(
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
            await SendRequestAsync<SignInitResult>(
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
            await SendRequestAsync<byte[]>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/decrypt-content-confirm");

        public async Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            byte[] certificateContent) =>
            await SendRequestAsync<DecryptionInitResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/decrypt-content",
                new DecryptDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)});
    }
}
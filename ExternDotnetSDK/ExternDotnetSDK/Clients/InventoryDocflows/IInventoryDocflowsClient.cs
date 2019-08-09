using System;
using System.Threading.Tasks;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Documents;

namespace ExternDotnetSDK.Clients.InventoryDocflows
{
    public interface IInventoryDocflowsClient
    {
        IInventoryDocflowsClientRefit ClientRefit { get; }

        Task<DocflowPage> GetAllInventoryDocflowsAsync(
            Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, DocflowFilter filter = null);

        Task<Docflow> GetInventoryDocflowAsync(Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, Guid inventoryId);

        Task<byte[]> PrintInventoryDocflowDocumentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            byte[] decryptedDocumentContent);

        Task<byte[]> GetInventoryDocflowDocumentEncryptedContentAsync(
            Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, Guid inventoryId, Guid documentId);

        Task<byte[]> GetInventoryDocflowDocumentDecryptedContentAsync(
            Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, Guid inventoryId, Guid documentId);

        Task<byte[]> GetSignatureContentAsync(
            Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, Guid inventoryId, Guid documentId, Guid signatureId);

        Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent);

        Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] senderIpContent);

        Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] content);

        Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] signature);
    }
}
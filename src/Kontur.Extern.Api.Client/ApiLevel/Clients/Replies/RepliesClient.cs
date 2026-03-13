using System;
using System.Net;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows.Documents;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Replies;
using Kontur.Extern.Api.Client.Http;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Replies
{
    public class RepliesClient : IRepliesClient
    {
        private readonly IHttpRequestFactory http;

        public RepliesClient(IHttpRequestFactory http) => this.http = http;

        public Task<ReplyDocument> GetReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null)
        {
            return GetReplyAsync(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}",
                $"{nameof(RepliesClient)}.{nameof(GetReplyAsync)}",
                timeout
            );
        }

        public Task<ReplyDocument> GetInventoryReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null)
        {
            return GetReplyAsync(
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}",
                $"{nameof(RepliesClient)}.{nameof(GetInventoryReplyAsync)}",
                timeout
            );
        }

        public Task<ReplyDocument> GenerateReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            byte[] certificate,
            TimeSpan? timeout = null)
        {
            var body = new GenerateReplyDocumentRequest
            {
                CertificateBase64 = certificate
            };
            var url = new RequestUrlBuilder($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/generate-reply")
                .AppendToQuery("documentType", documentType.Nss)
                .Build();
            var callingMethod = $"{nameof(RepliesClient)}.{nameof(GenerateReplyAsync)}";
            return PostReplyAsync(url, body, callingMethod, timeout);
        }

        public Task<ReplyDocument> GenerateReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            string[] declineNoticeErrorCodes,
            byte[] certificate,
            TimeSpan? timeout = null)
        {
            var body = new GenerateReplyDocumentRequest
            {
                CertificateBase64 = certificate
            };
            var url = new RequestUrlBuilder($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/generate-reply")
                .AppendToQuery("documentType", documentType.Nss)
                .AppendToQuery("declineNoticeErrorCode", declineNoticeErrorCodes)
                .Build();
            var callingMehod = $"{nameof(RepliesClient)}.{nameof(GenerateReplyAsync)}";
            return PostReplyAsync(url, body, callingMehod, timeout);
        }

        public Task<ReplyDocument> GenerateInventoryReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Urn documentType,
            byte[] certificate,
            TimeSpan? timeout = null)
        {
            var body = new GenerateReplyDocumentRequest
            {
                CertificateBase64 = certificate
            };
            var url = new RequestUrlBuilder($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/generate-reply")
                .AppendToQuery("documentType", documentType.Nss)
                .Build();
            var callingMethod = $"{nameof(RepliesClient)}.{nameof(GenerateInventoryReplyAsync)}";
            return PostReplyAsync(url, body, callingMethod, timeout);
        }

        public Task<IDocflowWithDocuments> SendReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            IPAddress senderIp,
            TimeSpan? timeout = null)
        {
            var body = new SendReplyDocumentRequest
            {
                SenderIp = senderIp
            };
            var url = $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/send";
            var callingMethod = $"{nameof(RepliesClient)}.{nameof(SendReplyAsync)}";
            return PostDocflowAsync(url, body, callingMethod, timeout);
        }

        public Task<IDocflowWithDocuments> SendInventoryReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            IPAddress senderIp,
            TimeSpan? timeout = null)
        {
            var body = new SendReplyDocumentRequest
            {
                SenderIp = senderIp
            };
            var url = $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/send";
            var callingMethod = $"{nameof(RepliesClient)}.{nameof(SendInventoryReplyAsync)}";
            return http.PutAsync<SendReplyDocumentRequest, IDocflowWithDocuments>(url, body, timeout, callingMethod);
        }

        public Task<ReplyDocument> UpdateReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/signature";
            var callingMethod = $"{nameof(RepliesClient)}.{nameof(UpdateReplySignatureAsync)}";
            return http.PutAsync<byte[], ReplyDocument>(url, signature, timeout, callingMethod);
        }

        public Task<ReplyDocument> UpdateInventoryReplySignatureAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/signature";
            var callingMethod = $"{nameof(RepliesClient)}.{nameof(UpdateInventoryReplySignatureAsync)}";
            return http.PutAsync<byte[], ReplyDocument>(url, signature, timeout, callingMethod);
        }

        public Task<ReplyDocument> UpdateReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/content";
            var callingMethod = $"{nameof(RepliesClient)}.{nameof(UpdateReplyContentAsync)}";
            return http.PutAsync<byte[], ReplyDocument>(url, content, timeout, callingMethod);
        }

        public Task<ReplyDocument> UpdateInventoryReplyContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/content";
            var callingMethod = $"{nameof(RepliesClient)}.{nameof(UpdateInventoryReplyContentAsync)}";
            return http.PutAsync<byte[], ReplyDocument>(url, content, timeout, callingMethod);
        }

        private Task<IDocflowWithDocuments> PostDocflowAsync<TDto>(string url, TDto dto, string callingMethod, TimeSpan? timeout) =>
            http.PostAsync<TDto, IDocflowWithDocuments>(url, dto, timeout, callingMethod);

        private Task<ReplyDocument> PostReplyAsync<TDto>(Uri url, TDto dto, string callingMethod, TimeSpan? timeout) =>
            http.PostAsync<TDto, ReplyDocument>(url, dto, timeout, callingMethod);

        private Task<ReplyDocument> GetReplyAsync(string url, string callingMethod, TimeSpan? timeout) => http.GetAsync<ReplyDocument>(url, timeout, callingMethod);
    }
}
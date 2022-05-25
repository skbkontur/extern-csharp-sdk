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
            return GetReplyAsync($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}", timeout);
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
            return GetReplyAsync($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}", timeout);
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
            return PostReplyAsync(url, body, timeout);
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
            return PostReplyAsync(url, body, timeout);
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
            return PostReplyAsync(url, body, timeout);
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
            return PostDocflowAsync(url, body, timeout);
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
            return http.PutAsync<SendReplyDocumentRequest, IDocflowWithDocuments>(url, body, timeout);
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
            return http.PutAsync<byte[], ReplyDocument>(url, signature, timeout);
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
            return http.PutAsync<byte[], ReplyDocument>(url, signature, timeout);
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
            return http.PutAsync<byte[], ReplyDocument>(url, content, timeout);
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
            return http.PutAsync<byte[], ReplyDocument>(url, content, timeout);
        }

        private Task<IDocflowWithDocuments> PostDocflowAsync<TDto>(string url, TDto dto, TimeSpan? timeout) =>
            http.PostAsync<TDto, IDocflowWithDocuments>(url, dto, timeout);

        private Task<ReplyDocument> PostReplyAsync<TDto>(Uri url, TDto dto, TimeSpan? timeout) =>
            http.PostAsync<TDto, ReplyDocument>(url, dto, timeout);

        private Task<ReplyDocument> GetReplyAsync(string url, TimeSpan? timeout) => http.GetAsync<ReplyDocument>(url, timeout);
    }
}
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Api;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Documents;
using Kontur.Extern.Client.ApiLevel.Models.Documents.Data;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.HttpLevel;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.ApiLevel.Clients.Replies
{
    public class RepliesClient : IRepliesClient
    {
        private readonly IHttpRequestsFactory http;
        
        public RepliesClient(IHttpRequestsFactory http) => this.http = http;

        public Task<ApiReplyDocument> GetReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null)
        {
            return GetReplyAsync($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}", timeout);
        }

        public Task<ApiReplyDocument> GetInventoryReplyAsync(
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

        public Task<ApiReplyDocument> GenerateReplyAsync(
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

        public Task<ApiReplyDocument> GenerateReplyAsync(
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

        public Task<ApiReplyDocument> GenerateInventoryReplyAsync(
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

        public Task<Docflow> SendReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string senderIp,
            TimeSpan? timeout = null)
        {
            var body = new SendReplyDocumentRequest
            {
                SenderIp = senderIp
            };
            var url = $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/send";
            return PostDocflowAsync(url, body, timeout);
        }

        public Task<Docflow> SendInventoryReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            string senderIp,
            TimeSpan? timeout = null)
        {
            var body = new SendReplyDocumentRequest
            {
                SenderIp = senderIp
            };
            var url = $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/send";
            return http.PutAsync<SendReplyDocumentRequest, Docflow>(url, body, timeout);
        }

        public Task<ApiReplyDocument> UpdateReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/signature";
            return http.PutAsync<byte[], ApiReplyDocument>(url, signature, timeout);
        }

        public Task<ApiReplyDocument> UpdateInventoryReplySignatureAsync(
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
            return http.PutAsync<byte[], ApiReplyDocument>(url, signature, timeout);
        }

        public Task<ApiReplyDocument> UpdateReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/content";
            return http.PutAsync<byte[], ApiReplyDocument>(url, content, timeout);
        }

        public Task<ApiReplyDocument> UpdateInventoryReplyContentAsync(
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
            return http.PutAsync<byte[], ApiReplyDocument>(url, content, timeout);
        }

        public Task<SignInitResult> DssSignReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/cloud-sign";
            return http.PostAsync<SignInitResult>(url, timeout);
        }

        public Task<SignInitResult> DssSignInventoryReplyAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/cloud-sign";
            return http.PostAsync<SignInitResult>(url, timeout);
        }

        public Task<ApiTaskResult<CryptOperationStatusResult>> GetDssSignReplyTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/tasks/{taskId}";
            return http.GetAsync<ApiTaskResult<CryptOperationStatusResult>>(url, timeout);
        }

        public Task<ApiTaskResult<CryptOperationStatusResult>> GetDssSignReplyTaskAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid replyId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/tasks/{taskId}";
            return http.GetAsync<ApiTaskResult<CryptOperationStatusResult>>(url, timeout);
        }
        
        private Task<Docflow> PostDocflowAsync<TDto>(string url, TDto dto, TimeSpan? timeout) => 
            http.PostAsync<TDto, Docflow>(new Uri(url), dto, timeout);

        private Task<ApiReplyDocument> PostReplyAsync<TDto>(Uri url, TDto dto, TimeSpan? timeout) => 
            http.PostAsync<TDto, ApiReplyDocument>(url, dto, timeout);

        private Task<ApiReplyDocument> GetReplyAsync(string url, TimeSpan? timeout) => http.GetAsync<ApiReplyDocument>(url, timeout);
    }
}
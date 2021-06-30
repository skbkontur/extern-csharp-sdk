using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.Requests;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Documents;
using Kontur.Extern.Client.Models.Documents.Data;
using Kontur.Extern.Client.Models.Drafts;

namespace Kontur.Extern.Client.Clients.Replies
{
    public class RepliesClient : IRepliesClient
    {
        private readonly InnerCommonClient client;
        private readonly IRequestBodySerializer requestBodySerializer;

        public RepliesClient(ILogger logger, _IRequestSender requestSender, IRequestBodySerializer requestBodySerializer)
        {
            this.requestBodySerializer = requestBodySerializer;
            client = new InnerCommonClient(logger, requestSender);
        }

        public Task<ApiReplyDocument> GetReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}");
            return client.SendJsonRequestAsync<ApiReplyDocument>(request, timeout);
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
            var request = Request.Get($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}");
            return client.SendJsonRequestAsync<ApiReplyDocument>(request, timeout);
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
            var request = Request.Post(url)
                .WithContent(requestBodySerializer.SerializeToJson(body));
            return client.SendJsonRequestAsync<ApiReplyDocument>(request, timeout);
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
            var request = Request.Post(url)
                .WithContent(requestBodySerializer.SerializeToJson(body));
            return client.SendJsonRequestAsync<ApiReplyDocument>(request, timeout);
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
            var request = Request.Post(url)
                .WithContent(requestBodySerializer.SerializeToJson(body));
            return client.SendJsonRequestAsync<ApiReplyDocument>(request, timeout);
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
            var request = Request.Post($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/send")
                .WithContent(requestBodySerializer.SerializeToJson(body));
            return client.SendJsonRequestAsync<Docflow>(request, timeout);
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
            var request = Request.Post($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/send")
                .WithContent(requestBodySerializer.SerializeToJson(body));
            return client.SendJsonRequestAsync<Docflow>(request, timeout);
        }

        public Task<ApiReplyDocument> UpdateReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null)
        {
            var request = Request.Put($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/signature")
                .WithContent(requestBodySerializer.SerializeToJson(signature));
            return client.SendJsonRequestAsync<ApiReplyDocument>(request, timeout);
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
            var request = Request.Put($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/signature")
                .WithContent(requestBodySerializer.SerializeToJson(signature));
            return client.SendJsonRequestAsync<ApiReplyDocument>(request, timeout);
        }

        public Task<ApiReplyDocument> UpdateReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null)
        {
            var request = Request.Put($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/content")
                .WithContent(requestBodySerializer.SerializeToJson(content));
            return client.SendJsonRequestAsync<ApiReplyDocument>(request, timeout);
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
            var request = Request.Put($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/content")
                .WithContent(requestBodySerializer.SerializeToJson(content));
            return client.SendJsonRequestAsync<ApiReplyDocument>(request, timeout);
        }

        public Task<SignInitResult> DssSignReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/cloud-sign");
            return client.SendJsonRequestAsync<SignInitResult>(request, timeout);
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
            var request = Request.Post($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/replies/{replyId}/cloud-sign");
            return client.SendJsonRequestAsync<SignInitResult>(request, timeout);
        }

        public Task<ApiTaskResult<CryptOperationStatusResult>> GetDssSignReplyTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/tasks/{taskId}");
            return client.SendJsonRequestAsync<ApiTaskResult<CryptOperationStatusResult>>(request, timeout);
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
            var request = Request.Get($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/tasks/{taskId}");
            return client.SendJsonRequestAsync<ApiTaskResult<CryptOperationStatusResult>>(request, timeout);
        }
    }
}
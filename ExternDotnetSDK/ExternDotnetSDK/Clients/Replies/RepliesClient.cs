using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Documents;
using Kontur.Extern.Client.Models.Documents.Data;
using Kontur.Extern.Client.Models.Drafts;

namespace Kontur.Extern.Client.Clients.Docflows
{
    //todo Сделать нормальные тесты для методов.
    public class RepliesClient : IRepliesClient
    {
        private readonly InnerCommonClient client;

        public RepliesClient(ILogger logger, IRequestSender requestSender)
        {
            client = new InnerCommonClient(logger, requestSender);
        }

        public async Task<ApiReplyDocument> GetDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null)
        {
            return await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}",
                timeout: timeout);
        }

        public async Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent,
            TimeSpan? timeout = null)
        {
            return await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/generate-reply",
                new Dictionary<string, object> {[nameof(documentType)] = documentType},
                new GenerateReplyDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)},
                timeout);
        }

        public async Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string senderIp,
            TimeSpan? timeout = null)
        {
            return await client.SendRequestAsync<Docflow>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/send",
                contentDto: new SendReplyDocumentRequest {SenderIp = senderIp},
                timeout: timeout);
        }

        public async Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null)
        {
            return await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/signature",
                contentDto: Convert.ToBase64String(signature),
                timeout: timeout);
        }

        public async Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null)
        {
            return await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/content",
                contentDto: Convert.ToBase64String(content),
                timeout: timeout);
        }

        public async Task<SignInitResult> CloudSignDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            bool forceConfirmation,
            TimeSpan? timeout = null)
        {
            return await client.SendRequestAsync<SignInitResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/cloud-sign",
                new Dictionary<string, object> {[nameof(forceConfirmation)] = forceConfirmation},
                timeout: timeout);
        }

        public async Task<SignResult> CloudSignConfirmDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string code,
            string requestId,
            TimeSpan? timeout = null)
        {
            return await client.SendRequestAsync<SignResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/cloud-sign-confirm",
                new Dictionary<string, object>
                {
                    [nameof(code)] = code,
                    [nameof(requestId)] = requestId
                },
                timeout: timeout);
        }
    }
}
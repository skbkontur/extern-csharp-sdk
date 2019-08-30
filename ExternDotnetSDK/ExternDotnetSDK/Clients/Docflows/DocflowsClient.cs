using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Common;
using Kontur.Extern.Client.Clients.Common.Logging;
using Kontur.Extern.Client.Clients.Common.RequestSenders;
using Kontur.Extern.Client.Models.Api;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Documents;
using Kontur.Extern.Client.Models.Documents.Data;
using Kontur.Extern.Client.Models.Drafts;

namespace Kontur.Extern.Client.Clients.Docflows
{
    //todo Сделать нормальные тесты для методов.
    public class DocflowsClient : IDocflowsClient
    {
        private readonly InnerCommonClient client;

        public DocflowsClient(ILogger logger, IRequestSender requestSender) =>
            client = new InnerCommonClient(logger, requestSender);

        public async Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DocflowPage>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows",
                filter?.ConvertToQueryParameters(),
                timeout: timeout);

        public async Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Docflow>(HttpMethod.Get, $"/v1/{accountId}/docflows/{docflowId}", timeout: timeout);

        public async Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<List<Document>>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents",
                timeout: timeout);

        public async Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Document>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}",
                timeout: timeout);

        public async Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DocflowDocumentDescription>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/description",
                timeout: timeout);

        public async Task<byte[]> GetEncryptedDocumentContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/encrypted-content",
                timeout: timeout);

        public async Task<byte[]> GetDecryptedDocumentContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypted-content",
                timeout: timeout);

        public async Task<List<Signature>> GetDocumentSignaturesAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<List<Signature>>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures",
                timeout: timeout);

        public async Task<Signature> GetSignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Signature>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}",
                timeout: timeout);

        public async Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}/content",
                timeout: timeout);

        public async Task<ApiTaskResult<byte[]>> GetApiTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid apiTaskId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiTaskResult<byte[]>>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/tasks/{apiTaskId}",
                timeout: timeout);

        public async Task<ApiReplyDocument> GetDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}",
                timeout: timeout);

        public async Task<string> PrintDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] data,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<string>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/print",
                contentDto: new PrintDocumentData {Content = Convert.ToBase64String(data)},
                timeout: timeout);

        public async Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            DecryptDocumentRequestData data,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DecryptionInitResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content",
                contentDto: data,
                timeout: timeout);

        public async Task<byte> ConfirmDocumentContentDecryptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool unzip = false,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<byte>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content-confirm",
                new Dictionary<string, object>
                {
                    [nameof(requestId)] = requestId,
                    [nameof(code)] = code,
                    [nameof(unzip)] = unzip
                },
                timeout: timeout);

        public async Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/generate-reply",
                new Dictionary<string, object> {[nameof(documentType)] = documentType},
                new GenerateReplyDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)},
                timeout);

        public async Task<RecognizedMeta> RecognizeDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] content,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<RecognizedMeta>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/recognize",
                contentDto: Convert.ToBase64String(content),
                timeout: timeout);

        public async Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string senderIp,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<Docflow>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/send",
                contentDto: new SendReplyDocumentRequest {SenderIp = senderIp},
                timeout: timeout);

        public async Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/signature",
                contentDto: Convert.ToBase64String(signature),
                timeout: timeout);

        public async Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/content",
                contentDto: Convert.ToBase64String(content),
                timeout: timeout);

        public async Task<SignInitResult> CloudSignDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            bool forceConfirmation,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<SignInitResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/cloud-sign",
                new Dictionary<string, object> {[nameof(forceConfirmation)] = forceConfirmation},
                timeout: timeout);

        public async Task<SignResult> CloudSignConfirmDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string code,
            string requestId,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<SignResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/cloud-sign-confirm",
                new Dictionary<string, object>
                {
                    [nameof(code)] = code,
                    [nameof(requestId)] = requestId
                },
                timeout: timeout);

        public async Task<DocflowPage> GetRelatedDocflows(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter,
            TimeSpan? timeout = null) =>
            await client.SendRequestAsync<DocflowPage>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/related",
                filter.ConvertToQueryParameters(),
                timeout: timeout);
    }
}
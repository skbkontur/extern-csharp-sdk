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

namespace ExternDotnetSDK.Clients.Docflows
{
    public class DocflowsClient : InnerCommonClient, IDocflowsClient
    {
        public DocflowsClient(ILogError logError, HttpClient client)
            : base(logError, client)
        {
        }

        public async Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null) =>
            await SendRequestAsync<DocflowPage>(HttpMethod.Get, $"/v1/{accountId}/docflows", filter?.StringifyParams());

        public async Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId) =>
            await SendRequestAsync<Docflow>(HttpMethod.Get, $"/v1/{accountId}/docflows/{docflowId}");

        public async Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId) =>
            await SendRequestAsync<List<Document>>(HttpMethod.Get, $"/v1/{accountId}/docflows/{docflowId}/documents");

        public async Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await SendRequestAsync<Document>(HttpMethod.Get, $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}");

        public async Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId) =>
            await SendRequestAsync<DocflowDocumentDescription>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/description");

        public async Task<byte[]> GetEncryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/encrypted-content");

        public async Task<byte[]> GetDecryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypted-content");

        public async Task<List<Signature>> GetDocumentSignaturesAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await SendRequestAsync<List<Signature>>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures");

        public async Task<Signature> GetSignatureAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId) =>
            await SendRequestAsync<Signature>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}");

        public async Task<byte[]> GetSignatureContentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId) =>
            await SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}/content");

        public async Task<ApiTaskResult<byte[]>> GetApiTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid apiTaskId) => await SendRequestAsync<ApiTaskResult<byte[]>>(
            HttpMethod.Get,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/tasks/{apiTaskId}");

        public async Task<ApiReplyDocument> GetDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId) => await SendRequestAsync<ApiReplyDocument>(
            HttpMethod.Get,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}");

        public async Task<string> PrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, byte[] data) =>
            await SendRequestAsync<string>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/print",
                new PrintDocumentData {Content = Convert.ToBase64String(data)});

        public async Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            DecryptDocumentRequestData data) => await SendRequestAsync<DecryptionInitResult>(
            HttpMethod.Post,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content",
            data);

        public async Task<byte> ConfirmDocumentContentDecryptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool unzip = false) => await SendRequestAsync<byte>(
            HttpMethod.Post,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content-confirm",
            StringifyUriQueryParams(
                new Dictionary<string, object>
                {
                    [nameof(requestId)] = requestId,
                    [nameof(code)] = code,
                    [nameof(unzip)] = unzip,
                }));

        public async Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent) => await SendRequestAsync<ApiReplyDocument>(
            HttpMethod.Post,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/generate-reply",
            new GenerateReplyDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)},
            StringifyUriQueryParams(new Dictionary<string, object> {[nameof(documentType)] = documentType}));

        public async Task<RecognizedMeta> RecognizeDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] content) => await SendRequestAsync<RecognizedMeta>(
            HttpMethod.Post,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/recognize",
            contentDto: Convert.ToBase64String(content));

        public async Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string senderIp) => await SendRequestAsync<Docflow>(
            HttpMethod.Post,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/send",
            new SendReplyDocumentRequest {SenderIp = senderIp});

        public async Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature) => await SendRequestAsync<ApiReplyDocument>(
            HttpMethod.Put,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/signature",
            contentDto: Convert.ToBase64String(signature));

        public async Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content) => await SendRequestAsync<ApiReplyDocument>(
            HttpMethod.Put,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/content",
            contentDto: Convert.ToBase64String(content));

        public async Task<SignInitResult> CloudSignDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            bool forceConfirmation) => await SendRequestAsync<SignInitResult>(
            HttpMethod.Post,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/cloud-sign",
            uriQueryParams: StringifyUriQueryParams(
                new Dictionary<string, object> {[nameof(forceConfirmation)] = forceConfirmation}));

        public async Task<SignResult> CloudSignConfirmDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string code,
            string requestId) => await SendRequestAsync<SignResult>(
            HttpMethod.Post,
            $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/cloud-sign-confirm",
            uriQueryParams: StringifyUriQueryParams(
                new Dictionary<string, object>
                {
                    [nameof(code)] = code,
                    [nameof(requestId)] = requestId
                }));
    }
}
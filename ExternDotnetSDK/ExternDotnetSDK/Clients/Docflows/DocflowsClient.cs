using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Authentication;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Clients.Common.SendAsync;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Api;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Docflows;
using ExternDotnetSDK.Models.Documents;
using ExternDotnetSDK.Models.Documents.Data;
using ExternDotnetSDK.Models.Drafts;

namespace ExternDotnetSDK.Clients.Docflows
{
    public class DocflowsClient : IDocflowsClient
    {
        private readonly RequestFactory factory;

        public DocflowsClient(ILogger logger, IHttpSender client, IAuthenticationProvider authenticationProvider) =>
            factory = new RequestFactory(logger, client, authenticationProvider);

        public async Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null) =>
            await factory.SendRequestAsync<DocflowPage>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows",
                filter?.ConvertToQueryParameters());

        public async Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId) =>
            await factory.SendRequestAsync<Docflow>(HttpMethod.Get, $"/v1/{accountId}/docflows/{docflowId}");

        public async Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId) =>
            await factory.SendRequestAsync<List<Document>>(HttpMethod.Get, $"/v1/{accountId}/docflows/{docflowId}/documents");

        public async Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await factory.SendRequestAsync<Document>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}");

        public async Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId) =>
            await factory.SendRequestAsync<DocflowDocumentDescription>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/description");

        public async Task<byte[]> GetEncryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await factory.SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/encrypted-content");

        public async Task<byte[]> GetDecryptedDocumentContentAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await factory.SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypted-content");

        public async Task<List<Signature>> GetDocumentSignaturesAsync(Guid accountId, Guid docflowId, Guid documentId) =>
            await factory.SendRequestAsync<List<Signature>>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures");

        public async Task<Signature> GetSignatureAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId) =>
            await factory.SendRequestAsync<Signature>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}");

        public async Task<byte[]> GetSignatureContentAsync(Guid accountId, Guid docflowId, Guid documentId, Guid signatureId) =>
            await factory.SendRequestAsync<byte[]>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}/content");

        public async Task<ApiTaskResult<byte[]>> GetApiTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid apiTaskId) =>
            await factory.SendRequestAsync<ApiTaskResult<byte[]>>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/tasks/{apiTaskId}");

        public async Task<ApiReplyDocument> GetDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId) =>
            await factory.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}");

        public async Task<string> PrintDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, byte[] data) =>
            await factory.SendRequestAsync<string>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/print",
                new PrintDocumentData {Content = Convert.ToBase64String(data)});

        public async Task<DecryptionInitResult> DecryptDocumentContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            DecryptDocumentRequestData data) =>
            await factory.SendRequestAsync<DecryptionInitResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content",
                data);

        public async Task<byte> ConfirmDocumentContentDecryptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool unzip = false) =>
            await factory.SendRequestAsync<byte>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content-confirm",
                new Dictionary<string, object>
                {
                    [nameof(requestId)] = requestId,
                    [nameof(code)] = code,
                    [nameof(unzip)] = unzip
                });

        public async Task<ApiReplyDocument> GenerateDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Urn documentType,
            byte[] certificateContent) =>
            await factory.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/generate-reply",
                new GenerateReplyDocumentRequestData {CertificateBase64 = Convert.ToBase64String(certificateContent)},
                new Dictionary<string, object> {[nameof(documentType)] = documentType});

        public async Task<RecognizedMeta> RecognizeDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] content) =>
            await factory.SendRequestAsync<RecognizedMeta>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/recognize",
                Convert.ToBase64String(content));

        public async Task<Docflow> SendDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string senderIp) =>
            await factory.SendRequestAsync<Docflow>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/send",
                new SendReplyDocumentRequest {SenderIp = senderIp});

        public async Task<ApiReplyDocument> UpdateDocumentReplySignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] signature) =>
            await factory.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/signature",
                Convert.ToBase64String(signature));

        public async Task<ApiReplyDocument> UpdateDocumentReplyContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            byte[] content) =>
            await factory.SendRequestAsync<ApiReplyDocument>(
                HttpMethod.Put,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/content",
                Convert.ToBase64String(content));

        public async Task<SignInitResult> CloudSignDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            bool forceConfirmation) =>
            await factory.SendRequestAsync<SignInitResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/cloud-sign",
                new Dictionary<string, object> {[nameof(forceConfirmation)] = forceConfirmation});

        public async Task<SignResult> CloudSignConfirmDocumentReplyAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid replyId,
            string code,
            string requestId) =>
            await factory.SendRequestAsync<SignResult>(
                HttpMethod.Post,
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/replies/{replyId}/cloud-sign-confirm",
                new Dictionary<string, object>
                {
                    [nameof(code)] = code,
                    [nameof(requestId)] = requestId
                });

        public async Task<DocflowPage> GetRelatedDocflows(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter) =>
            await factory.SendRequestAsync<DocflowPage>(
                HttpMethod.Get,
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/related",
                filter.ConvertToQueryParameters());
    }
}
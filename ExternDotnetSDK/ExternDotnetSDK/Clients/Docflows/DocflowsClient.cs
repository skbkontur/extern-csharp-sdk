using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Kontur.Extern.Client.Clients.Docflows
{
    //todo Сделать нормальные тесты для методов.
    public class DocflowsClient : IDocflowsClient
    {
        private readonly InnerCommonClient client;
        private readonly IRequestBodySerializer requestBodySerializer;

        public DocflowsClient(ILogger logger, IRequestSender requestSender, IRequestBodySerializer requestBodySerializer)
        {
            this.requestBodySerializer = requestBodySerializer;
            client = new InnerCommonClient(logger, requestSender);
        }

        public Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter filter = null, TimeSpan? timeout = null)
        {
            var urlBuilder = new RequestUrlBuilder($"/v1/{accountId}/docflows");
            if (filter != null)
            {
                foreach (var kv in filter.ConvertToQueryParameters().Where(kv => kv.Value != null))
                    urlBuilder.AppendToQuery(kv.Key, kv.Value);
            }
            var request = Request.Get(urlBuilder.Build());
            return client.SendJsonRequestAsync<DocflowPage>(request, timeout);
        }

        public Task<DocflowPage> GetRelatedDocflows(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter filter = null,
            TimeSpan? timeout = null)
        {
            var urlBuilder = new RequestUrlBuilder($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/related");
            if (filter != null)
            {
                foreach (var kv in filter.ConvertToQueryParameters().Where(kv => kv.Value != null))
                    urlBuilder.AppendToQuery(kv.Key, kv.Value);
            }
            var request = Request.Get(urlBuilder.Build());
            return client.SendJsonRequestAsync<DocflowPage>(request, timeout);
        }

        public Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/docflows/{docflowId}");
            return client.SendJsonRequestAsync<Docflow>(request, timeout);
        }

        public Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/docflows/{docflowId}/documents");
            return client.SendJsonRequestAsync<List<Document>>(request, timeout);
        }

        public Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}");
            return client.SendJsonRequestAsync<Document>(request, timeout);
        }

        public Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/description");
            return client.SendJsonRequestAsync<DocflowDocumentDescription>(request, timeout);
        }

        public Task<List<Signature>> GetDocumentSignaturesAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures");
            return client.SendJsonRequestAsync<List<Signature>>(request, timeout);
        }

        public Task<Signature> GetSignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}");
            return client.SendJsonRequestAsync<Signature>(request, timeout);
        }

        public Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}/content");
            return client.SendJsonRequestAsync<byte[]>(request, timeout);
        }

        public Task<PrintDocumentResult> PrintDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null)
        {
            var body = new PrintDocumentRequest
            {
                ContentId = contentId
            };
            var request = Request.Post($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/print")
                .WithContent(requestBodySerializer.SerializeToJson(body));
            return client.SendJsonRequestAsync<PrintDocumentResult>(request, timeout);
        }

        public Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null)
        {
            var body = new PrintDocumentRequest
            {
                ContentId = contentId
            };
            var url = new RequestUrlBuilder($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/print")
                .AppendToQuery("deferred", true)
                .Build();
            var request = Request.Post(url)
                .WithContent(requestBodySerializer.SerializeToJson(body));
            return client.SendJsonRequestAsync<ApiTaskResult<PrintDocumentResult>>(request, timeout);
        }

        public Task<ApiTaskResult<PrintDocumentResult>> GetPrintTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            var request = Request.Get($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/tasks/{taskId}");
            return client.SendJsonRequestAsync<ApiTaskResult<PrintDocumentResult>>(request, timeout);
        }

        public Task<CloudDecryptionInitResult> StartCloudDecryptDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] certificate,
            TimeSpan? timeout = null)
        {
            var body = new CertificateRequest
            {
                Content = certificate
            };
            var request = Request.Post($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content")
                .WithContent(requestBodySerializer.SerializeToJson(body));
            return client.SendJsonRequestAsync<CloudDecryptionInitResult>(request, timeout);
        }

        public Task<DecryptDocumentResult> ConfirmDocumentDecryptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool? unzip = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/confirm-content-decryption")
                .AppendToQuery("requestId", requestId)
                .AppendToQuery("code", code)
                .AppendToQuery("unzip", unzip)
                .Build();
            var request = Request.Post(url);
            return client.SendJsonRequestAsync<DecryptDocumentResult>(request, timeout);
        }

        public Task<DssDecryptionInitResult> StartDssDecryptDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] certificate,
            bool? unzip = null,
            TimeSpan? timeout = null)
        {
            var body = new CertificateRequest
            {
                Content = certificate
            };
            var url = new RequestUrlBuilder($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content")
                .AppendToQuery("unzipIfCan", unzip)
                .Build();
            var request = Request.Post(url)
                .WithContent(requestBodySerializer.SerializeToJson(body));
            return client.SendJsonRequestAsync<DssDecryptionInitResult>(request, timeout);
        }

        public Task<ApiTaskResult<DecryptDocumentResult>> GetDssDecryptDocumentTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            var request = Request.Post($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/tasks/{taskId}");
            return client.SendJsonRequestAsync<ApiTaskResult<DecryptDocumentResult>>(request, timeout);
        }

        public Task<RecognizeResult> RecognizeDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null)
        {
            var body = new RecognizeRequest
            {
                ContentId = contentId
            };
            var request = Request.Post($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/recognize")
                .WithContent(requestBodySerializer.SerializeToJson(body));
            return client.SendJsonRequestAsync<RecognizeResult>(request, timeout);
        }
    }
}